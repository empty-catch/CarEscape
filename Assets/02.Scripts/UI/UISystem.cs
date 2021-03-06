#pragma warning disable CS0649

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UISystem : MonoBehaviour
{
    [SerializeField]
    private Text stageNumber;
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
            if (Stage.Current == 2)
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

    private void Awake()
    {
        stageNumber.text = $"STAGE {(Stage.Current + 1).ToString()}";
    }
}
