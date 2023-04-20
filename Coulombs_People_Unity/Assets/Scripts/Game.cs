using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net;
using System.Net.Http;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    private int saniye=4;
    public TextMeshProUGUI RoundCountDown;
    // Start is called before the first frame update
    void Start()
    {   if (!StaticGame.isStarted)
        {
            saniye = 5;
            Client <VideoData> cli =new Client <VideoData> ();
            //StartCoroutine(cli.GetRequest(StaticGame.URL+"get_video"));
            VideoData [] videos=cli.httpGet("get_video");
            StaticGame.fillArray(videos);
        }
        StartCoroutine(time());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator time()
    {
        while (saniye>0)
        {   if(!StaticGame.isStarted)
                RoundCountDown.text = saniye.ToString();
            else
            {
                int round = StaticGame.round + 1;
                RoundCountDown.text = "Round " + (round.ToString());
            }
                
            saniye -= 1;
            yield return new WaitForSeconds(1);
        }
        if (!StaticGame.isStarted)
            RoundCountDown.text = "Start";
        StaticGame.isStarted = true;
        SceneManager.LoadScene("Videos");
    }
  
}
