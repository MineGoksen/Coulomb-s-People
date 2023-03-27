using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ButtonInteractions : MonoBehaviour
{

    public int counter = 0;

    public TextMeshProUGUI simpleUIText;

    public void SignInButtonClicked()
    {
        counter++;
        simpleUIText.text = "Sign Ip Clicked: " + counter;
    }

    public void SignUpButtonClicked()
    {
        counter++;
        simpleUIText.text = "Sign Up Clicked: " + counter;
    }




}
