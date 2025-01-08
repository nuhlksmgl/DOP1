using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private bool facingRight = true;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    [Header("Ladder Settings")]
    [SerializeField] private float climbSpeed = 8f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isDashing = false;
    private bool canDash = true;
    private bool isLadder = false;
    private bool isClimbing = false;
    private bool isCrouching = false;

    private float horizontalInput;
    private float verticalInput;
    private float dashTime;
    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        rb.gravityScale = 4f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        currentSpeed = moveSpeed;
    }

    private void Update()
    {
        if (!isDashing)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            if (horizontalInput != 0)
            {
                bool shouldFaceRight = horizontalInput > 0;
                if (facingRight != shouldFaceRight)
                {
                    Flip();
                }
            }

            if (isLadder && Mathf.Abs(verticalInput) > 0f)
            {
                isClimbing = true;
                animator.SetBool("ladderClimb", true); // T�rmanma animasyonunu tetikle
                Debug.Log("T�rmanma animasyonu ba�lat�ld�.");
            }
            else if (!isLadder || Mathf.Abs(verticalInput) == 0f)
            {
                isClimbing = false;
                animator.SetBool("ladderClimb", false); // T�rmanma animasyonunu durdur
                Debug.Log("T�rmanma animasyonu durduruldu.");
            }

            // Y�r�y�� animasyonunu tetikleme
            if (!isClimbing && !isCrouching)
            {
                animator.SetBool("isWalking", horizontalInput != 0);
                Debug.Log("Y�r�y�� animasyonu tetiklendi.");
            }

            // Saklanma mekani�i kontrol�
            if (Input.GetKeyDown(KeyCode.C))
            {
                StartCrouch();
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                EndCrouch();
            }

            // Idle animasyonunu tetikleme
            animator.SetBool("isIdle", horizontalInput == 0 && verticalInput == 0 && !isClimbing && !isDashing && !isCrouching);
            if (horizontalInput == 0 && verticalInput == 0 && !isClimbing && !isDashing && !isCrouching)
            {
                Debug.Log("Idle animasyonu tetiklendi.");
            }
        }

        // Dash mekanizmas�n� kontrol et
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && horizontalInput != 0 && !isClimbing)
        {
            StartDash();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) return;

        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
        }
        else
        {
            rb.gravityScale = 4f;
            rb.velocity = new Vector2(horizontalInput * currentSpeed, rb.velocity.y);
        }
    }

    private void StartDash()
    {
        isDashing = true;
        canDash = false;
        dashTime = dashDuration;

        rb.velocity = new Vector2(horizontalInput * dashSpeed, 0f);

        animator.SetBool("isDashing", true); // Dash animasyonunu tetikleme
        Debug.Log("Dash animasyonu ba�lat�ld�.");

        Invoke(nameof(EndDash), dashDuration);
    }

    private void EndDash()
    {
        isDashing = false;
        animator.SetBool("isDashing", false); // Dash animasyonunu durdurma
        Debug.Log("Dash animasyonu durduruldu.");

        Invoke(nameof(ResetDashCooldown), dashCooldown);
    }

    private void ResetDashCooldown()
    {
        canDash = true;
    }

    private void StartCrouch()
    {
        isCrouching = true;
        currentSpeed = crouchSpeed;
        animator.SetBool("isCrouching", true); // E�ilme animasyonunu tetikleme
        Debug.Log("E�ilme animasyonu ba�lat�ld�.");
    }

    private void EndCrouch()
    {
        isCrouching = false;
        currentSpeed = moveSpeed;
        animator.SetBool("isCrouching", false); // E�ilme animasyonunu durdurma
        Debug.Log("E�ilme animasyonu durduruldu.");
    }

    private void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            Debug.Log("Merdivene dokunuldu.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            animator.SetBool("ladderClimb", false); // Merdivenden ��karken animasyonu durdur
            Debug.Log("Merdivenden ��k�ld�.");

            // Y�r�y�� animasyonunu tetikleme
            animator.SetBool("isWalking", horizontalInput != 0);
            Debug.Log("Y�r�y�� animasyonu tetiklendi.");
        }
    }
}
