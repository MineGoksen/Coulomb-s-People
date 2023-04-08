using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class ButtonInteractions : MonoBehaviour
{
private TouchScreenKeyboard overlayKeyboard;
    public int counter = 0;

    public TextMeshProUGUI simpleUIText;

    public void SignInButtonClicked()
    {
        counter++;
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        simpleUIText.text = "Sign Ip Clicked: " + counter;
    }

    public void SignUpButtonClicked()
    {
        counter++;
        simpleUIText.text = "Sign Up Clicked: " + counter;
    }

    public void AnswerA_Clicked()
    {
        counter++;
        simpleUIText.text = "AnswerA: " + counter;
    }

    public void AnswerB_Clicked()
    {
        counter++;
        simpleUIText.text = "AnswerB: " + counter;
    }

    public void AnswerC_Clicked()
    {
        counter++;
        simpleUIText.text = "AnswerC: " + counter;
    }

    public void AnswerD_Clicked()
    {
        counter++;
        simpleUIText.text = "AnswerD: " + counter;
    }




}
