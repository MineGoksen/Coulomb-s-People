using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]

public  class StaticGame 
{
    // Start is called before the first frame update
    public static string URL="http://192.168.1.6:5000/";
    public static double score=0;
    public static int round=0;
    public static float[] videoCoordinates=new float[2];
    public static string [] roundCountry=new string[10];
    public static VideoStruct[] locations = new VideoStruct [2];
    public static bool isStarted = false;

    public static bool hintUsed = false;

    public static float[] guessed_coordinates = new float[2];
   
    public static void CalculateScore()
    {
        double lat = locations[round].lat;
        double lon = locations[round].lon;

        double distance = HaversineFormula(locations[round].lat, locations[round].lon, guessed_coordinates[0], guessed_coordinates[1]);
        Debug.Log(distance);
        if (distance < 100)
            score += 125;
        else if (distance < 500)
            score += 100;
        else if (distance < 750)
            score += 75;
        else if (distance < 1000)
            score += 50;
        else if (distance < 1500)
            score += 25;

        if (hintUsed)
            score -= 25;

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
            score *= 0.25;
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
    public static void fillArray()
    {   if (locations[0] != null)
            return;
        locations[0] = new VideoStruct();
        locations[0].fill("https://www.youtube.com/watch?v=ImRJuBl1z6w&ab_channel=Y%C3%BCr%C3%BCBenimle-WalkwithMe","Turkey", 39.925533, 32.866287);
        locations[1] = new VideoStruct();
        locations[1].fill("https://www.youtube.com/watch?v=uM5BHCWlcio&ab_channel=WalkWithMe%21", "Greece", 37.983810, 23.727539);
        Debug.Log("filled");
        
    }
    public static string returnUrl()
    {
        return locations[round].URL;
    }

    public static string returnCountry()
    {
        return locations[round].country;
    }



}
