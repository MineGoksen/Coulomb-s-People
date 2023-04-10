using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
namespace YoutubePlayer
{
    public class VideoControl : MonoBehaviour
    {
        public YoutubePlayer youtubePlayer;
        VideoPlayer videoPlayer;
        public Button start_btn;
        public TextMeshProUGUI loading;
        public TextMeshProUGUI countdown;
        private int saniye = 20;

        private void Awake()
        {
            start_btn.interactable = true;
            videoPlayer = youtubePlayer.GetComponent<VideoPlayer>();

        }
        void Start()
        {
            loading.enabled = false;
        }


        public async void Prepare()
        {   
            //start_btn.SetActive(false);
            print("video hazirlaniyor..");
            loading.enabled = true;
            try
            {
                await youtubePlayer.PrepareVideoAsync();
                print("video hazir");
            }
            catch
            {
                print("hata");
            }
            loading.enabled = false;
            videoPlayer.Play();
            StartCoroutine(time());
        }
        public void guess()
        {
            SceneManager.LoadScene("New_map");
        }

        
        IEnumerator time()
        {
            while (saniye > 0)
            {
                countdown.text = saniye.ToString();
                saniye -= 1;
                yield return new WaitForSeconds(1);
            }
            SceneManager.LoadScene("New_map");
            Debug.Log(StaticGame.startGame());

        }
    }
}
