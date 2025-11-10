using UnityEngine;
using System.Collections; // Diperlukan untuk Coroutine

public class PlayerController : MonoBehaviour
{
    // === KOMPONEN ===
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    // === PENGATURAN GERAKAN ===
    public float speed;
    private Vector2 movement;
    private bool isInteracting = false; // Flag untuk menghentikan gerakan saat animasi

    // === PENGATURAN INTERAKSI ===
    public Transform holdingSpot;
    private GameObject heldTrash;
    private GameObject trashInRange = null;
    private BinController trashcanInRange = null;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Jika sedang berinteraksi (membuang/mengambil), jangan proses input baru
        if (isInteracting)
        {
            movement = Vector2.zero; // Hentikan input gerakan
            return;
        }

        // 1. Proses Input Gerakan
        HandleMovementInput();
        
        // 2. Atur Animasi Gerakan & Balik Sprite
        HandleMovementAnimation();

        // 3. Proses Input Interaksi (E)
        HandleInteractionInput();
    }

    void FixedUpdate()
    {
        // Hentikan gerakan fisik jika sedang berinteraksi
        if (isInteracting)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        
        // Terapkan gerakan fisik
        rb.linearVelocity = movement * speed;
    }

    // --- BAGIAN GERAKAN ---

    void HandleMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal"); // A/D atau Panah Kiri/Kanan
        movement.y = Input.GetAxis("Vertical");   // W/S atau Panah Atas/Bawah
        movement.Normalize(); // Agar kecepatan diagonal sama
    }

    void HandleMovementAnimation()
    {
        // Atur parameter Animator untuk Blend Tree
        anim.SetBool("IsMoving", movement.magnitude > 0);

        if (movement.magnitude > 0)
        {
            // Kirim arah ke Animator
            anim.SetFloat("MoveX", movement.x);
            anim.SetFloat("MoveY", movement.y);
        }

        // Logika membalik sprite (Sprite Flip)
        // Asumsi: Animasi 'jalan_kesamping' Anda menghadap ke KIRI
        if (movement.x > 0.01f) // Bergerak ke KANAN
        {
            spriteRenderer.flipX = true; // Balik sprite (menghadap Kanan)
        }
        else if (movement.x < -0.01f) // Bergerak ke KIRI
        {
            spriteRenderer.flipX = false; // Gunakan sprite asli (menghadap Kiri)
        }
    }

    // --- BAGIAN INTERAKSI ---

    void HandleInteractionInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldTrash != null && trashcanInRange != null)
            {
                // Kasus 1: Memegang sampah DAN dekat tong sampah -> BUANG SAMPAH
                StartCoroutine(DropTrashCoroutine());
            }
            else if (heldTrash == null && trashInRange != null)
            {
                // Kasus 2: Tidak memegang sampah DAN dekat sampah -> AMBIL SAMPAH
                StartCoroutine(PickUpTrashCoroutine(trashInRange));
            }
        }
    }

    IEnumerator PickUpTrashCoroutine(GameObject trashToPickUp)
    {
        isInteracting = true; // Berhenti bergerak
        trashInRange = null;  // Pastikan tidak bisa diambil lagi
        
        // Mainkan animasi ambil (Asumsi nama Trigger: "ambil_depan")
        anim.SetTrigger("ambil_depan");

        // Tunggu animasi selesai (sesuaikan 0.5f dengan panjang animasi Anda)
        yield return new WaitForSeconds(0.5f);

        // Logika mengambil sampah (dari skrip lama Anda)
        heldTrash = trashToPickUp;
        heldTrash.transform.SetParent(holdingSpot);
        heldTrash.transform.localPosition = Vector3.zero;

        Collider2D trashCollider = heldTrash.GetComponent<Collider2D>();
        if (trashCollider != null)
        {
            trashCollider.enabled = false;
        }

        isInteracting = false; // Boleh bergerak lagi
    }

    IEnumerator DropTrashCoroutine()
    {
        isInteracting = true; // Berhenti bergerak

        // Tentukan arah player relatif terhadap tong sampah
        Vector2 playerPos = transform.position;
        Vector2 binPos = trashcanInRange.transform.position;
        Vector2 diff = playerPos - binPos;

        // --- Logika Animasi Buang Sampah ---
        if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x)) // Dominan Vertikal
        {
            if (diff.y > 0) // Player di ATAS tong sampah
            {
                anim.SetTrigger("buang_depan"); // Mainkan "buang_s_depan"
            }
            else // Player di BAWAH tong sampah
            {
                anim.SetTrigger("buang_belakang"); // Mainkan "buang_s_belakang"
            }
        }
        else // Dominan Horizontal
        {
            // Asumsi: Animasi 'buang_s_samping' menghadap ke KIRI
            if (diff.x > 0) // Player di KANAN tong sampah (menghadap Kiri)
            {
                spriteRenderer.flipX = false; // Gunakan sprite asli
                anim.SetTrigger("buang_samping");
            }
            else // Player di KIRI tong sampah (menghadap Kanan)
            {
                spriteRenderer.flipX = true; // Balik sprite
                anim.SetTrigger("buang_samping");
            }
        }

        // Tunggu animasi buang selesai (sesuaikan 0.7f dengan panjang animasi Anda)
        yield return new WaitForSeconds(0.7f);

        // Logika membuang sampah (dari skrip lama Anda)
        Destroy(heldTrash);
        heldTrash = null;

        isInteracting = false; // Boleh bergerak lagi
    }

    // --- BAGIAN DETEKSI (dari skrip lama PlayerControl) ---

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sampah"))
        {
            // Hanya set jika belum ada sampah lain yang terdeteksi
            if(trashInRange == null) 
                trashInRange = other.gameObject;
        }
        if (other.CompareTag("TrashCan"))
        {
            trashcanInRange = other.GetComponent<BinController>();
            if (trashcanInRange != null)
                trashcanInRange.OpenBin(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Hanya set null jika yang keluar adalah sampah yang sedang kita deteksi
        if (other.CompareTag("Sampah") && other.gameObject == trashInRange)
        {
            trashInRange = null;
        }

        if (other.CompareTag("TrashCan") && trashcanInRange != null)
        {
            trashcanInRange.OpenBin(false);
            trashcanInRange = null;
        }
    }
}