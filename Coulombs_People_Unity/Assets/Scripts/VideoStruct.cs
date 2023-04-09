using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoStruct
{
    public string URL = "";
    public string country = "";
    public double lat;
    public double lon;
    public void fill(string url,string country, double lat, double lon)
    {
        this.URL = url;
        this.country = country;
        this.lat = lat;
        this.lon = lon;
    }
   
}
