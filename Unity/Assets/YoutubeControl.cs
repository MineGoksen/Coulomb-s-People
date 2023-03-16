
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
namespace YoutubePlayer
{

    public class YoutubeControl : MonoBehaviour
    {
        public YoutubePlayer youtubePlayer;
        VideoPlayer videoPlayer;
        public Button bt_play;
        public Button bt_Pause;
        public Button bt_Reset;

        private void Awake()
        {
            bt_play.interactable = false;
            bt_Pause.interactable = false;
            bt_Reset.interactable = false;
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
            print("Video Isleniyor");
            try
            {
                await youtubePlayer.PrepareVideoAsync();
                print("Video hazýr.");
            }
            catch
            {
                print("hata");
            }
        }

        public void PlayVideo()
        {
            videoPlayer.Play();
        }
        public void PauseVideo()
        {
            videoPlayer.Pause();
        }
        public void ResetVideo()
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }
        void OnDestroy()
        {
            videoPlayer.prepareCompleted -= VideoPlayerPreparedCompleted;
        }
    }
}
