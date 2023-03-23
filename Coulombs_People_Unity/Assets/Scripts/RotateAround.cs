using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform parent;
    public float speed = 10f;

    void Update()
    {
        transform.RotateAround(parent.position, Vector3.down, speed * Time.deltaTime);
    }
}