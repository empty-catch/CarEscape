using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndingSceneManager : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private VideoPlayer videoPlayer;

    private void Update(){
        var videoProgress = (videoPlayer.time/videoPlayer.length);
        if(videoProgress > 0.95f){
            SceneManager.LoadScene("00.StartScene");
        }
    }

}
