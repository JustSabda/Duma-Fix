using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoScript : MonoBehaviour
{
    VideoPlayer _videoPlayer;
    void Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _videoPlayer.loopPointReached += NextScene;
    }

    void NextScene(VideoPlayer vp)
    {
        if (SceneManager.GetActiveScene().name == ("Cutscene Prolog"))
        {
            SceneLoad.Instance.NextLevel();
        }
        else
        {
            SceneLoad.Instance.BackMainMenu();
        }
    }
}
