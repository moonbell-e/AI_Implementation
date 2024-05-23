using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MainMenuController : MonoBehaviour
{
    [Header("Master Volume Slider")]
    [SerializeField] private TMP_Text _masterVolumeTextValue = null;
    [SerializeField] private Slider _masterVolumeSlider = null;
    [SerializeField] private float _defaultMasterVolume = 100.0f;

    [Header("Music Volume Slider")]
    [SerializeField] private TMP_Text _musicVolumeTextValue = null;
    [SerializeField] private Slider _musicVolumeSlider = null;
    [SerializeField] private float _defaultMusicVolume = 100.0f;

    [Header("Sound Volume Slider")]
    [SerializeField] private TMP_Text _soundVolumeTextValue = null;
    [SerializeField] private Slider _soundVolumeSlider = null;
    [SerializeField] private float _defaultSoundVolume = 100.0f;

    [Header("Brightness Slider")]
    [SerializeField] private TMP_Text _brightnessTextValue = null;
    [SerializeField] private Slider _brightnessSlider = null;
    [SerializeField] private float _defaultBrightness = 2.5f;
    [SerializeField] private Volume _volume;
    private ColorAdjustments _colorAdjustments;

    [Header("Settings Toggles")]
    [SerializeField] private Toggle _cursorBlockToggle = null;
    [SerializeField] private Toggle _screenShakingToggle = null;
    [SerializeField] private Toggle _easyModeToggle = null;
    [SerializeField] private Toggle _vibrationToggle = null;
    [SerializeField] private Toggle _timerToggle = null;
    [SerializeField] private Toggle _damageNumbersToggle = null;

    [Header("Sensativity Slider")]
    [SerializeField] private TMP_Text _controllerSensativityTextValue = null;
    [SerializeField] private Slider _controllerSensativitySlider = null;
    [SerializeField] private float _defaultControllerSensativity = 4.0f;
    public int _mainControllerSensativity = 4;

    [Header("Controls Toggles")]
    [SerializeField] private Toggle _autoAimToggle = null;
    [SerializeField] private Toggle _attackOnCursorToggle = null;
    [SerializeField] private Toggle _dashOnCursorToggle = null;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown _resolutionDropdown;
    private List<Resolution> _resolutions = null;

    [Header("Graphics Quality Dropdown")]
    public TMP_Dropdown _graphicsQualityDropdown;

    [Header("Display Toggles")]
    [SerializeField] private Toggle _fullscreenToggle = null;
    [SerializeField] private Toggle _borderToggle = null;
    [SerializeField] private Toggle _verticalSyncToggle = null;

    [Header("Save Buttons")]
    [SerializeField] private Button _save1 = null;
    [SerializeField] private Button _save2 = null;
    [SerializeField] private Button _save3 = null;
    [SerializeField] private Button _save4 = null;
    [SerializeField] private Button _backToMenu = null;


    //Making Player prefs works
    private void Start()
    {
        //Looking for resolutions
        _resolutions = Screen.resolutions.ToList();
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int curentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Count; i++)
        {
            if (_resolutions[i].refreshRateRatio.value != Screen.currentResolution.refreshRateRatio.value)
            {
                _resolutions.Remove(_resolutions[i]);
                i--;
            }
        }

        for (int i = 0; i < _resolutions.Count; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
            {
                curentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = curentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();


        //Looking for PlayerPrefs
        if (!PlayerPrefs.HasKey("HavePlayerPrefs"))
        {
            ResetButton("Settings");
            ResetButton("Controls");
            ResetButton("Display");
            PlayerPrefs.SetString("language", "ru");
        }
        PlayerPrefs.SetInt("HavePlayerPrefs", 1);

        //Sliders
        AudioListener.volume = PlayerPrefs.GetFloat("masterVolume");
        _masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        _soundVolumeSlider.value = PlayerPrefs.GetFloat("soundVolume");

        _brightnessSlider.value = PlayerPrefs.GetFloat("brightness");
        if (_volume.profile.TryGet(out _colorAdjustments))
        {
            _colorAdjustments.postExposure.value = PlayerPrefs.GetFloat("brightness") - 2.5f;
        }

        _controllerSensativitySlider.value = PlayerPrefs.GetFloat("controllerSensativity");
        _mainControllerSensativity = Mathf.RoundToInt(PlayerPrefs.GetFloat("controllerSensativity"));


        //Dropdowns
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"));
        _graphicsQualityDropdown.value = PlayerPrefs.GetInt("qualityLevel");


        //Toggles
        if (PlayerPrefs.GetInt("cursorBlockToggle") == 1)
        {
            _cursorBlockToggle.isOn = true;
        }
        else
        {
            _cursorBlockToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("screenShakingToggle") == 1)
        {
            _screenShakingToggle.isOn = true;
        }
        else
        {
            _screenShakingToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("easyModeToggle") == 1)
        {
            _easyModeToggle.isOn = true;
        }
        else
        {
            _easyModeToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("vibrationToggle") == 1)
        {
            _vibrationToggle.isOn = true;
        }
        else
        {
            _vibrationToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("timerToggle") == 1)
        {
            _timerToggle.isOn = true;
        }
        else
        {
            _timerToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("damageNumbersToggle") == 1)
        {
            _damageNumbersToggle.isOn = true;
        }
        else
        {
            _damageNumbersToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("autoAimToggle") == 1)
        {
            _autoAimToggle.isOn = true;
        }
        else
        {
            _autoAimToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("attackOnCursorToggle") == 1)
        {
            _attackOnCursorToggle.isOn = true;
        }
        else
        {
            _attackOnCursorToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("dashOnCursorToggle") == 1)
        {
            _dashOnCursorToggle.isOn = true;
        }
        else
        {
            _dashOnCursorToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("fullscreenToggle") == 1)
        {
            _fullscreenToggle.isOn = true;
            Screen.fullScreen = true;
        }
        else
        {
            _fullscreenToggle.isOn = false;
            Screen.fullScreen = false;
        }

        if (PlayerPrefs.GetInt("borderToggle") == 1)
        {
            _borderToggle.isOn = true;
        }
        else
        {
            _borderToggle.isOn = false;
        }

        if (PlayerPrefs.GetInt("verticalSyncToggle") == 1)
        {
            _verticalSyncToggle.isOn = true;
        }
        else
        {
            _verticalSyncToggle.isOn = false;
        }
    }


    //Exit button
    public void ExitButton()
    {
        Application.Quit();
    }


    //Settings sliders
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        _masterVolumeTextValue.text = volume.ToString("0");
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        _musicVolumeTextValue.text = volume.ToString("0");
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetSoundVolume(float volume)
    {
        _soundVolumeTextValue.text = volume.ToString("0");
        PlayerPrefs.SetFloat("soundVolume", volume);
    }
    public void SetBrightness(float brightness)
    {
        _brightnessTextValue.text = brightness.ToString("0.0");
        PlayerPrefs.SetFloat("brightness", brightness);
        if (_volume.profile.TryGet(out _colorAdjustments))
        {
            _colorAdjustments.postExposure.value = brightness - 2.5f;
        }
    }


    //Settings toggles
    public void SetCursorBlock(bool toggle)
    {
        PlayerPrefs.SetInt("cursorBlockToggle", (toggle ? 1 : 0));
    }
    public void SetScreenShaking(bool toggle)
    {
        PlayerPrefs.SetInt("screenShakingToggle", (toggle ? 1 : 0));
    }
    public void SetEasyMode(bool toggle)
    {
        PlayerPrefs.SetInt("easyModeToggle", (toggle ? 1 : 0));
    }
    public void SetVibration(bool toggle)
    {
        PlayerPrefs.SetInt("vibrationToggle", (toggle ? 1 : 0));
    }
    public void SetTimerToggle(bool toggle)
    {
        PlayerPrefs.SetInt("timerToggle", (toggle ? 1 : 0));
    }
    public void SetDamageNumbersToggle(bool toggle)
    {
        PlayerPrefs.SetInt("damageNumbersToggle", (toggle ? 1 : 0));
    }


    //Controls sliders
    public void SetControllerSensativity(float sensativity)
    {
        _mainControllerSensativity = Mathf.RoundToInt(sensativity);
        _controllerSensativityTextValue.text = sensativity.ToString("0");
        PlayerPrefs.SetFloat("controllerSensativity", sensativity);
    }


    //Controls toggles
    public void SetAutoAimToggle(bool toggle)
    {
        PlayerPrefs.SetInt("autoAimToggle", (toggle ? 1 : 0));
    }
    public void SetAttackOnCursorToggle(bool toggle)
    {
        PlayerPrefs.SetInt("attackOnCursorToggle", (toggle ? 1 : 0));
    }
    public void SetDashOnCursorToggle(bool toggle)
    {
        PlayerPrefs.SetInt("dashOnCursorToggle", (toggle ? 1 : 0));
    }


    //Display dropdowns
    public void SetQuality(int qualityIndex)
    {
        PlayerPrefs.SetInt("qualityLevel", qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    //Display toggles
    public void SetFullscreenToggle(bool toggle)
    {
        PlayerPrefs.SetInt("fullscreenToggle", (toggle ? 1 : 0));
        Screen.fullScreen = toggle;
    }
    public void SetBorderToggle(bool toggle)
    {
        PlayerPrefs.SetInt("borderToggle", (toggle ? 1 : 0));
    }
    public void SetVerticalSyncToggle(bool toggle)
    {
        PlayerPrefs.SetInt("verticalSyncToggle", (toggle ? 1 : 0));
    }


    //Resets
    public void ResetButton(string menuType)
    {
        if (menuType == "Settings")
        {
            AudioListener.volume = _defaultMasterVolume;
            _masterVolumeSlider.value = _defaultMasterVolume;
            _masterVolumeTextValue.text = _defaultMasterVolume.ToString("0");
            PlayerPrefs.SetFloat("masterVolume", _defaultMasterVolume);

            _musicVolumeSlider.value = _defaultMusicVolume;
            _musicVolumeTextValue.text = _defaultMusicVolume.ToString("0");
            PlayerPrefs.SetFloat("musicVolume", _defaultMusicVolume);

            _soundVolumeSlider.value = _defaultSoundVolume;
            _soundVolumeTextValue.text = _defaultSoundVolume.ToString("0");
            PlayerPrefs.SetFloat("soundVolume", _defaultSoundVolume);

            _brightnessSlider.value = _defaultBrightness;
            _brightnessTextValue.text = _defaultBrightness.ToString("0.0");
            PlayerPrefs.SetFloat("brightness", _defaultBrightness);
            if (_volume.profile.TryGet(out _colorAdjustments))
            {
                _colorAdjustments.postExposure.value = _defaultBrightness - 2.5f;
            }

            PlayerPrefs.SetInt("cursorBlockToggle", 1);
            _cursorBlockToggle.isOn = true;
            PlayerPrefs.SetInt("screenShakingToggle", 1);
            _screenShakingToggle.isOn = true;
            PlayerPrefs.SetInt("easyModeToggle", 0);
            _easyModeToggle.isOn = false;
            PlayerPrefs.SetInt("vibrationToggle", 1);
            _vibrationToggle.isOn = true;
            PlayerPrefs.SetInt("timerToggle", 0);
            _timerToggle.isOn = false;
            PlayerPrefs.SetInt("damageNumbersToggle", 1);
            _damageNumbersToggle.isOn = true;
        }

        if (menuType == "Controls")
        {
            _mainControllerSensativity = Mathf.RoundToInt(_defaultControllerSensativity);
            _controllerSensativitySlider.value = _defaultControllerSensativity;
            _controllerSensativityTextValue.text = _defaultControllerSensativity.ToString("0");
            PlayerPrefs.SetFloat("controllerSensativity", _defaultControllerSensativity);

            PlayerPrefs.SetInt("autoAimToggle", 1);
            _autoAimToggle.isOn = true;
            PlayerPrefs.SetInt("attackOnCursorToggle", 1);
            _attackOnCursorToggle.isOn = true;
            PlayerPrefs.SetInt("dashOnCursorToggle", 0);
            _dashOnCursorToggle.isOn = false;
        }

        if (menuType == "Display")
        {
            PlayerPrefs.SetInt("qualityLevel", 2);
            QualitySettings.SetQualityLevel(2);
            _graphicsQualityDropdown.value = 2;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            _resolutionDropdown.value = _resolutions.Count;

            PlayerPrefs.SetInt("fullscreenToggle", 1);
            _fullscreenToggle.isOn = true;
            Screen.fullScreen = true;
            PlayerPrefs.SetInt("borderToggle", 1);
            _borderToggle.isOn = true;
            PlayerPrefs.SetInt("verticalSyncToggle", 1);
            _verticalSyncToggle.isOn = true;
        }
    }


    //Setting language
    public void SetLanguage(string language)
    {
        PlayerPrefs.SetString("language", language);
    }


    //Play button
    public void PlayButton()
    {
        _save1.interactable = true;
        _save2.interactable = true;
        _save3.interactable = true;
        _save4.interactable = true;
        _backToMenu.interactable = true;
    }


    public void BackToMenu()
    {
        _save1.interactable = false;
        _save2.interactable = false;
        _save3.interactable = false;
        _save4.interactable = false;
        _backToMenu.interactable = false;
    }


    //Loading scene
    public IEnumerator Wait(int saveIndex)
    {
        yield return new WaitForSeconds(1.5f);

        if (saveIndex == 0)
        {
            SceneManager.LoadScene("Vladislavania");
            BackToMenu();
        }
        if (saveIndex == 1)
        {
            SceneManager.LoadScene("Aigerimia");
            BackToMenu();
        }
        if (saveIndex == 2)
        {
            SceneManager.LoadScene("Location");
            BackToMenu();
        }
        if (saveIndex == 3)
        {
            SceneManager.LoadScene("Vladislavania");
            BackToMenu();
        }
    }

    public void SaveLoader(int saveIndex)
    {
        StartCoroutine(Wait(saveIndex));
    }
}