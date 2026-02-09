using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void HandleSceneChange()
    {
        SceneManager.LoadScene(sceneName);
    }
}
