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
        videoPlayer.url = "https://rr2---sn-4g5edndl.googlevideo.com/videoplayback?expire=1680492028&ei=nPEpZIr4Lcqu8wTurquQDg&ip=193.33.66.141&id=o-AFD9AOUKTKP6WxEL4mlXoQGg-1siKRy6nFnWkmyNXwxj&itag=22&source=youtube&requiressl=yes&spc=99c5CZD-LHlzR0fQMBRqLSeNIdruSPg&vprv=1&svpuc=1&mime=video%2Fmp4&cnr=14&ratebypass=yes&dur=923.527&lmt=1660464690623544&fexp=24007246&c=ANDROID&txp=5432434&sparams=expire%2Cei%2Cip%2Cid%2Citag%2Csource%2Crequiressl%2Cspc%2Cvprv%2Csvpuc%2Cmime%2Ccnr%2Cratebypass%2Cdur%2Clmt&sig=AOq0QJ8wRQIgYO8_Vgfvaes11MGgJ52ohpiAnE47BdMTK8xOPCHzHLQCIQDtjfbOaDhw1HIxfJ4mXeZRXzLwkrjAYvVEvI-tEUfbMQ%3D%3D&title=What+can+you+see+in+3+days+in+Istanbul+-Turkey&rm=sn-gjo-w43s7l&req_id=784676656cdfa3ee&ipbypass=yes&redirect_counter=2&cm2rm=sn-ab5eee7z&cms_redirect=yes&cmsv=e&mh=G-&mip=193.140.109.37&mm=34&mn=sn-4g5edndl&ms=ltu&mt=1680468986&mv=u&mvi=2&pl=18&lsparams=ipbypass,mh,mip,mm,mn,ms,mv,mvi,pl&lsig=AG3C_xAwRQIgRKpsakZZOisKZNXwVc4g-oigP4o_dz5Fjf4agoYEQ5wCIQC_hag__7d4E3iUnJNTHTZl9Dz8-eRae2hVO0avBVPkNQ%3D%3D";
    }
    
    public void Play()
    {

        videoPlayer.Play();
        
    }

    public void Pause()
    {
        videoPlayer.Pause();
    }
    public void toSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

}