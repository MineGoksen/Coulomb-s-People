using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class API : MonoBehaviour
{
    private string URL =StaticGame.URL+"questionsApi/";
    public TextMeshProUGUI question;
    public TextMeshProUGUI a;
    public TextMeshProUGUI b;
    public TextMeshProUGUI c;
    public TextMeshProUGUI d;
    public Button nextRoundBtn;
    private bool isClicked=false;
    private string rightAnswer;


    public Button buton_a ;
    public Button buton_b ;
    public Button buton_c ;
    public Button buton_d ;

    void Start()
    {// string country = StaticGame.returnCountry();
        nextRoundBtn.gameObject.SetActive(false);
        StartCoroutine(GetRequest(URL+"Turkey"));
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
                    rightAnswer = data[0].right_answer;
                    break;
            }
        }

        
    }

    public void RightAnswerCheck()
    {
        switch (rightAnswer)
        {
            case "A":
                buton_a.GetComponent<Image>().color = Color.green;
                buton_b.GetComponent<Image>().color = Color.red;
                buton_c.GetComponent<Image>().color = Color.red;
                buton_d.GetComponent<Image>().color = Color.red;
                break;
            case "B":
                buton_a.GetComponent<Image>().color = Color.red;
                buton_b.GetComponent<Image>().color = Color.green;
                buton_c.GetComponent<Image>().color = Color.red;
                buton_d.GetComponent<Image>().color = Color.red;
                break;
            case "C":
                buton_a.GetComponent<Image>().color = Color.red;
                buton_b.GetComponent<Image>().color = Color.red;
                buton_c.GetComponent<Image>().color = Color.green;
                buton_d.GetComponent<Image>().color = Color.red;
                break;
            case "D":
                buton_a.GetComponent<Image>().color = Color.red;
                buton_b.GetComponent<Image>().color = Color.red;
                buton_c.GetComponent<Image>().color = Color.red;
                buton_d.GetComponent<Image>().color = Color.green;
                break;
        }
        nextRoundBtn.gameObject.SetActive(true);

    }

    public void clickedA()
{
        StaticGame.correctAnswer = true;

}
 public void clickedB()
{
        StaticGame.correctAnswer = true;


    }
    public void clickedC()
{
        StaticGame.correctAnswer = true;


    }
    public void clickedD()
{
        StaticGame.correctAnswer = true;

    }

    public void nextRound(){
        StaticGame.endRound();
}

}
