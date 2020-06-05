using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class StartSceneManager : MonoBehaviour
{
    private bool isNickName;

    [Header("Object")]
    [SerializeField]
    private GameObject nickNameSelectCanvas;

    [SerializeField]
    private InputField nickInputField;

    [SerializeField]
    private VideoPlayer videoPlayer;

    private void Awake(){
        if(PlayerPrefs.HasKey("Opening")){
            isNickName = bool.Parse(PlayerPrefs.GetString("Opening"));
        } else {
            isNickName = false;
        }

        if(!isNickName){
            nickNameSelectCanvas.SetActive(true);
        }

        PlayerPrefs.SetString("Opening", isNickName.ToString());
    }

    public void SettingNickName(){
        PlayerPrefs.SetString(nickInputField.text, "NickName");
        nickNameSelectCanvas.gameObject.SetActive(false);
        videoPlayer.Play();
    }

    public void StartGame(){
        SceneManager.LoadScene("01.StageSelectScene");
    }

    public void ResetGame(){
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("00.StartScene");
    }

    public void Exit(){
        Application.Quit();
    }

    private void Update() {
        if(videoPlayer.isPlaying){
            if((videoPlayer.time / videoPlayer.length) > 0.98f){
                videoPlayer.Stop();
                videoPlayer.gameObject.SetActive(false);

                this.enabled = false;
                
                PlayerPrefs.SetString("Opening", "true");
            }
        }
    }
}
