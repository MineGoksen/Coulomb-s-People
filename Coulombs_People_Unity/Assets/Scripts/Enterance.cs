using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enterance : MonoBehaviour
{
    public GameObject image_popup;
    public string email;
    public string username;
    void Start()
    {
        image_popup.SetActive(false);
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
    public void toVideo()
    {
        SceneManager.LoadScene("Video");
    }
}
