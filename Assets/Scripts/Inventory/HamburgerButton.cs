using UnityEngine;
using UnityEngine.UI;

public class HamburgerButton : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        GetComponent<Button>().onClick.AddListener(UseHamburger);
    }

    private void UseHamburger()
    {
        // Hamburger kullan�m� i�in gerekli kodlar
        // �rne�in, Player'�n enerjisini artt�rabilir
        inventory.RemoveItem("Hamburger");
    }
}
