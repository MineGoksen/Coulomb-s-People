using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Enterance : MonoBehaviour
{
    public GameObject image_popup;
    private string URL = StaticGame.URL+"playerApi/";
    public TextMeshProUGUI email;
    public TextMeshProUGUI username;
    private TextMeshProUGUI puan;
    private string userId;
    public VideoPlayer MyVideoPlayer;
    void Start()
    {
        image_popup.SetActive(false);
        MyVideoPlayer.Play();
        userId=PlayerPrefs.GetString("userId");
        if(userId==null ||userId==""){
             SceneManager.LoadScene("SignInUp");
        }
        Debug.Log(userId);
        Client <UserData> cli =new Client <UserData> ();
        UserData[] user=cli.httpGet("playerApi/"+userId);
        if(user.Length!=0){
            email.text=user[0].e_mail;
            username.text=user[0].nickname;
            }
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
    public void logOut(){
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("SignInUp");
    }
    public void startGame()
    {
        SceneManager.LoadScene("Game");
    }
  
}
