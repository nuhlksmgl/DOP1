using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool facingRight = true;

    [Header("Ladder Settings")]
    [SerializeField] private float climbSpeed = 8f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isDashing = false;
    private bool canDash = true;
    private bool isLadder = false;
    private bool isClimbing = false;

    private float horizontalInput;
    private float verticalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        rb.gravityScale = 4f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
                animator.SetBool("ladderClimb", false); // Y�r�y�� animasyonuna ge�
                Debug.Log("T�rmanma animasyonu durduruldu.");
            }
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
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
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
            animator.SetBool("ladderClimb", true); // Merdivene dokununca t�rmanma animasyonunu tetikle
            Debug.Log("Merdivene dokunuldu ve t�rmanma animasyonu tetiklendi.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            animator.SetBool("ladderClimb", false); // Merdivenden ��karken animasyonu durdur
            Debug.Log("Merdivenden ��k�ld� ve t�rmanma animasyonu durduruldu.");
        }
    }
}
