using UnityEngine;
using TMPro; // WAJIB untuk TextMeshPro

public class ScoreManager : MonoBehaviour
{
    // --- Singleton (agar gampang dipanggil dari mana saja) ---
    public static ScoreManager instance;

    [Header("Komponen UI")]
    public TextMeshProUGUI scoreText; // Si "Kasir" (dari Langkah 2)

    [Header("Data Skor")]
    private int currentScore = 0; // Skor dimulai dari 0

    void Awake()
    {
        // Setup Singleton
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
        // Tampilkan skor awal (0) saat game dimulai
        UpdateScoreText();
    }

    // Fungsi 'sakti' untuk menambah atau mengurangi skor
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreText();

        Debug.Log("Skor berubah: " + currentScore);
    }

    // Fungsi internal untuk memperbarui teks di UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            // "Skor: 100"
            scoreText.text = "Score : " + currentScore.ToString();
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}