using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;


public class PopUpController : MonoBehaviour
{
    private string URL =StaticGame.URL+"tipApi";
    public GameObject image_tip;
    public TextMeshProUGUI tip;
    string t = "tip";

    private void Start()
    {
        // Hide the pop-up when the scene starts
        image_tip.SetActive(false);
        tip.text = t;
        StartCoroutine(GetRequest(URL));


    }

    public void ShowPopUp()
    {
        // Show the pop-up when the button is clicked
        image_tip.SetActive(true);
        
    }

    public void HidePopUp()
    {
        // Hide the pop-up when the button is clicked
        image_tip.SetActive(false);
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
                    Tip[] tips = JsonConvert.DeserializeObject<Tip[]>(webRequest.downloadHandler.text);
                    Debug.Log(tips[0].tip);
                    tip.text = tips[0].tip;
                    break;
            }
        }
    }
}
