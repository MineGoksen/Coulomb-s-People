using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ClosingSceneScript : MonoBehaviour
{

    public TextMeshProUGUI resultsText;
    private string userId;
    // Start is called before the first frame update

    void Start()
    {   userId=PlayerPrefs.GetString("userId");
        //if(userId==null ||userId==""){
        //     SceneManager.LoadScene("SignInUp");
        //}
        if(resultsText.text == " ")
        resultsText.text = StaticGame.showResults();
        Client <Object> cli =new Client <Object> ();
        Debug.Log("add_Point/"+userId+"/"+(int)StaticGame.total);
        cli.httpPut("add_Point/"+userId+"/"+(int)StaticGame.total);
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartButton()
    {
        StaticGame.restart();
    }


}
