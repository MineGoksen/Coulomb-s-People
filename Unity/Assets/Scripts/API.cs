using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;


public class API : MonoBehaviour
{
    private const string URL = "http://127.0.0.1:5000/questionsApi";
    public TextMeshProUGUI question;
    public TextMeshProUGUI a;
    public TextMeshProUGUI b;
    public TextMeshProUGUI c;
    public TextMeshProUGUI d;
    private bool a_ans=false;
    private bool b_ans=false;
    private bool c_ans=false;
    private bool d_ans=false;

    void Start()
    {
        StartCoroutine(GetRequest(URL));
        Debug.Log("the request is sent");
        Debug.Log("the request is sent2");

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
                    question.text=data[0].question;
                    a.text= data[0].choice_a;
                    b.text = data[0].choice_b;
                    c.text = data[0].choice_c;
                    d.text = data[0].choice_d;        

                    switch (data[0].right_answer){
                        case "a":
                            a_ans=true;
                            break;

                        case "b":
                            b_ans=true;
                            break;
                        case "c":
                            c_ans=true;
                            break;
                        case "d":
                            d_ans=true;
                            break;
                    }
                    break;
            }
        }
    }

    public void click_a(){
    if(a_ans)
    Debug.Log("Dogru cevapladiniz");
    else
    Debug.Log("Yanlis cevap");

    }
    public void click_b(){   
        if(b_ans)
        Debug.Log("Dogru cevapladiniz");
        else
        Debug.Log("Yanlis cevap");

    }
    public void click_c(){
       if(c_ans)
        Debug.Log("Dogru cevapladiniz");
        else
        Debug.Log("Yanlis cevap");
    }
    public void click_d(){
       if(d_ans)
        Debug.Log("Dogru cevapladiniz");
        else
        Debug.Log("Yanlis cevap");
    }



}