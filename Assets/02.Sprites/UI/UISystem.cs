using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UISystem : MonoBehaviour
{
    public void StageCleared()
    {
        PlayerPrefs.SetInt($"Stage{Stage.Current}Clear", 1);
        DOVirtual.DelayedCall(0.2F, () => SceneManager.LoadScene("01.StageSelectScene"));
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
