#pragma warning disable CS0649

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UISystem : MonoBehaviour
{
    [SerializeField]
    private Text heartCount;

    public void StageCleared()
    {
        PlayerPrefs.SetInt($"Stage{Stage.Current}Clear", 1);
        DOVirtual.DelayedCall(0.2F, () => SceneManager.LoadScene("01.StageSelectScene"));
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
