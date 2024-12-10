using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    //[SerializeField] private GameObject instructionsPanel;
    //[SerializeField] private Button instructionsButton;
    //[SerializeField] private Button backButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private Slider backgroundMusicVolumeSlider;
    [SerializeField] private Toggle backgroundMusicMuteToggle;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle sfxMuteToggle;
    [SerializeField] private string sceneName = "Main";

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        //instructionsButton.onClick.AddListener(ShowInstructions);
        //backButton.onClick.AddListener(BackToMain);
        settingsButton.onClick.AddListener(OpenSettings);
        closeSettingsButton.onClick.AddListener(CloseSettings);

        backgroundMusicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        backgroundMusicMuteToggle.onValueChanged.AddListener(delegate { ToggleMute(backgroundMusicMuteToggle.isOn, "music"); });
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        sfxMuteToggle.onValueChanged.AddListener(delegate { ToggleMute(sfxMuteToggle.isOn, "sfx"); });

        backgroundMusicVolumeSlider.value = MusicManager.instance.MusicVolume;
        backgroundMusicMuteToggle.isOn = MusicManager.instance.MusicVolume != 0;
        sfxVolumeSlider.value = MusicManager.instance.SFXVolume;
        sfxMuteToggle.isOn = MusicManager.instance.SFXVolume != 0;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    //void ShowInstructions()
    //{
    //    instructionsPanel.SetActive(true);
    //}

    //void BackToMain()
    //{
    //    instructionsPanel.SetActive(false);
    //}

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

    private void SetSFXVolume(float volume)
    {
        MusicManager.instance.SetSFXVolume(volume);
        if (volume > 0 && !sfxMuteToggle.isOn)
        {
            sfxMuteToggle.isOn = true;
        }
    }

    private void ToggleMute(bool isUnmuted, string musicOrSFX)
    {
        if (musicOrSFX == "music")
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
        else if (musicOrSFX == "sfx")
        {
            if (isUnmuted)
            {
                MusicManager.instance.SetSFXVolume(sfxVolumeSlider.value);
            }
            else
            {
                MusicManager.instance.SetSFXVolume(0);
            }
        }
    }
}
