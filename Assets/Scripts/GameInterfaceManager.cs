using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameInterfaceManager : MonoBehaviour
{
    private GameManager gameManager;
    
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI[] playerValueText;
    [SerializeField] private TextMeshProUGUI[] playerBonusValueText;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button[] restartButton;
    [SerializeField] private Button[] exitButton;
    [SerializeField] private int homeSceneIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        pauseButton.onClick.AddListener(PauseGame);
        continueButton.onClick.AddListener(ContinueGame);
        nextLevelButton.onClick.AddListener(NextLevel);
        foreach (Button button in restartButton) button.onClick.AddListener(RestartGame);
        foreach (Button button in exitButton) button.onClick.AddListener(ExitToHome);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerValueAndBonusTextUpdate();

        if (gameManager.isWin)
        {
            ShowWinPanel();
        }
        if (gameManager.isGameOver)
        {
            ShowGameOverPanel();
        }
    }

    private void PlayerValueAndBonusTextUpdate()
    {
        for (int i = 0; i < playerValueText.Length; i++)
        {
            playerValueText[i].text =gameManager.playerCount.ToString();
        }
        for (int i = 0; i < playerBonusValueText.Length; i++)
        {
            playerBonusValueText[i].text = "×" + gameManager.playerBonus.ToString();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        scorePanel.SetActive(false);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        scorePanel.SetActive(true);
    }

    private void NextLevel()
    {
        // Load the next level scene
        // SceneManager.LoadScene("NextLevelSceneName");
        Time.timeScale = 0; // Instead of stopping time -> load the next level scene
        Debug.Log("Level Up!");
    }

    private void RestartGame()
    {
        Time.timeScale = 1;
        gameManager.isGameOver = false;
        gameManager.isWin = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitToHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(homeSceneIndex);
    }

    private void ShowWinPanel()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        scorePanel.SetActive(false);
    }

    private void ShowGameOverPanel()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        scorePanel.SetActive(false);
    }
}
