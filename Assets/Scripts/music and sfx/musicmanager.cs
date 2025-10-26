using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("References")]
    public musiclibrary musicLibrary;
    public AudioSource musicSource;
    public AudioMixer mixer;

    [Header("Settings")]
    public float fadeDuration = 1f;
    private string currentTrack;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate MusicManager destroyed.");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        Debug.Log("MusicManager created in scene: " + SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0f);
        mixer.SetFloat("MusicVolume", savedVolume);

        PlayMusicFromScene(SceneManager.GetActiveScene().name);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentTrack = ""; // reset agar play music tetap dijalankan
        PlayMusicFromScene(scene.name);
    }

    private void PlayMusicFromScene(string sceneName)
    {
        Debug.Log("Scene Loaded: " + sceneName);

        switch (sceneName)
        {
            case "Menu":
                PlayMusic("menu");
                break;
            case "IntroScene":
                PlayMusic("intro");
                break;
            case "SampleScene":
                PlayMusic("game");
                break;
            case "Percobaan satu":
                PlayMusic("game");
                break;
            default:
                Debug.Log("No matching music for scene: " + sceneName);
                break;
        }
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void PlayMusic(string trackName)
    {
        if (currentTrack == trackName && musicSource.isPlaying)
        {
            Debug.Log("Already playing music: " + trackName);
            return;
        }

        AudioClip newClip = musicLibrary.GetClipFromName(trackName);
        if (newClip == null)
        {
            Debug.LogWarning("Track not found in library: " + trackName);
            return;
        }

        StopAllCoroutines();
        StartCoroutine(FadeInNewMusic(newClip));
        currentTrack = trackName;
    }

    IEnumerator FadeInNewMusic(AudioClip clip)
    {
        Debug.Log("Fading to new music: " + clip.name);

        // Fade out music
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();

        // Fade in music
        for (float t = 0; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = 1f;
    }
}
