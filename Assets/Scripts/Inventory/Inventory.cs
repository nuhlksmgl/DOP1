using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject HealthKitPanel;
    public GameObject HamburgerPanel;
    public TMP_Text healthKitText;
    public TMP_Text hamburgerText;
    public GameObject inventoryUI;
    public KeyCode toggleKey = KeyCode.E;

    private void Start()
    {
        HealthKitPanel.SetActive(false);
        HamburgerPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    public void AddItem(InventoryItem newItem)
    {
        if (newItem.itemName == "HealthKit")
        {
            HealthKitPanel.SetActive(true);
            int qty = int.Parse(healthKitText.text.Substring(12, 1)); // "HealthKit (0)" ifadesinden miktarý alýr (parantez sayýsýný düzelttik)
            qty += newItem.quantity;
            healthKitText.text = $"HealthKit ({qty})";
        }
        else if (newItem.itemName == "Hamburger")
        {
            HamburgerPanel.SetActive(true);
            int qty = int.Parse(hamburgerText.text.Substring(12, 1)); // "Hamburger (0)" ifadesinden miktarý alýr (parantez sayýsýný düzelttik)
            qty += newItem.quantity;
            hamburgerText.text = $"Hamburger ({qty})";
        }
    }

    public void RemoveItem(string itemName)
    {
        if (itemName == "HealthKit")
        {
            int qty = int.Parse(healthKitText.text.Substring(12, 1)); // "HealthKit (1)" ifadesinden miktarý alýr (parantez sayýsýný düzelttik)
            qty--;
            if (qty <= 0)
            {
                HealthKitPanel.SetActive(false);
            }
            healthKitText.text = $"HealthKit ({qty})";
        }
        else if (itemName == "Hamburger")
        {
            int qty = int.Parse(hamburgerText.text.Substring(12, 1)); // "Hamburger (1)" ifadesinden miktarý alýr (parantez sayýsýný düzelttik)
            qty--;
            if (qty <= 0)
            {
                HamburgerPanel.SetActive(false);
            }
            hamburgerText.text = $"Hamburger ({qty})";
        }
    }
}
