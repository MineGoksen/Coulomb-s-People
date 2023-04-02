using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.enabled = true;
        videoPlayer.url = "https://rr5---sn-ab5l6nrl.googlevideo.com/videoplayback?expire=1680459669&ei=NXMpZL_dOsXTgwPLtpDABw&ip=91.229.104.141&id=o-AN0ij-OsZgqXR0rOQgI0p5cpYujZaW2mjRxoz6cd69Qc&itag=22&source=youtube&requiressl=yes&pcm2=yes&spc=99c5Cf4qsGoYq-h0R3IVttmqCDqDcTk&vprv=1&svpuc=1&mime=video%2Fmp4&cnr=14&ratebypass=yes&dur=891.228&lmt=1668306644343299&fexp=24007246&c=ANDROID&txp=4532434&sparams=expire%2Cei%2Cip%2Cid%2Citag%2Csource%2Crequiressl%2Cpcm2%2Cspc%2Cvprv%2Csvpuc%2Cmime%2Ccnr%2Cratebypass%2Cdur%2Clmt&sig=AOq0QJ8wRAIgJAHkEPDuEprvreAfSwDibO4vpPw8oxsKU5w5Skx1AWgCIEqG_ePNw86i-JhkGDXWknM6a9-Dt0io0xPmDARtu3QX&title=Togg%E2%80%99u+CEO%E2%80%99suyla+Birlikte+Kulland%C4%B1k+|+0-100+Yapt%C4%B1k&redirect_counter=1&rm=sn-gjo-w43s7e&req_id=d5ba0bc3b55fa3ee&cms_redirect=yes&cmsv=e&ipbypass=yes&mh=K3&mm=29&mn=sn-ab5l6nrl&ms=rdu&mt=1680437829&mv=m&mvi=5&pl=23&lsparams=ipbypass,mh,mm,mn,ms,mv,mvi,pl&lsig=AG3C_xAwRgIhAL8ak4GCdJwRt6TCRlAyzp-hb8HSCfLIi0wF1a4VRShrAiEA214D4amLllB60uVPdZpVHQgEarIWikfoB6NZCqvPU_E%3D";
    }
    
    public void Play()
    {

        videoPlayer.Play();
        
    }

    public void Pause()
    {
        videoPlayer.Pause();
    }

}