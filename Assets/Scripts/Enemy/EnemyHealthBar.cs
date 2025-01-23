using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI Settings")]
    [SerializeField] private GameObject healthBarCanvas; // Sa�l�k bar�n�n canvas objesi (World Space olmal�)
    [SerializeField] private Image healthBarFill; // Sa�l�k bar�n�n dolgu rengi i�in kullan�lacak Image

    private Camera mainCamera;

    private void Awake()
    {
        // Ba�lang��ta can� maksimum de�ere ayarla
        currentHealth = maxHealth;

        // Sa�l�k bar�n� tam dolu olarak ba�lat
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = 1f;
            healthBarFill.color = Color.red; // Sa�l�k bar� k�rm�z� renkte olacak
        }

        // Ana kameray� referans al
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Sa�l�k bar�n�n kameraya d�n�k olmas�n� sa�la
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.LookAt(mainCamera.transform);
            healthBarCanvas.transform.Rotate(0, 180, 0); // Yanl�� y�ne d�nmesini engelle
        }
    }

    private void LateUpdate()
    {
        // Sa�l�k bar�n� d��man�n �st�nde tut
        if (healthBarCanvas != null)
        {
            Vector3 barPosition = transform.position + new Vector3(0, 1, 0); // D��man �st�nde 1 birim yukar�da
            healthBarCanvas.transform.position = barPosition;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Sa�l�k bar�n� g�ncelle
        if (healthBarFill != null)
        {
            float healthPercentage = (float)currentHealth / maxHealth; // Sa�l�k oran�n� hesapla
            healthBarFill.fillAmount = healthPercentage; // Dolgu oran�n� ayarla
        }

        Debug.Log("Enemy Health: " + currentHealth);

        // Can s�f�ra ula��rsa d��man� yok et
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
