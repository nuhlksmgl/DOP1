using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI Settings")]
    [SerializeField] private GameObject healthBarCanvas; // Saðlýk barýnýn canvas objesi (World Space olmalý)
    [SerializeField] private Image healthBarFill; // Saðlýk barýnýn dolgu rengi için kullanýlacak Image

    private Camera mainCamera;

    private void Awake()
    {
        // Baþlangýçta caný maksimum deðere ayarla
        currentHealth = maxHealth;

        // Saðlýk barýný tam dolu olarak baþlat
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = 1f;
            healthBarFill.color = Color.red; // Saðlýk barý kýrmýzý renkte olacak
        }

        // Ana kamerayý referans al
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Saðlýk barýnýn kameraya dönük olmasýný saðla
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.LookAt(mainCamera.transform);
            healthBarCanvas.transform.Rotate(0, 180, 0); // Yanlýþ yöne dönmesini engelle
        }
    }

    private void LateUpdate()
    {
        // Saðlýk barýný düþmanýn üstünde tut
        if (healthBarCanvas != null)
        {
            Vector3 barPosition = transform.position + new Vector3(0, 1, 0); // Düþman üstünde 1 birim yukarýda
            healthBarCanvas.transform.position = barPosition;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Saðlýk barýný güncelle
        if (healthBarFill != null)
        {
            float healthPercentage = (float)currentHealth / maxHealth; // Saðlýk oranýný hesapla
            healthBarFill.fillAmount = healthPercentage; // Dolgu oranýný ayarla
        }

        Debug.Log("Enemy Health: " + currentHealth);

        // Can sýfýra ulaþýrsa düþmaný yok et
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
