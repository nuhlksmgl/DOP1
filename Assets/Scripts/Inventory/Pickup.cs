using UnityEngine;

public class Pickup : MonoBehaviour
{
    public InventoryItem item; // Pickup edilecek itemi belirler

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) // Sað týklama ile pickup iþlemi
        {
            FindObjectOfType<Inventory>().AddItem(item);
            Destroy(gameObject); // Pickup edildikten sonra objeyi yok eder
        }
    }
}
