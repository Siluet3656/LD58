using UnityEngine;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string key;
    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        LocalizationEvents.onLanguageChanged += UpdateText;
    }

    void Start() => UpdateText();

    void OnDestroy() =>
        LocalizationEvents.onLanguageChanged -= UpdateText;

    void UpdateText()
    {
        text.text = LocalizationManager.Instance.Get(key);
    }
}