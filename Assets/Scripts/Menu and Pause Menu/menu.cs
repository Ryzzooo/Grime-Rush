using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0f);

        musicSlider.onValueChanged.AddListener(MusicManager.Instance.SetVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetVolume);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial"); // menuju scene intro
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
