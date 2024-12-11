using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private Slider backgroundMusicVolumeSlider;
    [SerializeField] private Toggle backgroundMusicMuteToggle;
    [SerializeField] private Slider uiVolumeSlider;
    [SerializeField] private Toggle uiMuteToggle;
    [SerializeField] private Slider gameEffectVolumeSlider;
    [SerializeField] private Toggle gameEffectMuteToggle;
    [SerializeField] private string sceneName = "Main";

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        instructionsButton.onClick.AddListener(ShowInstructions);
        backButton.onClick.AddListener(BackToMain);
        settingsButton.onClick.AddListener(OpenSettings);
        closeSettingsButton.onClick.AddListener(CloseSettings);

        backgroundMusicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        backgroundMusicMuteToggle.onValueChanged.AddListener(delegate { ToggleMute(backgroundMusicMuteToggle.isOn, "music"); });
        uiVolumeSlider.onValueChanged.AddListener(SetUIVolume);
        uiMuteToggle.onValueChanged.AddListener(delegate { ToggleMute(uiMuteToggle.isOn, "ui"); });
        gameEffectVolumeSlider.onValueChanged.AddListener(SetGameEffectVolume);
        gameEffectMuteToggle.onValueChanged.AddListener(delegate { ToggleMute(gameEffectMuteToggle.isOn, "gameEffect"); });

        backgroundMusicVolumeSlider.value = MusicManager.instance.MusicVolume;
        backgroundMusicMuteToggle.isOn = MusicManager.instance.MusicVolume != 0;
        uiVolumeSlider.value = MusicManager.instance.UIVolume;
        uiMuteToggle.isOn = MusicManager.instance.UIVolume != 0;
        gameEffectVolumeSlider.value = MusicManager.instance.GameEffectVolume;
        gameEffectMuteToggle.isOn = MusicManager.instance.GameEffectVolume != 0;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    void BackToMain()
    {
        instructionsPanel.SetActive(false);
    }

    private void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    private void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    private void SetMusicVolume(float volume)
    {
        MusicManager.instance.SetMusicVolume(volume);
        if (volume > 0 && !backgroundMusicMuteToggle.isOn)
        {
            backgroundMusicMuteToggle.isOn = true;
        }
    }

    private void SetUIVolume(float volume)
    {
        MusicManager.instance.SetUIVolume(volume);
        if (volume > 0 && !uiMuteToggle.isOn)
        {
            uiMuteToggle.isOn = true;
        }
    }

    private void SetGameEffectVolume(float volume)
    {
        MusicManager.instance.SetGameEffectVolume(volume);
        if (volume > 0 && !gameEffectMuteToggle.isOn)
        {
            gameEffectMuteToggle.isOn = true;
        }
    }

    private void ToggleMute(bool isUnmuted, string category)
    {
        if (category == "music")
        {
            if (isUnmuted)
            {
                MusicManager.instance.SetMusicVolume(backgroundMusicVolumeSlider.value);
            }
            else
            {
                MusicManager.instance.SetMusicVolume(0);
            }
        }
        else if (category == "ui")
        {
            if (isUnmuted)
            {
                MusicManager.instance.SetUIVolume(uiVolumeSlider.value);
            }
            else
            {
                MusicManager.instance.SetUIVolume(0);
            }
        }
        else if (category == "gameEffect")
        {
            if (isUnmuted)
            {
                MusicManager.instance.SetGameEffectVolume(gameEffectVolumeSlider.value);
            }
            else
            {
                MusicManager.instance.SetGameEffectVolume(0);
            }
        }
    }
}
