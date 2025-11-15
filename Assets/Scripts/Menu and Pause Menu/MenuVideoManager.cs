using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; // Penting untuk VideoPlayer

public class MenuVideoManager : MonoBehaviour
{
    // Nama 'key' untuk ingatan
    private const string HasPlayedKey = "HasPlayedFirstTime";

    [Header("Komponen Video")]
    public GameObject videoPanel;    // RawImage fullscreen (VideoPanel)
    public VideoPlayer videoPlayer;  // Komponen VideoPlayer
    
    [Header("Menu Utama")]
    public GameObject mainMenuPanel; // Tombol-tombol menu utamamu

    void Start()
    {
        // 1. Cek 'ingatan'
        //    PlayerPrefs.GetInt(key, 0) artinya: "Cari 'key', jika tidak ada, beri nilai 0"
        if (PlayerPrefs.GetInt(HasPlayedKey, 0) == 0)
        {
            // --- INI PERTAMA KALINYA MAIN ---

            // 1. Matikan menu utama, nyalakan panel video
            mainMenuPanel.SetActive(false);
            videoPanel.SetActive(true);

            // 2. Daftarkan 'event' saat video selesai
            videoPlayer.loopPointReached += OnVideoFinished;

            // 3. Mulai mainkan video
            videoPlayer.Play();
        }
        else
        {
            // --- SUDAH PERNAH MAIN ---

            // Langsung matikan panel video dan nyalakan menu
            videoPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }

    // Fungsi ini akan otomatis dipanggil saat video selesai
    void OnVideoFinished(VideoPlayer vp)
    {
        // 1. Matikan panel video
        videoPanel.SetActive(false);
        
        // 2. Nyalakan menu utama
        mainMenuPanel.SetActive(true);

        // 3. ATUR INGATAN: "Sudah pernah main"
        PlayerPrefs.SetInt(HasPlayedKey, 1);
        PlayerPrefs.Save(); // Simpan ingatan
    }
}