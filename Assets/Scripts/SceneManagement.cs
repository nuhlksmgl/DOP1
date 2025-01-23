using UnityEngine;
using UnityEngine.SceneManagement; // Sahne de�i�tirme i�in gerekli

public class SceneManagement : MonoBehaviour
{
    private bool isPlayerNear = false; // Oyuncunun tetikleyiciye yak�n olup olmad���n� kontrol etmek i�in

    // Oyuncu trigger alan�na girince
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // E�er Player tag'ine sahip bir obje ile �arp���yorsa
        {
            isPlayerNear = true;
            Debug.Log("Oyuncu tetikleyiciye yakla�t�."); // Test i�in log ekleyelim
        }
    }

    // Oyuncu trigger alan�ndan ��k�nca
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // E�er Player tag'ine sahip bir obje alan� terk ederse
        {
            isPlayerNear = false;
            Debug.Log("Oyuncu tetikleyiciden uzakla�t�."); // Test i�in log ekleyelim
        }
    }

    void Update()
    {
        // E�er oyuncu yak�ndaysa ve Enter tu�una bas�lm��sa
        if (isPlayerNear && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Enter tu�una bas�ld�, sahne de�i�iyor..."); // Test i�in log ekleyelim
            SceneManager.LoadScene("stage2"); // Buraya ge�i� yap�lacak sahne ad�n� yaz
        }
    }
}
