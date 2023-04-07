using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMove : MonoBehaviour
{
    public static PointerMove instance;
    public float[] coordinates = new float[2];

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        coordinates[0] = 0f;
        coordinates[1] = 0f;
    }

    public void setCoordinates(float lat, float longt)
    {
        coordinates[0] = lat;
        coordinates[1] = longt;
    }
    public float[] getCoordinates()
    {
       
        return coordinates;
    }
    
}
