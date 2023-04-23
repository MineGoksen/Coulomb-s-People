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
    public GameObject coulombspeople;
    public GameObject howToPlay_popup;
    public GameObject globalchart_popup;
    public TextMeshProUGUI email;
    public TextMeshProUGUI username;
    public TextMeshProUGUI puan;
    private string userId;
    public VideoPlayer MyVideoPlayer;
    void Start()
    {
        image_popup.SetActive(false);
        howToPlay_popup.SetActive(false);
        coulombspeople.SetActive(true);
        globalchart_popup.SetActive(false);
        MyVideoPlayer.Play();
        userId=PlayerPrefs.GetString("userId");
        if(userId==null ||userId==""){
             SceneManager.LoadScene("SignInUp");
        }
        Debug.Log(userId);
        getUserData();
        getChartData();
        
    }

    private void getUserData(){
        Client <UserData> cli =new Client <UserData> ();
        UserData[] user=cli.httpGet("playerApi/"+userId);
        if(user.Length!=0){
            email.text=user[0].e_mail;
            username.text=user[0].nickname;
            puan.text=""+user[0].point;
            }
    }

    public TextMeshProUGUI first_player;
    public TextMeshProUGUI sec_player;
    public TextMeshProUGUI third_player;
    public TextMeshProUGUI user_ranking;
    private void getChartData(){
        Client <UserData> cli_chart =new Client <UserData> ();
        UserData[] user=cli_chart.httpGet("topList");
        first_player.text=user[0].nickname+":\t"+user[0].point;
        sec_player.text=user[1].nickname+":\t"+user[1].point;
        third_player.text=user[2].nickname+":\t"+user[2].point;

        Client <ChartUserData> cli =new Client <ChartUserData> ();
        ChartUserData[] chartUser=cli.httpGet("topList/"+userId);
        user_ranking.text=""+(chartUser[0].index+1)+".\t "+chartUser[0].point+" point";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void HidePopUp()
    {
        // Hide the pop-up when the button is clicked
        image_popup.SetActive(false);
        coulombspeople.SetActive(true);
    }
    public void ShowPopUp()
    {
        // Show the pop-up when the button is clicked
        image_popup.SetActive(true);
        coulombspeople.SetActive(false);
    }
    public void logOut(){
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("SignInUp");
    }

    public void HideChartPopUp()
    {
        // Hide the pop-up when the button is clicked
        globalchart_popup.SetActive(false);
        coulombspeople.SetActive(true);
    }
    public void ShowChartPopUp()
    {
        // Show the pop-up when the button is clicked
        globalchart_popup.SetActive(true);
        coulombspeople.SetActive(false);
    }

     public void startGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void HideHowToPlayPopUp()
    {
        // Hide the pop-up when the button is clicked
        
        howToPlay_popup.SetActive(false);
        coulombspeople.SetActive(true);
    }
    public void ShowHowToPlayPopUp()
    {
        // Show the pop-up when the button is clicked
        howToPlay_popup.SetActive(true);
        coulombspeople.SetActive(false);


    }
  
}