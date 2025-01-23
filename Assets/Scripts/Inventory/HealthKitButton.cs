using UnityEngine;
using UnityEngine.UI;

public class HealthKitButton : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        GetComponent<Button>().onClick.AddListener(UseHealthKit);
    }

    private void UseHealthKit()
    {
        FindObjectOfType<PlayerHealth>().Heal(50);
        inventory.RemoveItem("HealthKit");
    }
}
