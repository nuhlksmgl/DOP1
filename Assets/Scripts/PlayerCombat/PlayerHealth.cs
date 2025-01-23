using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("UI Elements")]
    [SerializeField] private Image healthBarImage; // Saðlýk görselini baðlayacaðýmýz alan

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player health: " + currentHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Player healed. Current health: " + currentHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            // Doluluk seviyesini can miktarýna göre güncelle
            healthBarImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Player died.");
        // Ölüm mekanizmasý
    }
}