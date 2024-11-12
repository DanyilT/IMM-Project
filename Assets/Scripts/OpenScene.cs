using UnityEngine;

public class OpenScene : MonoBehaviour
{
    [SerializeField] private string sceneName; // Or use [SerializeField] private int sceneIndex;

    // Open the scene by name
    public void OpenSceneByName()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
