using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    [Header("Ladder Settings")]
    [SerializeField] private float climbSpeed = 8f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isLadder = false;
    private bool isClimbing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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
                animator.SetBool("ladderClimb", false); // Yürüyüþ animasyonuna geç
                Debug.Log("Týrmanma animasyonu durduruldu.");
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
            rb.gravityScale = 4f;
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
            if (animator != null)
            {
                animator.SetBool("ladderClimb", false); // Merdivenden çýkýnca animasyonu durdur
                Debug.Log("Merdivenden çýkýldý ve týrmanma animasyonu durduruldu.");
            }
        }
    }
}
