using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
namespace YoutubePlayer
{
    public class VideoControl : MonoBehaviour
    {
        public YoutubePlayer youtubePlayer;
        VideoPlayer videoPlayer;
        public Button bt_play;
        public Button bt_Pause;
        public Button bt_Reset;


        private void Awake()
        {
            bt_play.interactable = true;
            bt_Pause.interactable = true;
            bt_Reset.interactable = true;
            videoPlayer = youtubePlayer.GetComponent<VideoPlayer>();
            videoPlayer.prepareCompleted += VideoPlayerPreparedCompleted;


        }



        void VideoPlayerPreparedCompleted(VideoPlayer source)
        {
            bt_play.interactable = source.isPrepared;
            bt_Pause.interactable = source.isPrepared;
            bt_Reset.interactable = source.isPrepared;

        }

        public async void Prepare()
        {
            print("video hazirlaniyor..");
            try
            {
                await youtubePlayer.PrepareVideoAsync();
                print("video hazir");
            }
            catch
            {
                print("hata");
            }
            videoPlayer.Play();
        }

        public void PlayVideo()
        {
            videoPlayer.Play();
        }


        public void PauseVideo()
        {
            videoPlayer.Pause();
            print("Pause");
        }
        public void ResetVideo()
        {
            videoPlayer.Stop();
            videoPlayer.Play();
            print("ResetVideo");
        }

        void OnDestroy()
        {
            videoPlayer.prepareCompleted -= VideoPlayerPreparedCompleted;
        }
    }
}

