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
        FindObjectOfType<PlayerHunger>().IncreaseHunger(30);
        inventory.RemoveItem("Hamburger");
    }
}
