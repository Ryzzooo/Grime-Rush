using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pausemenu : MonoBehaviour
{
    [Header("Panel Utama")]
    public GameObject pauseMenuUI;
    public GameObject optionPanel;
    public GameObject volumePanel;
    public GameObject creditPanel;
    public GameObject controlPanel;
    public GameObject statusPanel;

    [Header("Slider Volume")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private bool isPaused = false;

    void Start()
    {
        // Inisialisasi slider dengan volume tersimpan
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0f);

        // Hubungkan slider ke fungsi volume manager
        musicSlider.onValueChanged.AddListener(MusicManager.Instance.SetVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetVolume);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuUI.SetActive(true);
        optionPanel.SetActive(false);
        volumePanel.SetActive(false);
        creditPanel.SetActive(false);
        controlPanel.SetActive(false);
        if (statusPanel != null) statusPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuUI.SetActive(false);
        optionPanel.SetActive(false);
        volumePanel.SetActive(false);
        creditPanel.SetActive(false);
        controlPanel.SetActive(false);
        if (statusPanel != null) statusPanel.SetActive(false);
    }

    // --- Navigasi antar panel ---
    public void OpenOptionPanel()
    {
        pauseMenuUI.SetActive(false);
        optionPanel.SetActive(true);
        volumePanel.SetActive(false);
        creditPanel.SetActive(false);
        controlPanel.SetActive(false);
        if (statusPanel != null) statusPanel.SetActive(false);
    }

    public void OpenVolumePanel()
    {
        optionPanel.SetActive(false);
        volumePanel.SetActive(true);
    }

    public void OpenCreditPanel()
    {
        pauseMenuUI.SetActive(false);
        creditPanel.SetActive(true);
    }

    public void OpenControlPanel()
    {
        optionPanel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void OpenStatusPanel()
    {
        pauseMenuUI.SetActive(false);
        optionPanel.SetActive(false);
        volumePanel.SetActive(false);
        creditPanel.SetActive(false);
        controlPanel.SetActive(false);
        if (statusPanel != null) statusPanel.SetActive(true);
    }

    // 🆕 Tombol Quit ke Main Menu
    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); // Ganti jika nama scene utama kamu berbeda
    }
}
