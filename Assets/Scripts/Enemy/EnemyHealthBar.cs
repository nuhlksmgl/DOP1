using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI Settings")]
    [SerializeField] private GameObject healthBarCanvas; // Canvas objesi (World Space olmalý)
    [SerializeField] private Image healthBarFill; // Doldurulabilir can barý (Image)

    private Camera mainCamera;

    private void Awake()
    {
        // Baþlangýç caný ayarla
        currentHealth = maxHealth;

        // Can barýný tam dolu olarak ayarla
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = 1f;
        }

        // Ana kamerayý referans olarak al
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Can barýnýn kameraya dönük olmasýný saðla
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.LookAt(mainCamera.transform);
            healthBarCanvas.transform.Rotate(0, 180, 0); // Barýn ters dönmesini engelle
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);

        // Can barýný güncelle
        if (healthBarFill != null)
        {
            float healthPercentage = (float)currentHealth / maxHealth; // Can oranýný hesapla
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
        Destroy(gameObject); // Düþmaný yok et
    }
}