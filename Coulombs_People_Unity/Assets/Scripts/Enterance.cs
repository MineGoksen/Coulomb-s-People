using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class Enterance : MonoBehaviour
{
    public GameObject image_popup;
    private const string URL = "http://127.0.0.1:5000/questionsApi";
    public string email;
    public string username;
    void Start()
    {
        image_popup.SetActive(false);
        StartCoroutine(GetRequest(URL));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HidePopUp()
    {
        // Hide the pop-up when the button is clicked
        image_popup.SetActive(false);
    }
    public void ShowPopUp()
    {
        // Show the pop-up when the button is clicked
        image_popup.SetActive(true);

    }
    public void toVideo()
    {
        SceneManager.LoadScene("Video");
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
                    
                    break;
            }
        }
    }
}
