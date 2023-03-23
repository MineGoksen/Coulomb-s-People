using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SignUp : MonoBehaviour
{
    // Start is called before the first frame update
    public string email;
    public string password;
    public Button button;
    private static Firebase.Auth.FirebaseAuth auth;
   
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void readEmail(string s)
    {
        email = s;
    }
    public void readPassword(string s)
    {
        password = s;
    }
    public void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                //Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                //Popup.Show("Some error has occured");
                return;
            }
            if (task.IsFaulted)
            {
                //Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                string exception = task.Exception.ToString();
                //Popup.Show(exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            //Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                //newUser.DisplayName, newUser.UserId);
        });
    }
}
