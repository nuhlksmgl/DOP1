using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI Settings")]
    [SerializeField] private GameObject healthBarCanvas; // Canvas objesi (World Space olmal�)
    [SerializeField] private Image healthBarFill; // Doldurulabilir can bar� (Image)

    private Camera mainCamera;

    private void Awake()
    {
        // Ba�lang�� can� ayarla
        currentHealth = maxHealth;

        // Can bar�n� tam dolu olarak ayarla
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = 1f;
        }

        // Ana kameray� referans olarak al
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Can bar�n�n kameraya d�n�k olmas�n� sa�la
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.LookAt(mainCamera.transform);
            healthBarCanvas.transform.Rotate(0, 180, 0); // Bar�n ters d�nmesini engelle
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);

        // Can bar�n� g�ncelle
        if (healthBarFill != null)
        {
            float healthPercentage = (float)currentHealth / maxHealth; // Can oran�n� hesapla
            healthBarFill.fillAmount = healthPercentage;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy is Dead.");
        Destroy(gameObject); // D��man� yok et
    }
}