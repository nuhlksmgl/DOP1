using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    private Vector2 direction;

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Destroy(gameObject); // Okun çarpýþmadan sonra yok edilmesi
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject); // Okun yere çarpmasý
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
}
