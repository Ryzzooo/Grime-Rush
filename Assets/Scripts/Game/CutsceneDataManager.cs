using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // Penting untuk VideoClip
using System.Collections;

public class CutsceneDataManager : MonoBehaviour
{
    // --- Singleton (agar gampang dipanggil dari mana saja) ---
    public static CutsceneDataManager instance;

    // --- Data 'Surat Perintah' (Statis) ---
    public static VideoClip videoToPlay;
    public static string nextSceneName;

    void Awake()
    {
        // Setup Singleton DontDestroyOnLoad
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Hanya boleh ada satu
        }
    }

    // Ini adalah 'tombol' ajaib yang akan kita panggil
    public static void LoadCutsceneScene(VideoClip clip, string sceneToLoadAfter)
    {
        // 1. Simpan data di 'surat perintah'
        videoToPlay = clip;
        nextSceneName = sceneToLoadAfter;

        // 2. Pergi ke scene cutscene
        // (Pastikan kamu punya scene bernama "CutsceneScene")
        instance.StartCoroutine(instance.LoadCutsceneSceneRoutine());
    }

    private IEnumerator LoadCutsceneSceneRoutine()
    {
        // 1. Perintahkan MusicManager untuk fade out (jika ada)
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.FadeOutMusic();

            // 2. TUNGGU sampai fade out selesai
            // (Kita asumsikan fadeDuration di MusicManager itu publik)
            yield return new WaitForSeconds(MusicManager.Instance.fadeDuration);
        }

        // 3. SETELAH fade out selesai, BARU pindah scene
        SceneManager.LoadScene("Cutscene"); 
    }
}