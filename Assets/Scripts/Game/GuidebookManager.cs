using UnityEngine;

public class GuidebookManager : MonoBehaviour
{
    [Header("Komponen UI")]
    public GameObject panelGuideBook; // Panel yang akan muncul

    [Header("Key G")]
    public KeyCode guideBookKey = KeyCode.G; // Tombol keyboard (G)

    private PlayerMove playerMoveScript; // Referensi ke script gerak
    private bool isGuidebookOpen = false; // Status buku

    void Start()
    {
        // Temukan script gerak player
        playerMoveScript = FindFirstObjectByType<PlayerMove>();
        
        // Pastikan buku tertutup saat mulai
        panelGuideBook.SetActive(false);
        isGuidebookOpen = false;
    }

    void Update()
    {
        // 1. Mendengar tombol 'G' di keyboard
        if (Input.GetKeyDown(guideBookKey))
        {
            ToggleGuidebook();
        }
    }

    // 2. FUNGSI INI AKAN DIPANGGIL OLEH KEYBOARD DAN TOMBOL UI
    // Ini adalah 'saklar' utama
    public void ToggleGuidebook()
    {
        // Balik statusnya
        isGuidebookOpen = !isGuidebookOpen; 

        if (isGuidebookOpen)
        {
            // Buka buku dan HENTIKAN player
            panelGuideBook.SetActive(true);
            if (playerMoveScript != null)
            {
                playerMoveScript.setInteracting(true);
            }
        }
        else
        {
            // Tutup buku dan BEBASKAN player
            panelGuideBook.SetActive(false);
            if (playerMoveScript != null)
            {
                playerMoveScript.setInteracting(false);
            }
        }
    }
}