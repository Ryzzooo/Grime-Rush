using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // Penting

[RequireComponent(typeof(VideoPlayer))]
public class CutscenePlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
        videoPlayer.SetTargetAudioSource(0, audioSource);

        // 1. Cek 'surat perintah' dari DataManager
        if (CutsceneDataManager.videoToPlay != null)
        {
            // Pasang video-nya
            videoPlayer.clip = CutsceneDataManager.videoToPlay;

            // Daftarkan 'event' saat video selesai
            videoPlayer.loopPointReached += OnVideoFinished;

            // Mulai mainkan
            videoPlayer.Play();
        }
        else
        {
            // Failsafe: Jika kita tidak sengaja masuk ke scene ini
            Debug.LogWarning("Tidak ada video yang diset! Kembali ke Menu.");
            SceneManager.LoadScene("MainMenu"); // Ganti "MainMenu" dengan nama scene menumu
        }
    }

    // Fungsi ini akan otomatis dipanggil saat video selesai
    void OnVideoFinished(VideoPlayer vp)
    {
        // 3. Baca 'surat perintah' lagi untuk tahu harus ke mana
        if (!string.IsNullOrEmpty(CutsceneDataManager.nextSceneName))
        {
            SceneManager.LoadScene(CutsceneDataManager.nextSceneName);
        }
        else
        {
            // Failsafe lagi
            SceneManager.LoadScene("MainMenu");
        }
    }
}