using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public GameObject deathPanel;

    private void Start()
    {
        currentHealth = maxHealth;
        if (deathPanel != null)
        {
            deathPanel.SetActive(false); // Ba�lang��ta kapal�
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died.");
        if (deathPanel != null)
        {
            deathPanel.SetActive(true); // Player �ld���nde panel a��l�r
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Sahneyi yeniden y�kle
    }
}
