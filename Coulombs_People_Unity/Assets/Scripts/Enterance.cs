using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.SceneManagement;

public class Enterance : MonoBehaviour
{
    public GameObject image_popup;
    private string URL = StaticGame.URL+"playerApi/";
    public TextMeshProUGUI email;
    public TextMeshProUGUI username;
    private TextMeshProUGUI puan;
    private string userId;
    void Start()
    {
        image_popup.SetActive(false);
        userId=PlayerPrefs.GetString("userId");
        Debug.Log(userId);
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
    public void startGame()
    {
        SceneManager.LoadScene("Game");
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
                    UserData[] data = JsonConvert.DeserializeObject<UserData[]>(webRequest.downloadHandler.text);
                    if(data.Length!=0){
                        email.text=data[0].e_mail;
                        username.text=data[0].nickname;
                        //puan.text=data[0];
                    }
                    
                    break;
            }
        }
    }
}
