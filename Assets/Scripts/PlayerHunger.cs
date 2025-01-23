using UnityEngine;
using UnityEngine.UI;

public class PlayerHunger : MonoBehaviour
{
    [Header("Hunger Settings")]
    [SerializeField] private int maxHunger = 100;
    private int currentHunger;

    [Header("Hunger Decay")]
    [SerializeField] private float hungerDecayInterval = 1f; // Açlýk azalmasýnýn gerçekleþeceði saniye aralýðý
    [SerializeField] private int hungerDecayAmount = 1; // Her intervalde azalacak açlýk miktarý
    private float hungerTimer;

    [Header("UI Elements")]
    [SerializeField] private Image hungerBarImage; // Açlýk görselini baðlayacaðýmýz alan

    private void Start()
    {
        currentHunger = maxHunger;
        UpdateHungerBar();
    }

    private void Update()
    {
        // Zaman geçtikçe açlýðý azalt
        hungerTimer += Time.deltaTime;

        if (hungerTimer >= hungerDecayInterval)
        {
            hungerTimer = 0f;
            DecreaseHunger(hungerDecayAmount);
        }
    }

    public void DecreaseHunger(int amount)
    {
        currentHunger -= amount;
        currentHunger = Mathf.Max(currentHunger, 0); // Açlýk sýfýrýn altýna inmesin
        Debug.Log("Player hunger: " + currentHunger);
        UpdateHungerBar();

        if (currentHunger <= 0)
        {
            HandleStarvation();
        }
    }

    public void IncreaseHunger(int amount)
    {
        currentHunger = Mathf.Min(currentHunger + amount, maxHunger); // Açlýk maksimumun üstüne çýkmasýn
        Debug.Log("Player ate food. Current hunger: " + currentHunger);
        UpdateHungerBar();
    }

    private void UpdateHungerBar()
    {
        if (hungerBarImage != null)
        {
            // Doluluk seviyesini açlýk miktarýna göre güncelle
            hungerBarImage.fillAmount = (float)currentHunger / maxHunger;
        }
    }

    private void HandleStarvation()
    {
        Debug.Log("Player is starving!");
        // Açlýktan kaynaklý etkiler burada uygulanabilir
    }
}