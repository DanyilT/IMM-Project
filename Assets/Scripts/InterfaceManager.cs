using TMPro;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI playerValueText;
    [SerializeField] private TextMeshProUGUI playerBonusValueText;
    [SerializeField] private GameObject gameOverPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver)
        {
            gameOverPanel.gameObject.SetActive(false);
            playerValueText.text = "Player Count:\n" + gameManager.playerCount.ToString();
            playerBonusValueText.text = "Player Bonus:\n×" + gameManager.playerBonus.ToString();
        }
        else
        {
            gameOverPanel.gameObject.SetActive(true);
            playerValueText.gameObject.SetActive(true);
            playerBonusValueText.gameObject.SetActive(false);
        }
    }
}
