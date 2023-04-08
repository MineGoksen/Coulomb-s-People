using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SignIn : MonoBehaviour
{
    private bool succesfull = false;
    public string email;
    public string password;
    public Button button;
    private static Firebase.Auth.FirebaseAuth auth;
    string text;
    public GameObject image_popup;
    public TextMeshProUGUI popup;
    Firebase.Auth.FirebaseUser newUser;
    void Start()
    {
        image_popup.SetActive(false);
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
    public void ReadEmails(string s)
    {
        email = s;
    }
    public void ReadPassword(string s)
    {
        password = s;
    }

    public void SignInUser()
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                //Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");

                text = "SignInWithEmailAndPasswordAsync was canceled.";

                return;
            }
            if (task.IsFaulted)
            {
                //Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                text = "SignInWithEmailAndPasswordAsync encountered an error: ";

                return;
            }

            newUser = task.Result;
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
}