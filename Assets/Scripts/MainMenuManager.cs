using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private string sceneName = "Main";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instructionsPanel.SetActive(false);
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(OpenSettings);
        closeSettingsButton.onClick.AddListener(CloseSettings);

        instructionsButton.onClick.AddListener(ShowInstructions);
        backButton.onClick.AddListener(BackToMain);
    }

    void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    void BackToMain()
    {
        instructionsPanel.SetActive(false);
    }
}
