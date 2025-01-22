using UnityEngine;

public class ArcherBedeviHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("ArcherBedevi health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("ArcherBedevi died.");
        Destroy(gameObject); // ArcherBedevi'nin yok edilmesi
    }
}
