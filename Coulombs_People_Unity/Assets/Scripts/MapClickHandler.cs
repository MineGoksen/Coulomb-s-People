using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Esri.ArcGISMapsSDK.Components;
using Esri.ArcGISMapsSDK.Samples.Components;
using Esri.ArcGISMapsSDK.Utils.GeoCoord;
using Esri.GameEngine.Extent;
using Esri.GameEngine.Geometry;
using Esri.GameEngine.Map;
using Esri.Unity;
using Unity.Mathematics;
using System;

public class MapClickHandler : MonoBehaviour
{
    public string APIKey = "AAPK4eab47141ee64b1da1f7a0f65696110b6tnB5gZxPWNLsu8wfV9nRkv6QrHJ7CkVNZFPSQQinTRgSoYZCF4LE89zVdwjYNWo";

    // Reference to the Map component in the scene
    public ArcGISMapComponent arcGISMapComponent;

    private void Update()
    {
		/* if (Input.GetMouseButtonDown(0))
         {
             // Get the screen position of the mouse click
             Vector2 screenPoint = Input.mousePosition;

         // Convert the screen position to a location in geographic coordinates
         ArcGISPoint location = Map.EngineToGeographic(screenPoint);

             // Store the location coordinates
             double latitude = location.Y;
             double longitude = location.X;

             // Use the coordinates as needed
             Debug.Log("Latitude: " + latitude + ", Longitude: " + longitude);
         }*/
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				var arcGISRaycastHit = arcGISMapComponent.GetArcGISRaycastHit(hit);
				var layer = arcGISRaycastHit.layer;
				var featureId = arcGISRaycastHit.featureId;

				
					var geoPosition = arcGISMapComponent.EngineToGeographic(hit.point);
	
					Debug.Log("MouseLatitude: " + geoPosition.Y + ", MouseLongitude: " + geoPosition.X);
				
			}
		}
	}
}

