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
        // Hamburger kullanýmý için gerekli kodlar
        // Örneðin, Player'ýn enerjisini arttýrabilir
        inventory.RemoveItem("Hamburger");
    }
}
