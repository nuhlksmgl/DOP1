using UnityEngine;
using UnityEngine.UI;

public class PlayerHunger : MonoBehaviour
{
    [Header("Hunger Settings")]
    [SerializeField] private int maxHunger = 100;
    private int currentHunger;

    [Header("Hunger Decay")]
    [SerializeField] private float hungerDecayInterval = 1f; // A�l�k azalmas�n�n ger�ekle�ece�i saniye aral���
    [SerializeField] private int hungerDecayAmount = 1; // Her intervalde azalacak a�l�k miktar�
    private float hungerTimer;

    [Header("UI Elements")]
    [SerializeField] private Image hungerBarImage; // A�l�k g�rselini ba�layaca��m�z alan

    private void Start()
    {
        currentHunger = maxHunger;
        UpdateHungerBar();
    }

    private void Update()
    {
        // Zaman ge�tik�e a�l��� azalt
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
        currentHunger = Mathf.Max(currentHunger, 0); // A�l�k s�f�r�n alt�na inmesin
        Debug.Log("Player hunger: " + currentHunger);
        UpdateHungerBar();

        if (currentHunger <= 0)
        {
            HandleStarvation();
        }
    }

    public void IncreaseHunger(int amount)
    {
        currentHunger = Mathf.Min(currentHunger + amount, maxHunger); // A�l�k maksimumun �st�ne ��kmas�n
        Debug.Log("Player ate food. Current hunger: " + currentHunger);
        UpdateHungerBar();
    }

    private void UpdateHungerBar()
    {
        if (hungerBarImage != null)
        {
            // Doluluk seviyesini a�l�k miktar�na g�re g�ncelle
            hungerBarImage.fillAmount = (float)currentHunger / maxHunger;
        }
    }

    private void HandleStarvation()
    {
        Debug.Log("Player is starving!");
        // A�l�ktan kaynakl� etkiler burada uygulanabilir
    }
}