#pragma warning disable CS0649

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UISystem : MonoBehaviour
{
    [SerializeField]
    private Text heartCount;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private GameObject continueStage;
    [SerializeField]
    private Text continueElapsed;
    [SerializeField]
    private GameObject finalStageCleared;
    [SerializeField]
    private Text finalStageElapsed;

    public void StageCleared()
    {
        timer.IsPaused = true;
        DOVirtual.DelayedCall(0.4F, () =>
        {
            if (Stage.AllCleared)
            {
                // 엔딩 보여주기
            }
            else if (Stage.Current == 3)
            {
                finalStageElapsed.text = timer.Elapsed;
                finalStageCleared.SetActive(true);
            }
            else
            {
                continueElapsed.text = timer.Elapsed;
                continueStage.SetActive(true);
            }
        });
    }

    public void LoadNextStage()
    {
        if (Stage.Current == 3)
        {
            throw new Exception("There is no more stage exists.");
        }

        Stage.Current++;
        LoadScene("02.InGameScene");
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void UpdateHeart()
    {
        heartCount.text = Heart.Count.ToString();
    }
}
