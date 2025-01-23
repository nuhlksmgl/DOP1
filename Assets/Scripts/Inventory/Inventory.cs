using UnityEngine;
using TMPro;
using System.Collections.Generic;

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
            int qty = GetCurrentQuantity(healthKitText);
            qty += newItem.quantity;
            healthKitText.text = $"HealthKit ({qty})";
        }
        else if (newItem.itemName == "Hamburger")
        {
            HamburgerPanel.SetActive(true);
            int qty = GetCurrentQuantity(hamburgerText);
            qty += newItem.quantity;
            hamburgerText.text = $"Hamburger ({qty})";
        }
    }

    public int GetCurrentQuantity(TMP_Text textComponent)
    {
        // Parantez içindeki miktarý çýkar ve miktarý döndür
        string quantityString = textComponent.text.Substring(textComponent.text.IndexOf('(') + 1, textComponent.text.IndexOf(')') - textComponent.text.IndexOf('(') - 1);
        return int.Parse(quantityString);
    }

    public void RemoveItem(string itemName)
    {
        if (itemName == "HealthKit")
        {
            int qty = GetCurrentQuantity(healthKitText);
            qty--;
            if (qty <= 0)
            {
                HealthKitPanel.SetActive(false);
            }
            healthKitText.text = $"HealthKit ({qty})";
        }
        else if (itemName == "Hamburger")
        {
            int qty = GetCurrentQuantity(hamburgerText);
            qty--;
            if (qty <= 0)
            {
                HamburgerPanel.SetActive(false);
            }
            hamburgerText.text = $"Hamburger ({qty})";
        }
    }
}
