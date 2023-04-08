using UnityEngine;
using UnityEngine.Video;

public class RandomVideoPlayer : MonoBehaviour
{
    public string[] videoUrls;
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        PlayRandomVideo();
    }

    void PlayRandomVideo()
    {
        int randomIndex = Random.Range(0, videoUrls.Length);
        videoPlayer.url = videoUrls[randomIndex];
        videoPlayer.Play();
    }
}