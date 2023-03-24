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
        videoPlayer.url = "https://rr3---sn-ab5l6nkd.googlevideo.com/videoplayback?expire=1679509060&ei=5PEaZPq_Mf2W_9EPydOnwAM&ip=91.229.104.28&id=o-APDWxM1Kk6DwkOJI924baYdn6G5jBzlLOE5CFAXKOEO9&itag=18&source=youtube&requiressl=yes&spc=H3gIhhd5TjjG-YhehrCtNUkW7W90K-M&vprv=1&svpuc=1&mime=video%2Fmp4&gir=yes&clen=44439902&ratebypass=yes&dur=687.310&lmt=1679036121411626&fexp=24007246&c=ANDROID&txp=5538434&sparams=expire%2Cei%2Cip%2Cid%2Citag%2Csource%2Crequiressl%2Cspc%2Cvprv%2Csvpuc%2Cmime%2Cgir%2Cclen%2Cratebypass%2Cdur%2Clmt&sig=AOq0QJ8wRAIgIS7LcoGkUBx9UOyGxd2ugh3JsSSxlcAm8-jwLFk3V20CIBjCLDI9GnW5oh7hOfDu7JMdBMxon7PY3rUgJzSir5De&title=Konu%C5%9Fanlar+97.+B%C3%B6l%C3%BCm+Shot&redirect_counter=1&rm=sn-gjo-w43s7e&req_id=4c5e42b596c2a3ee&cms_redirect=yes&cmsv=e&ipbypass=yes&mh=SC&mm=29&mn=sn-ab5l6nkd&ms=rdu&mt=1679487199&mv=m&mvi=3&pl=23&lsparams=ipbypass,mh,mm,mn,ms,mv,mvi,pl&lsig=AG3C_xAwRgIhAJDMYJB9aclAv7SVkPpeQpanNpMwHmXsUxrS-ssgAqg1AiEA-I59CXfaHIetqOM5DdSEUJDU1FYKduAEPkKaKa_04kc%3D";
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