using UnityEngine;
using UnityEngine.SceneManagement;
//Scene‘JˆÚ
public class SceneChange : MonoBehaviour
{
    [SerializeField] private string Scene;

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(Scene))
        {
            PreviousScene.SavePreviousScene();
            SceneManager.LoadScene(Scene);
        }
        else
        {
            Debug.LogWarning("Scene‚ªİ’è‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
        }
    }
}
