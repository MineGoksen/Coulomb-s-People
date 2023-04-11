using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Authetication : MonoBehaviour
{
    public TextMeshProUGUI code;
     private string URL = "http://192.168.208.26:5000/code/";
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(100000,999999);
        Debug.Log(num);
        code.text=""+num;
        URL+=(""+num);
        
    }

    // Update is called once per frame
    void Update()
    {  
        StartCoroutine(GetRequest(URL));
    }

     IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    AuthData[] data = JsonConvert.DeserializeObject<AuthData[]>(webRequest.downloadHandler.text);
                    if(data.Length!=0){
                        Debug.Log("sec"+data[0].id);
                        PlayerPrefs.SetString("userId",data[0].id);
                        code.text="Giriş Yapıldı.";
                        StartCoroutine(time());
                    }
                    break;
            }
        }   
    }

     IEnumerator time()
    {   int saniye =5;
        while (saniye>0)
        {       
            saniye -= 1;
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Entrance");
    }
}
