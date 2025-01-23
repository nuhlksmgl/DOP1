using UnityEngine;
using UnityEngine.SceneManagement; // Sahne deðiþtirme için gerekli

public class SceneManagement : MonoBehaviour
{
    private bool isPlayerNear = false; // Oyuncunun tetikleyiciye yakýn olup olmadýðýný kontrol etmek için

    // Oyuncu trigger alanýna girince
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Eðer Player tag'ine sahip bir obje ile çarpýþýyorsa
        {
            isPlayerNear = true;
            Debug.Log("Oyuncu tetikleyiciye yaklaþtý."); // Test için log ekleyelim
        }
    }

    // Oyuncu trigger alanýndan çýkýnca
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Eðer Player tag'ine sahip bir obje alaný terk ederse
        {
            isPlayerNear = false;
            Debug.Log("Oyuncu tetikleyiciden uzaklaþtý."); // Test için log ekleyelim
        }
    }

    void Update()
    {
        // Eðer oyuncu yakýndaysa ve Enter tuþuna basýlmýþsa
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Enter tuþuna basýldý, sahne deðiþiyor..."); // Test için log ekleyelim
            SceneManager.LoadScene("stage2"); // Buraya geçiþ yapýlacak sahne adýný yaz
        }
    }
}
