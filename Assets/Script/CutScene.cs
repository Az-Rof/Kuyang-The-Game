using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnded;
    }

    void OnVideoEnded(VideoPlayer vp)
    {
        Debug.Log("Video ended! Loading next scene...");
        SceneManager.LoadScene("Level 1");
    }

    public void skipIntro()
    {
        Debug.Log("skip intro");
        SceneManager.LoadScene("Level 1");
    }
}