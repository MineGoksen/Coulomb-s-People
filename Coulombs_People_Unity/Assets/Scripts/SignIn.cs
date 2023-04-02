using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignIn : MonoBehaviour
{
    public string email;
    public string password;
    public Button button;    
    private static Firebase.Auth.FirebaseAuth auth;
<<<<<<< Updated upstream

    public Button popUp;
    public GameObject image_tip;
    public TextMeshProUGUI tip;
    string t = "eror";

=======
    public GameObject image_popup;
    public TextMeshProUGUI popup;
>>>>>>> Stashed changes
    void Start()
    {
        image_tip.SetActive(false);
        tip.text = t;
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadEmails(string s)
    {
        email = s;
    }
    public void ReadPassword(string s)
    {
        password = s;
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

    public void SignInUser()
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                //Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
<<<<<<< Updated upstream
                tip.text="user information is incorrect";
=======
                popup.text = "SignInWithEmailAndPasswordAsync was canceled.";
>>>>>>> Stashed changes
                ShowPopUp();
                return;
            }
            if (task.IsFaulted)
            {
                //Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                popup.text = "SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception;
                ShowPopUp();
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            popup.text = "User signed in successfully ";
            ShowPopUp();
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
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
}