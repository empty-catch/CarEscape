using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectSceneManager : MonoBehaviour
{
    public void GameStart(int index){
        Stage.Current = index;
        SceneManager.LoadScene("02.InGameScene");
    }

    public void GoHome(){
        SceneManager.LoadScene("00.StartScene");
    }
}
