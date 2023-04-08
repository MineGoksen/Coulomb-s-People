using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase;
using Firebase.Auth;

public class SignIn : MonoBehaviour
{
    private bool succesfull=false;
    public string email="";
    public string password="";
    public Button button;
    string text="UPDATE";
    private static Firebase.Auth.FirebaseAuth auth;
    public GameObject image_popup;
    public TextMeshProUGUI popup;
    Firebase.Auth.FirebaseUser newUser;
    void Start()
    {image_popup.SetActive(false);
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        popup.text = text;
        if (succesfull)
        {
            Debug.Log("here");
            PlayerPrefs.SetString("UserID", newUser.UserId);
            Debug.Log(PlayerPrefs.GetString("UserID"));
            succesfull = false;
            SceneManager.LoadScene("Entrance");
        }
        
    }
    private TouchScreenKeyboard overlayKeyboard;
    public int counter = 0;

    public TextMeshProUGUI simpleUIText;

    public void SignInButtonClicked()
    {
        counter++;
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        //simpleUIText.text = "Sign Ip Clicked: " + counter;
       // StartCoroutine("SignInUser");
        StartCoroutine(LoginAsync());
        image_popup.SetActive(true);
        
    }
    public void ReadEmails(string s)
    {
        email = s;
    }
    public void ReadPassword(string s)
    {
        password = s;
    }

    public void SignInUser()
    {   text="fonskiyona girdi";
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                //Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                simpleUIText.text="firebase'e girdi";
                text = "SignInWithEmailAndPasswordAsync was canceled.";
                return;
            }
            if (task.IsFaulted)
            {
                //Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
               text = "SignInWithEmailAndPasswordAsync encountered an error: ";
                simpleUIText.text="firebase'e girdi";
                return;
            }

            newUser = task.Result;
            simpleUIText.text="firebase'e girdi";
            text = "User signed in successfully ";
            succesfull = true;
            //Debug.LogFormat("User signed in successfully: {0} ({1})",newUser.DisplayName, newUser.UserId);
        });
    }
    

    public void ShowPopUp()
    {
        // Show the pop-up when the button is clicked
        image_popup.SetActive(true);

    }

    public void HidePopUp()
    {
        // Hide the pop-up when the button is clicked
        image_popup.SetActive(false);
    }
    public void toSignIn()
    {
        
    }
    

     private IEnumerator LoginAsync()
    {
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => loginTask.IsCompleted);
        if (loginTask.Exception != null)
        {
            //Debug.LogError(loginTask.Exception);
           simpleUIText.text="firebase'e girdi";
           text = "GİREMEDİ ";
        }
        else
        {   newUser = loginTask.Result;
            simpleUIText.text="firebase'e girdi";
            text = "User signed in successfully ";
            succesfull = true;
        }
    }
}
