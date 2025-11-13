using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    public float speed;
    private Vector2 movement;
    private bool isInteracting = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isInteracting)
        {
            movement = Vector2.zero;
        }
        else
        {
            HandleMovementInput();
        }
        HandleMovementAnimation();
    }

    void FixedUpdate()
    {
        if (isInteracting)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    void HandleMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal"); // A/D atau Panah Kiri/Kanan
        movement.y = Input.GetAxis("Vertical");   // W/S atau Panah Atas/Bawah
        movement.Normalize(); // Agar kecepatan diagonal sama
    }

    void HandleMovementAnimation()
    {
        anim.SetBool("IsMoving", movement.magnitude > 0);

        if (movement.magnitude > 0 && !isInteracting)
        {
            // Kirim arah ke Animator
            anim.SetFloat("MoveX", movement.x);
            anim.SetFloat("MoveY", movement.y);
        }

        if (movement.x > 0.01f) // Bergerak ke KANAN
        {
            spriteRenderer.flipX = true; // Balik sprite (menghadap Kanan)
        }
        else if (movement.x < -0.01f) // Bergerak ke KIRI
        {
            spriteRenderer.flipX = false; // Gunakan sprite asli (menghadap Kiri)
        }
    }

    public void setInteracting(bool state)
    {
        isInteracting = state;
    }

    public void PlayInteractionAnimation(string triggerName)
    {
        if (anim != null)
        {
            anim.SetTrigger(triggerName);
        }
    }

    public void SetFlipX(bool flip)
    {
        spriteRenderer.flipX = flip;
    }
}