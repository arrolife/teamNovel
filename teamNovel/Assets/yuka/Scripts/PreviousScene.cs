using UnityEngine;
using UnityEngine.SceneManagement;

public class PreviousScene : MonoBehaviour
{
    private static string previousScene;

    // シーンが切り替わる前に呼び出す
    public static void SavePreviousScene()
    {
        previousScene = SceneManager.GetActiveScene().name;
    }

    // 前のシーンに戻る
    public void ReturnPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("戻るシーンが保存されていません");
        }
    }
}