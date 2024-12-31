using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab; // Fixed typo
    [SerializeField] private RectTransform contentPanel;

    private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    // Initializes the inventory UI with a specified size
    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            // Instantiate the UI item and add it to the content panel
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel, false); // `false` keeps local scaling
            listOfUIItems.Add(uiItem);
        }
    }

    // Shows the inventory UI
    public void Show()
    {
        gameObject.SetActive(true); // Corrected to activate the UI
    }

    // Hides the inventory UI
    public void Hide()
    {
        gameObject.SetActive(false); // Hides the UI
    }
}