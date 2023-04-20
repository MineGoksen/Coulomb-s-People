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
    public GameObject button_popup;
    public TextMeshProUGUI url;
    public TextMeshProUGUI constant;
    private string URL ;
    
    // Start is called before the first frame update
    void Start()
    {
        string userId=PlayerPrefs.GetString("userId");
        if(userId!=null &&userId!=""){
             SceneManager.LoadScene("Entrance");
        }
        url.text=StaticGame.URL+"/sign_in";
        button_popup.SetActive(false);
        StartCoroutine(time());
        CreateRandNum();
      
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
                        constant.text="";
                        url.text="";
                        button_popup.SetActive(false);
                        Debug.Log("sec"+data[0].id);
                        PlayerPrefs.SetString("userId",data[0].id);
                        code.text="Giriş Yapıldı.";
                        StartCoroutine(timeToEnter());
                    }
                    break;
            }
        }   
    }

     IEnumerator timeToEnter()
    {   int saniye =5;
        while (saniye>0)
        {       
            saniye -= 1;
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Entrance");
    }

     IEnumerator time()
    {
        int saniye =15;
        while (saniye>0)
        {       
            saniye -= 1;
            yield return new WaitForSeconds(1);
        }
        button_popup.SetActive(true);
    }

    public void CreateRandNum(){
        System.Random rnd = new System.Random();
        int num = rnd.Next(100000,999999);
        Debug.Log(num);
        code.text=""+num;
        URL=StaticGame.URL+"code/"+num;
        
    }
}
