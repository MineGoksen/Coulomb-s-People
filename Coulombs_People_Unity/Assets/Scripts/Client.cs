using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Net.Http.Headers;

public class Client<T> {
      
    // using properties
    public T[] data
    {
        // using accessors
        get
        {
            return this.data;
        }
        set
        {
            this.data = value;
        }
    }
    public T[] httpGet(string path){
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(StaticGame.URL);
        Debug.Log(StaticGame.URL);
            // Add an Accept header for JSON format.
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.
        HttpResponseMessage response = client.GetAsync(path).Result;  // Blocking call!
        if (response.IsSuccessStatusCode)
        {
            var products = response.Content.ReadAsStringAsync().Result;
            //Debug.Log(products);
            T[] data = JsonConvert.DeserializeObject<T[]>(products);
            return data;
        }
        else
        {
            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        }
        return null;
    
    }
}
  
