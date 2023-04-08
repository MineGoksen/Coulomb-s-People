using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;

public class API : MonoBehaviour
{
    private const string URL = "http://192.168.208.26:5000/questionsApi";
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
                    QuestionData[] data = JsonConvert.DeserializeObject<QuestionData[]>(webRequest.downloadHandler.text);
                    Debug.Log(data[0].question);
                    question.text=data[0].question;
                    a.text= data[0].choice_a;
                    b.text = data[0].choice_b;
                    c.text = data[0].choice_c;
                    d.text = data[0].choice_d;
                    break;
            }
        }
    }



}
