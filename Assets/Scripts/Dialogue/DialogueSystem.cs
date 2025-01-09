using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro kütüphanesi
using Ink.Runtime;

public class DialogueSystem : MonoBehaviour
{
    public TextAsset inkJSONAsset;  // Ink dosyasını buraya bağlayacağız
    private Story story;  // Ink hikayesini burada saklıyoruz
    public TMP_Text dialogueText;  // TextMeshPro kullanarak UI metni
    public GameObject choicesContainer;  // Seçenekleri tutacak panel
    public Button choiceButtonPrefab;  // Seçenekler için düğme şablonu

    void Start()
    {
        if (inkJSONAsset != null)
        {
            story = new Story(inkJSONAsset.text);
            DisplayNextLine();
        }
        else
        {
            Debug.LogError("Ink JSON asset atanmadı.");
        }
    }

    void DisplayNextLine()
    {
        if (story.canContinue)
        {
            dialogueText.text = story.Continue().Trim();  // Trim boşlukları temizler
            DisplayChoices();
        }
        else
        {
            dialogueText.text = "Diyalog sona erdi.";
            ClearChoices();
        }
    }

    void DisplayChoices()
    {
        ClearChoices();  // Önceki seçenekleri temizle
        foreach (Choice choice in story.currentChoices)
        {
            Button button = Instantiate(choiceButtonPrefab, choicesContainer.transform);
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = choice.text;
            }
            button.onClick.AddListener(() => OnChoiceSelected(choice));
        }
    }

    void ClearChoices()
    {
        foreach (Transform child in choicesContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void OnChoiceSelected(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        DisplayNextLine();
    }
}