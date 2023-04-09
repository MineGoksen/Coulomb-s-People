using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    private int saniye=5;
    public TextMeshProUGUI RoundCountDown;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(time());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator time()
    {
        while (saniye>0)
        {
            RoundCountDown.text = saniye.ToString(); ;
            saniye -= 1;
            yield return new WaitForSeconds(1);
        }
        RoundCountDown.text = "Start";
        Debug.Log(StaticGame.startGame());

    }
  
}
