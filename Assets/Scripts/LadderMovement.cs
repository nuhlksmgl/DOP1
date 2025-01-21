using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    [Header("Ladder Settings")]
    [SerializeField] private float climbSpeed = 8f;

    [Header("Ground Settings")]
    [SerializeField] private Collider2D groundCollider;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isLadder = false;
    private bool isClimbing = false;
    private float originalGravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalGravity = rb.gravityScale;

        if (animator == null)
        {
            Debug.LogError("Animator bileþeni atanmadý! Lütfen bir Animator bileþeni ekleyin.");
        }
    }

    private void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        if (isLadder && Mathf.Abs(verticalInput) > 0f)
        {
            isClimbing = true;
            if (animator != null)
            {
                animator.SetBool("ladderClimb", true); // Týrmanma animasyonunu baþlat
                Debug.Log("Týrmanma animasyonu baþlatýldý.");
            }
        }
        else if (!isLadder || Mathf.Abs(verticalInput) == 0f)
        {
            isClimbing = false;
            if (animator != null)
            {
                animator.SetBool("ladderClimb", false); // Týrmanma animasyonunu durdur
                Debug.Log("Týrmanma animasyonu durduruldu.");
            }
        }

        // `S` tuþuna basýldýðýnda ground collider'ý trigger olarak iþaretle
        if (isLadder && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W)))
        {
            if (groundCollider != null)
            {
                groundCollider.isTrigger = true;
                Debug.Log("Ground collider trigger olarak iþaretlendi.");
            }
        }
        else if (!isLadder)
        {
            if (groundCollider != null)
            {
                groundCollider.isTrigger = false;
                Debug.Log("Ground collider normal hale getirildi.");
            }
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed);
        }
        else
        {
            rb.gravityScale = originalGravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            rb.gravityScale = originalGravity;
            if (animator != null)
            {
                animator.SetBool("ladderClimb", false); // Merdivenden çýkýnca animasyonu durdur
                Debug.Log("Merdivenden çýkýldý ve týrmanma animasyonu durduruldu.");
            }
        }
    }
}
