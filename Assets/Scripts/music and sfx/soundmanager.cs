using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public soundlibrary soundLibrary;
    public AudioSource sfxSource;
    public AudioMixer mixer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("SFXVolume", 0f);
        mixer.SetFloat("SFXVolume", savedVolume);
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void Play(string soundName)
    {
        AudioClip clip = soundLibrary.GetClipFromName(soundName);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
