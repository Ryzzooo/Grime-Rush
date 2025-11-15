using UnityEngine;
using TMPro; // <-- WAJIB, karena kita pakai TextMeshPro
using System.Collections; // <-- WAJIB, untuk Coroutine (timer)

public class NotificationManager : MonoBehaviour
{
    // --- Singleton (agar gampang dipanggil dari mana saja) ---
    public static NotificationManager instance;

    [Header("Komponen UI")]
    public GameObject notificationPanel; // Panel induk
    public TextMeshProUGUI notificationText; // Teks di dalam panel

    [Header("Pengaturan")]
    public float displayDuration = 4f; // Berapa lama pesan muncul (detik)

    private Coroutine notificationCoroutine; // Untuk 'melacak' timer

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
        // Pastikan panel mati di awal
        notificationPanel.SetActive(false);
    }

    // Ini adalah fungsi 'sakti' yang akan kita panggil
    public void ShowNotification(string message)
    {
        // Jika sudah ada notifikasi, hentikan dulu timer-nya
        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }
        
        // Mulai timer baru
        notificationCoroutine = StartCoroutine(ShowNotificationRoutine(message));
    }

    // Ini adalah 'timer' yang akan berjalan di latar belakang
    private IEnumerator ShowNotificationRoutine(string message)
    {
        // 1. Tulis pesan ke teks
        notificationText.text = message;
        
        // 2. Tampilkan panel
        notificationPanel.SetActive(true);

        // 3. Tunggu
        yield return new WaitForSeconds(displayDuration);

        // 4. Sembunyikan panel
        notificationPanel.SetActive(false);

        // 5. Tandai bahwa timer sudah selesai
        notificationCoroutine = null;
    }
}