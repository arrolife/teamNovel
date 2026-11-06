using UnityEngine;
using UnityEngine.SceneManagement;

public class SEManager : MonoBehaviour
{
    [SerializeField] AudioClip buttonSE;
    [SerializeField] AudioClip changeSceneSE;
    [SerializeField] string targetScene;

    bool isChangingScene = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // シーン読み込み完了時
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isChangingScene = false;
    }

    // 通常のボタンSE
    public void PlayButtonSE()
    {
        if (SoundManager.Instance == null)
        {
            Debug.LogError("SoundManagerが見つかりません");
            return;
        }
        SoundManager.Instance.PlaySe(buttonSE);
    }

    // シーン移動用SE
    public void ChangeSceneWithSE()
    {
        if (isChangingScene) return;

        if (SoundManager.Instance == null || changeSceneSE == null)
        {
            Debug.LogError("SoundManagerが見つかりません");
            return;
        }

        if (changeSceneSE == null)
        {
            Debug.LogError("シーン移動用SEが設定されていません");
            return;
        }

        isChangingScene = true;
        Debug.Log("シーン移動SEを再生");

        SoundManager.Instance.PlaySeAndChangeScene(changeSceneSE, targetScene);
        isChangingScene = false;
    }
}