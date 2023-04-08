using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class API1 : MonoBehaviour
{
    private const string URL = "https://64303261b289b1dec4c37e08.mockapi.io/api/user";
    public TextMeshProUGUI question;
    public TextMeshProUGUI a;
    public TextMeshProUGUI b;
    public TextMeshProUGUI c;
    public TextMeshProUGUI d;

    void Start()
    {
        StartCoroutine(GetRequest(URL));
        Debug.Log("the request is sent");
        

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
                    question.text=webRequest.downloadHandler.text;
                
                    break;
            }
        }
    }



}
