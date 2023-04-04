using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class SignUp : MonoBehaviour
{
    // Start is called before the first frame update
    public string email;
    public string password;
    public string username;
    public Button button;
    private static Firebase.Auth.FirebaseAuth auth;
    public GameObject image_popup;
    public TextMeshProUGUI popup;
    [SerializeField]
    string text;
    void Start()
    {
        popup.text = text;
        image_popup.SetActive(false);
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    private void Update()
    {
        popup.text = text;
    }
        public void readUsername(string s)
    {
        username = s;
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
                text = "CreateUserWithEmailAndPasswordAsync was canceled.";
                //Debug.Log(image_popup);
                
                return;
            }
            if (task.IsFaulted)
            {
                //Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                string exception = task.Exception.ToString();
                //Popup.Show(exception);
                text = "CreateUserWithEmailAndPasswordAsync encountered an error ";
                //Debug.Log(image_popup);
                return;
            }
            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            text = "Firebase user created successfully";
            

        });
    }
    public void ShowPopUp()
    {
        popup.text = text;
        // Show the pop-up when the button is clicked
        image_popup.SetActive(true);

    }
    public void toSignUp()
    {
        SceneManager.LoadScene("SignIn");
    }

    public void HidePopUp()
    {
        // Hide the pop-up when the button is clicked
        image_popup.SetActive(false);
    }
}
