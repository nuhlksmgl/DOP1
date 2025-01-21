using UnityEngine;

public class SimpleMovementWithCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;  // Karakterin hareket hızı
    [SerializeField] private Transform cameraTransform;  // Kamera objesini bağla

    private Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D bileşeni
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");  // Sağ ve sol yön tuşları
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
        UpdateCameraPosition();  // Kamera pozisyonunu güncelle
    }

    private void UpdateCameraPosition()
    {
        Vector3 cameraPosition = cameraTransform.position;
        cameraPosition.x = transform.position.x;  // Kamera x pozisyonu, karakterin x pozisyonuna eşitlenir
        cameraTransform.position = cameraPosition;
    }
}