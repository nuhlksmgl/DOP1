using UnityEngine;

public class SimpleMovementWithCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;  // Karakterin hareket h�z�
    [SerializeField] private Transform cameraTransform;  // Kamera objesini ba�la
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);  // Kamera ile karakter aras�ndaki mesafe
    [SerializeField] private float cameraSmoothSpeed = 0.125f;  // Kameran�n yumu�ak hareket h�z�

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D bile�eni

        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform atanmad�! L�tfen kamera objesini ba�lay�n.");
        }
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");  // Sa� ve sol y�n tu�lar�
        movement.y = Input.GetAxisRaw("Vertical");  // Yukar� ve a�a�� y�n tu�lar�
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        UpdateCameraPosition();  // Kamera pozisyonunu g�ncelle
    }

    private void UpdateCameraPosition()
    {
        if (cameraTransform != null)
        {
            Vector3 desiredPosition = transform.position + offset;  // Hedef pozisyon
            Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, cameraSmoothSpeed);  // Yumu�ak hareket
            cameraTransform.position = smoothedPosition;
        }
    }
}
