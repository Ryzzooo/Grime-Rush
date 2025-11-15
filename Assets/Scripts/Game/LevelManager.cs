using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // Penting untuk TextMeshPro

public class LevelManager : MonoBehaviour
{
    // --- Singleton (agar gampang dipanggil) ---
    public static LevelManager instance;

    [Header("Tujuan Level")]
    public int totalTrashForThisLevel = 10; // Tentukan target level ini
    private int trashDroppedCount = 0; // Penghitung internal

    [Header("UI Panel Selesai")]
    public GameObject panelLevelComplete;
    public TextMeshProUGUI scoreValueText; // Teks untuk angka skor

    [Header("Komponen UI")]
    public TextMeshProUGUI trashCounterText;

    [Header("Level Berikutnya")]
    public string nextSceneName; // Nama scene (misal "Stage_2")

    private PlayerMove playerMoveScript; // Untuk membekukan player

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Pastikan panel mati di awal
        panelLevelComplete.SetActive(false);
        
        // Cari script gerak player
        playerMoveScript = FindFirstObjectByType<PlayerMove>();
        UpdateTrashCounterText();
    }

    // --- FUNGSI 'PELAPORAN' ---
    // PlayerControl akan memanggil ini
    public void OnTrashDropped()
    {
        trashDroppedCount++;
        UpdateTrashCounterText();
        
        // Cek apakah level selesai
        if (trashDroppedCount >= totalTrashForThisLevel)
        {
            ShowLevelCompletePanel();
        }
    }

    void UpdateTrashCounterText()
    {
        if (trashCounterText != null)
        {
            // Hitung sisanya
            int remaining = totalTrashForThisLevel - trashDroppedCount;
            trashCounterText.text = "Trash : " + remaining.ToString();
        }
    }

    void ShowLevelCompletePanel()
    {
        // 1. Tampilkan panel
        panelLevelComplete.SetActive(true);

        // 2. Bekukan player
        if (playerMoveScript != null)
        {
            playerMoveScript.setInteracting(true);
        }

        // 3. Ambil skor dari 'Bank' dan tampilkan
        if (ScoreManager.instance != null && scoreValueText != null)
        {
            // Kita perlu fungsi 'GetScore' di ScoreManager
            int finalScore = ScoreManager.instance.GetCurrentScore(); 
            scoreValueText.text = finalScore.ToString();
        }
    }

    // --- FUNGSI TOMBOL ---
    // Pasang ini di tombol 'NextLevelButton'
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}