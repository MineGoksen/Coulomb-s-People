using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClosingSceneScript : MonoBehaviour
{
    public TextMeshProUGUI resultsText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(resultsText.text == " ")
        resultsText.text = StaticGame.showResults();
    }

    public void restartButton()
    {
        StaticGame.restart();
    }
}
