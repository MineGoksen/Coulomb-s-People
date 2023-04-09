// COPYRIGHT 1995-2022 ESRI
// TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
// Unpublished material - all rights reserved under the
// Copyright Laws of the United States and applicable international
// laws, treaties, and conventions.
//
// For additional information, contact:
// Attn: Contracts and Legal Department
// Environmental Systems Research Institute, Inc.
// 380 New York Street
// Redlands, California 92373
// USA
//
// email: legal@esri.com
using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Utils.Math;
using Esri.GameEngine.Geometry;
using Esri.GameEngine.View;
using Esri.HPFramework;
using System;
using Unity.Mathematics;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Esri.ArcGISMapsSDK.Samples.Components
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(HPTransform))]
	[AddComponentMenu("ArcGIS Maps SDK/Samples/ArcGIS Camera Controller")]
	public class ArcGISCameraControllerComponent : MonoBehaviour
	{
		private ArcGISMapComponent arcGISMapComponent;
		private HPTransform hpTransform;

		public ArcGISCameraControllerComponentActions CameraActions;
		private InputAction UpControls;
		private InputAction ForwardControls;
		private InputAction RightControls;
		private InputAction GetCoords;


		private float TranslationSpeed = 0.0f;
		private float RotationSpeed = 100.0f;
		private double MouseScrollSpeed = 0.001f;

		private static double MaxCameraHeight = 11000000.0;
		private static double MinCameraHeight = 0.1;
		private static double MaxCameraLatitude = 85.0;

		private double3 lastCartesianPoint = double3.zero;
		private ArcGISPoint lastArcGISPoint = new ArcGISPoint(0, 0, 0, ArcGISSpatialReference.WGS84());
		private double lastDotVC = 0.0f;
		private bool firstDragStep = true;

		private Vector3 lastMouseScreenPosition;
		private bool firstOnFocus = true;

		public double MaxSpeed = 2000000.0;
		public double MinSpeed = 1000.0;

		public ArcGISLocationComponent location;

		private void Awake()
		{
			location = gameObject.GetComponentInParent<ArcGISLocationComponent>();
			lastMouseScreenPosition = GetMousePosition();

			Application.focusChanged += FocusChanged;

			CameraActions = new ArcGISCameraControllerComponentActions();
			UpControls = CameraActions.Move.Up;
			ForwardControls = CameraActions.Move.Forward;
			RightControls = CameraActions.Move.Right;
			GetCoords = CameraActions.Move.GetCoordinates;
		}

		void OnEnable()
		{
			arcGISMapComponent = gameObject.GetComponentInParent<ArcGISMapComponent>();
			hpTransform = GetComponent<HPTransform>();

			UpControls.Enable();
			ForwardControls.Enable();
			RightControls.Enable();
			GetCoords.Enable();
		}

		private void OnDisable()
		{
			UpControls.Disable();
			ForwardControls.Disable();
			RightControls.Disable();
			GetCoords.Disable();
		}

		private Vector3 GetMousePosition()
		{
			return Mouse.current.position.ReadValue();

		}

		void GetTargetLocation()
		{

			Debug.Log("Latitude: " + location.Position.Y + ", Latitude: " + location.Position.X);
		}

		private double3 GetTotalTranslation()
		{
			var forward = hpTransform.Forward.ToDouble3();
			var right = hpTransform.Right.ToDouble3();
			var up = hpTransform.Up.ToDouble3();

			var totalTranslation = double3.zero;

			up *= UpControls.ReadValue<float>() * TranslationSpeed * Time.deltaTime;
			right *= RightControls.ReadValue<float>() * TranslationSpeed * Time.deltaTime;
			forward *= ForwardControls.ReadValue<float>() * TranslationSpeed * Time.deltaTime;
			totalTranslation += up + right + forward;


			return totalTranslation;
		}

		private float GetMouseScollValue()
		{
			return Mouse.current.scroll.ReadValue().y;

		}

		private bool IsMouseLeftClicked()
		{
			return Mouse.current.leftButton.ReadValue() == 1;

		}

		private bool IsMouseRightClicked()
		{
			return Mouse.current.rightButton.ReadValue() == 1;

		}


		void Start()
		{
			if (arcGISMapComponent == null)
			{
				Debug.LogError("An ArcGISMapComponent could not be found. Please make sure this GameObject is a child of a GameObject with an ArcGISMapComponent attached");

				enabled = false;
				return;
			}
		}

		void Update()
		{
			if (arcGISMapComponent == null)
			{
				return;
			}

			if (arcGISMapComponent.View.SpatialReference == null)
			{
				// Not functional until we have a spatial reference
				return;
			}

			GetCoords.performed += ctx =>
									  GetTargetLocation();

			DragMouseEvent();

			UpdateNavigation();
		}

		/// <summary>
		/// Move the camera based on user input
		/// </summary>
		private void UpdateNavigation()
		{
			var altitude = arcGISMapComponent.View.AltitudeAtCartesianPosition(Position);
			UpdateSpeed(altitude);

			var totalTranslation = GetTotalTranslation();

			if (GetMouseScollValue() != 0.0)
			{
				var towardsMouse = GetMouseRayCastDirection();
				var delta = Math.Max(1.0, (altitude - MinCameraHeight)) * MouseScrollSpeed * GetMouseScollValue();
				totalTranslation += towardsMouse * delta;
			}

			if (!totalTranslation.Equals(double3.zero))
			{
				MoveCamera(totalTranslation);
			}

		}

		/// <summary>
		/// Move the camera
		/// </summary>
		private void MoveCamera(double3 movDir)
		{
			var distance = math.length(movDir);
			movDir /= distance;

			var cameraPosition = Position;
			var cameraRotation = Rotation;

			if (arcGISMapComponent.MapType == GameEngine.Map.ArcGISMapType.Global)
			{
				var spheroidData = arcGISMapComponent.View.SpatialReference.SpheroidData;
				var nextArcGISPoint = arcGISMapComponent.View.WorldToGeographic(movDir + cameraPosition);

				if (nextArcGISPoint.Z > MaxCameraHeight)
				{
					var point = new ArcGISPoint(nextArcGISPoint.X, nextArcGISPoint.Y, MaxCameraHeight, nextArcGISPoint.SpatialReference);
					cameraPosition = arcGISMapComponent.View.GeographicToWorld(point);
				}
				else if (nextArcGISPoint.Z < MinCameraHeight)
				{
					var point = new ArcGISPoint(nextArcGISPoint.X, nextArcGISPoint.Y, MinCameraHeight, nextArcGISPoint.SpatialReference);
					cameraPosition = arcGISMapComponent.View.GeographicToWorld(point);
				}
				else
				{
					cameraPosition += movDir * distance;
				}

				var newENUReference = arcGISMapComponent.View.GetENUReference(cameraPosition);
				var oldENUReference = arcGISMapComponent.View.GetENUReference(Position);

				cameraRotation = math.mul(math.inverse(oldENUReference.GetRotation()), cameraRotation);
				cameraRotation = math.mul(newENUReference.GetRotation(), cameraRotation);
			}
			else
			{
				cameraPosition += movDir * distance;
			}

			Position = cameraPosition;
			Rotation = cameraRotation;
		}

		void OnTransformParentChanged()
		{
			OnEnable();
		}

		private void DragMouseEvent()
		{
			var cartesianPosition = Position;
			var cartesianRotation = Rotation;

			var deltaMouse = GetMousePosition() - lastMouseScreenPosition;

			if (!firstOnFocus)
			{
				if (IsMouseRightClicked())
				{
					if (deltaMouse != Vector3.zero)
					{
						if (arcGISMapComponent.MapType == GameEngine.Map.ArcGISMapType.Global)
						{
							GlobalDragging(ref cartesianPosition, ref cartesianRotation);
						}
						else if (arcGISMapComponent.MapType == GameEngine.Map.ArcGISMapType.Local)
						{
							LocalDragging(ref cartesianPosition);
						}
					}
				}
				else
				{
					firstDragStep = true;
				}
			}
			else
			{
				firstOnFocus = false;
			}

			Position = cartesianPosition;
			Rotation = cartesianRotation;

			lastMouseScreenPosition = GetMousePosition();
		}

		private void LocalDragging(ref double3 cartesianPosition)
		{
			var worldRayDir = GetMouseRayCastDirection();
			var isIntersected = Geometry.RayPlaneIntersection(cartesianPosition, worldRayDir, double3.zero, math.up(), out var intersection);

			if (isIntersected && intersection >= 0)
			{
				double3 cartesianCoord = cartesianPosition + worldRayDir * intersection;

				var delta = firstDragStep ? double3.zero : lastCartesianPoint - cartesianCoord;

				lastCartesianPoint = cartesianCoord + delta;
				cartesianPosition += delta;
				firstDragStep = false;
			}
		}

		private void GlobalDragging(ref double3 cartesianPosition, ref quaternion cartesianRotation)
		{
			var spheroidData = arcGISMapComponent.View.SpatialReference.SpheroidData;
			var worldRayDir = GetMouseRayCastDirection();
			var isIntersected = Geometry.RayEllipsoidIntersection(spheroidData, cartesianPosition, worldRayDir, 0, out var intersection);

			if (isIntersected && intersection >= 0)
			{
				var oldENUReference = arcGISMapComponent.View.GetENUReference(cartesianPosition);

				var geoPosition = arcGISMapComponent.View.WorldToGeographic(cartesianPosition);

				double3 cartesianCoord = cartesianPosition + worldRayDir * intersection;
				var currentGeoPosition = arcGISMapComponent.View.WorldToGeographic(cartesianCoord);

				var visibleHemisphereDir = math.normalize(arcGISMapComponent.View.GeographicToWorld(new ArcGISPoint(geoPosition.X, 0, 0, geoPosition.SpatialReference)));

				double dotVC = math.dot(cartesianCoord, visibleHemisphereDir);
				lastDotVC = firstDragStep ? dotVC : lastDotVC;

				double deltaX = firstDragStep ? 0 : lastArcGISPoint.X - currentGeoPosition.X;
				double deltaY = firstDragStep ? 0 : lastArcGISPoint.Y - currentGeoPosition.Y;

				deltaY = Math.Sign(dotVC) != Math.Sign(lastDotVC) ? 0 : deltaY;


				lastArcGISPoint = new ArcGISPoint(currentGeoPosition.X + deltaX, currentGeoPosition.Y + deltaY, lastArcGISPoint.Z, lastArcGISPoint.SpatialReference);


				var YVal = geoPosition.Y + (dotVC <= 0 ? -deltaY : deltaY);
				YVal = Math.Abs(YVal) < MaxCameraLatitude ? YVal : (YVal > 0 ? MaxCameraLatitude : -MaxCameraLatitude);

				geoPosition = new ArcGISPoint(geoPosition.X + deltaX, YVal, geoPosition.Z, geoPosition.SpatialReference);

				cartesianPosition = arcGISMapComponent.View.GeographicToWorld(geoPosition);

				var newENUReference = arcGISMapComponent.View.GetENUReference(cartesianPosition);
				cartesianRotation = math.mul(math.inverse(oldENUReference.GetRotation()), cartesianRotation);
				cartesianRotation = math.mul(newENUReference.GetRotation(), cartesianRotation);

				firstDragStep = false;
				lastDotVC = dotVC;
			}
		}


		private double3 GetMouseRayCastDirection()
		{
			var forward = hpTransform.Forward.ToDouble3();
			var right = hpTransform.Right.ToDouble3();
			var up = hpTransform.Up.ToDouble3();

			var camera = gameObject.GetComponent<Camera>();

			var view = new double4x4
			(
				math.double4(right, 0),
				math.double4(up, 0),
				math.double4(forward, 0),
				math.double4(double3.zero, 1)
			);

			var proj = camera.projectionMatrix.inverse.ToDouble4x4();

			proj.c2.w *= -1;
			proj.c3.z *= -1;

			var MousePosition = GetMousePosition();
			double3 ndcCoord = new double3(2.0 * (MousePosition.x / Screen.width) - 1.0, 2.0 * (MousePosition.y / Screen.height) - 1.0, 1);
			double3 viewRayDir = math.normalize(proj.HomogeneousTransformPoint(ndcCoord));
			return view.HomogeneousTransformVector(viewRayDir);
		}

		private void FocusChanged(bool isFocus)
		{
			firstOnFocus = true;
		}

		private void UpdateSpeed(double height)
		{
			var msMaxSpeed = (MaxSpeed * 1000) / 3600;
			var msMinSpeed = (MinSpeed * 1000) / 3600;
			TranslationSpeed = (float)(Math.Pow(Math.Min((height / 100000.0), 1), 2.0) * (msMaxSpeed - msMinSpeed) + msMinSpeed);
		}

		#region Properties
		/// <summary>
		/// Get/set the camera position in world coordinates
		/// </summary>
		private double3 Position
		{
			get
			{
				return hpTransform.UniversePosition;
			}
			set
			{
				hpTransform.UniversePosition = value;
			}
		}

		/// <summary>
		/// Get/set the camera rotation
		/// </summary>
		private quaternion Rotation
		{
			get
			{
				return hpTransform.UniverseRotation;
			}
			set
			{
				hpTransform.UniverseRotation = value;
			}
		}

		#endregion
	}
}
