using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool facingRight = true;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;      // Dash s�ras�nda h�z
    [SerializeField] private float dashDuration = 0.2f;  // Dash s�resi
    [SerializeField] private float dashCooldown = 1f;    // Dash sonras� bekleme s�resi

    private bool isDashing = false;                     // Dash s�ras�nda olup olmad���n� takip
    private bool canDash = true;                        // Dash yapmaya uygunluk
    private float dashTime;                             // Aktif dash s�resini takip

    [Header("Components")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Movement variables
    private float horizontalInput;
    private Vector2 currentVelocity;

    private void Awake()
    {
        // Get required components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Configure Rigidbody2D for 2D movement
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        if (!isDashing)
        {
            // Get horizontal input (-1 to 1)
            horizontalInput = Input.GetAxisRaw("Horizontal");

            // Handle sprite flipping based on movement direction
            if (horizontalInput != 0)
            {
                bool shouldFaceRight = horizontalInput > 0;
                if (facingRight != shouldFaceRight)
                {
                    Flip();
                }
            }
        }

        // Dash giri�ini kontrol et (Shift tu�u ile)
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && horizontalInput != 0)
        {
            StartDash();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return; // Dash s�ras�nda hareket i�lemi devre d���

        // Calculate movement velocity
        currentVelocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Apply movement
        rb.velocity = currentVelocity;
    }

    private void StartDash()
    {
        isDashing = true;
        canDash = false;
        dashTime = dashDuration;

        // Dash y�n�n� ayarla
        rb.velocity = new Vector2(horizontalInput * dashSpeed, 0f);

        // Dash biti�ini gecikmeli tetikle
        Invoke(nameof(EndDash), dashDuration);
    }

    private void EndDash()
    {
        isDashing = false;

        // Dash'i bekleme s�resi sonunda tekrar etkinle�tir
        Invoke(nameof(ResetDash), dashCooldown);
    }

    private void ResetDash()
    {
        canDash = true;
    }

    private void Flip()
    {
        // Flip the character's facing direction
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }
}
