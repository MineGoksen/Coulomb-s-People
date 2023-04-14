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

    public void AnswerA_Clicked()
    {
 
        Debug.Log("corrds: "+StaticGame.guessed_coordinates[0] + ", " + StaticGame.guessed_coordinates[1]);

    }

    public void AnswerB_Clicked()
    {

        Debug.Log("corrds: "+StaticGame.guessed_coordinates[0]+", "+ StaticGame.guessed_coordinates[1]);
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
