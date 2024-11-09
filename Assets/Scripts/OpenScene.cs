using UnityEngine;

public class OpenScene : MonoBehaviour
{
    [SerializeField] private string sceneName; // Or use [SerializeField] private int sceneIndex;

    public void OpenSceneByName()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
