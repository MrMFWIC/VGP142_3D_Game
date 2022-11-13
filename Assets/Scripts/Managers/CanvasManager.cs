using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CanvasManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    [Header("Buttons")]
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton;
    public Button resumeGame;
    public Button returnToMenu;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Slider")]
    public Slider masterVolSlider;
    public Slider musicVolSlider;
    public Slider SFXVolSlider;

    [Header("Text")]
    public Text masterVolSliderText;
    public Text musicVolSliderText;
    public Text SFXVolSliderText;

    void Start()
    {
        if (startButton)
        {
            startButton.onClick.AddListener(() => StartGame());
        }

        if (settingsButton)
        {
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());
        }

        if (quitButton)
        {
            quitButton.onClick.AddListener(() => QuitGame());
        }

        if (backButton)
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                backButton.onClick.AddListener(() => ShowMainMenu());
            }
            else if (SceneManager.GetActiveScene().name == "Level")
            {
                backButton.onClick.AddListener(() => ShowPauseMenu());
            }
        }

        if (masterVolSlider)
        {
            float mixerValue;
            audioMixer.GetFloat("MasterVol", out mixerValue);
            masterVolSlider.onValueChanged.AddListener((value) => MasterSliderValueChange(value));
            masterVolSlider.value = mixerValue + 80;
        }

        if (musicVolSlider)
        {
            float mixerValue;
            audioMixer.GetFloat("MusicVol", out mixerValue);
            musicVolSlider.onValueChanged.AddListener((value) => MusicSliderValueChange(value));
            musicVolSlider.value = mixerValue + 80;
        }

        if (SFXVolSlider)
        {
            float mixerValue;
            audioMixer.GetFloat("SFXVol", out mixerValue);
            SFXVolSlider.onValueChanged.AddListener((value) => SFXSliderValueChange(value));
            SFXVolSlider.value = mixerValue + 80;
        }

        if (resumeGame)
        {
            resumeGame.onClick.AddListener(() => ResumeGame());
        }

        if (returnToMenu)
        {
            returnToMenu.onClick.AddListener(() => LoadMenu());
        }
    }

    void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);

                if (pauseMenu.activeSelf)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
        }
    }

    void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level");
        if (pauseMenu)
        {
            pauseMenu.SetActive(false);
        }
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    void ShowSettingsMenu()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Level")
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
    }

    void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    void ShowPauseMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void MasterSliderValueChange(float value)
    {
        if (masterVolSliderText)
        {
            masterVolSliderText.text = (value).ToString();
            audioMixer.SetFloat("MasterVol", value - 80);
        }
    }

    void MusicSliderValueChange(float value)
    {
        if (musicVolSliderText)
        {
            musicVolSliderText.text = value.ToString();
            audioMixer.SetFloat("MusicVol", value - 80);
        }
    }

    void SFXSliderValueChange(float value)
    {
        if (SFXVolSliderText)
        {
            SFXVolSliderText.text = value.ToString();
            audioMixer.SetFloat("SFXVol", value - 80);
        }
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}