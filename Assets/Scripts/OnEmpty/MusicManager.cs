using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource backgroundMusic;
    private List<AudioSource> uiAudioSources = new List<AudioSource>();
    private List<AudioSource> gameEffectAudioSources = new List<AudioSource>();

    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip toggleClickSound;
    [SerializeField] private AudioClip sliderChangeSound;

    private float musicVolume = 1.0f; public float MusicVolume => musicVolume;
    private float uiVolume = 1.0f; public float UIVolume => uiVolume;
    public float gameEffectVolume = 1.0f; public float GameEffectVolume => gameEffectVolume;

    private const string MusicVolumeKey = "MusicVolume";
    private const string UIVolumeKey = "UIVolume";
    private const string GameEffectVolumeKey = "GameEffectVolume";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        backgroundMusic = GetComponent<AudioSource>();
        LoadVolumeSettings();
        backgroundMusic.volume = musicVolume;
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignClickSounds();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = musicVolume;
        }
        SaveVolumeSettings();
    }

    public void SetUIVolume(float volume)
    {
        uiVolume = volume;
        foreach (var audioSource in uiAudioSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = uiVolume;
            }
        }
        SaveVolumeSettings();
    }

    public void SetGameEffectVolume(float volume)
    {
        gameEffectVolume = volume;
        foreach (var audioSource in gameEffectAudioSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = gameEffectVolume;
            }
        }
        SaveVolumeSettings();
    }

    public void RegisterGameEffectAudioSource(AudioSource audioSource)
    {
        if (!gameEffectAudioSources.Contains(audioSource))
        {
            audioSource.volume = gameEffectVolume;
            gameEffectAudioSources.Add(audioSource);
        }
    }

    public void AssignClickSounds()
    {
        uiAudioSources.Clear();

        Button[] buttons = Resources.FindObjectsOfTypeAll<Button>();
        Toggle[] toggles = Resources.FindObjectsOfTypeAll<Toggle>();
        Slider[] sliders = Resources.FindObjectsOfTypeAll<Slider>();

        foreach (Button button in buttons)
        {
            if (button.gameObject.scene.name == null) continue;
            AudioSource audioSource = button.gameObject.AddComponent<AudioSource>();
            audioSource.clip = buttonClickSound;
            audioSource.volume = uiVolume;
            uiAudioSources.Add(audioSource);
            button.onClick.AddListener(() => PlaySound(audioSource));
        }

        foreach (Toggle toggle in toggles)
        {
            if (toggle.gameObject.scene.name == null) continue;
            AudioSource audioSource = toggle.gameObject.AddComponent<AudioSource>();
            audioSource.clip = toggleClickSound;
            audioSource.volume = uiVolume;
            uiAudioSources.Add(audioSource);
            toggle.onValueChanged.AddListener((value) => PlaySound(audioSource));
        }

        foreach (Slider slider in sliders)
        {
            if (slider.gameObject.scene.name == null) continue;
            AudioSource audioSource = slider.gameObject.AddComponent<AudioSource>();
            audioSource.clip = sliderChangeSound;
            audioSource.volume = uiVolume;
            uiAudioSources.Add(audioSource);

            EventTrigger trigger = slider.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback.AddListener((data) => PlaySound(audioSource));
            trigger.triggers.Add(entry);
        }
    }

    private void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(UIVolumeKey, uiVolume);
        PlayerPrefs.SetFloat(GameEffectVolumeKey, gameEffectVolume);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
        }

        if (PlayerPrefs.HasKey(UIVolumeKey))
        {
            uiVolume = PlayerPrefs.GetFloat(UIVolumeKey);
        }

        if (PlayerPrefs.HasKey(GameEffectVolumeKey))
        {
            gameEffectVolume = PlayerPrefs.GetFloat(GameEffectVolumeKey);
        }
    }
}
