using UnityEngine;

public class SimpleMovementWithCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;  // Karakterin hareket hýzý
    [SerializeField] private Transform cameraTransform;  // Kamera objesini baðla
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);  // Kamera ile karakter arasýndaki mesafe
    [SerializeField] private float cameraSmoothSpeed = 0.125f;  // Kameranýn yumuþak hareket hýzý

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D bileþeni

        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform atanmadý! Lütfen kamera objesini baðlayýn.");
        }
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");  // Sað ve sol yön tuþlarý
        movement.y = Input.GetAxisRaw("Vertical");  // Yukarý ve aþaðý yön tuþlarý
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        UpdateCameraPosition();  // Kamera pozisyonunu güncelle
    }

    private void UpdateCameraPosition()
    {
        if (cameraTransform != null)
        {
            Vector3 desiredPosition = transform.position + offset;  // Hedef pozisyon
            Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, cameraSmoothSpeed);  // Yumuþak hareket
            cameraTransform.position = smoothedPosition;
        }
    }
}
