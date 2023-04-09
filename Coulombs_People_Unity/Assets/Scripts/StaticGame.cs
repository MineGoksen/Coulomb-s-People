using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]

public  class StaticGame 
{
    // Start is called before the first frame update
    public static double score=0;
    public static int round=0;
    public static float[] videoCoordinates=new float[2];
    public static string [] roundCountry=new string[10];
   
    public static void CalculateScore(double lat, double lon)
    {
        double distance = HaversineFormula(videoCoordinates[0], videoCoordinates[1], lat, lon);
        score += distance;//1/distance yapýp 1 ile 100 arasý normalize edelim 
    }
    static double HaversineFormula(double lat1, double lon1, double lat2, double lon2)
    {
        double R = 6371; // Earth's radius in kilometers
        double dLat = ToRadians(lat2 - lat1);
        double dLon = ToRadians(lon2 - lon1);
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double tmp = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = R * tmp;
        return distance;
    }

    static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }
    public static void questionScoreAdd(bool answer)
    {
        if (answer)
        {
            score += 10;//bundan emin deðilim sadece distance ile aldýðýmýz deðeri 2 katýna da çýkarabiliriz doðru ise diye düþündüm 
        }
        round += 1;
    }
    static public string startGame()
    {
        score = 0;
        round = 0;
        videoCoordinates = new float[2];
        roundCountry = new string[10];
        return "worked";
    }



}
