using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField] private UIInventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;

    private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();
    
    /// <param name="inventorySize">Envanterdeki slot sayısı</param>
    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            // Prefab oluşturulur ve içerik paneline eklenir.
            UIInventoryItem uiItem = Instantiate(itemPrefab, contentPanel);
            listOfUIItems.Add(uiItem);

            // Olaylara abonelik
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }
    
    private void HandleBeginDrag(UIInventoryItem item)
    {
        Debug.Log($"Begin Drag: {item.name}");
    }

  
    private void HandleSwap(UIInventoryItem item)
    {
        Debug.Log($"Item Dropped On: {item.name}");
    }

  
    private void HandleEndDrag(UIInventoryItem item)
    {
        Debug.Log($"End Drag: {item.name}");
    }

  
    private void HandleShowItemActions(UIInventoryItem item)
    {
        Debug.Log($"Right Clicked: {item.name}");
    }

  
    private void HandleItemSelection(UIInventoryItem item)
    {
        Debug.Log($"Item Selected: {item.name}");
        Debug.Log("geliyor");
    }


    public void Show()
    {
        gameObject.SetActive(true);
    }

   
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
