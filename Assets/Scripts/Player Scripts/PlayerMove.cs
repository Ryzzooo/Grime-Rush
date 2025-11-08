using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim; // <-- BARU: Variabel untuk Animator
    private Vector2 movement;
    public float speed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // <-- BARU: Ambil komponen Animator saat game dimulai
    }

    void Update()
    {
        Move();

        // <-- BARU: Kirim informasi ke Animator -->
        // movement.magnitude akan bernilai 0 jika tidak ada input, 
        // dan 1 jika ada input (karena Anda menggunakan .Normalize())
        // Kita kirim 'true' ke parameter "IsMoving" jika magnitude > 0
        anim.SetBool("IsMoving", movement.magnitude > 0);
    }

    void Move()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        movement.Normalize();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }
}