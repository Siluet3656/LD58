using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    [SerializeField] private LocalizationDB database;

    private Dictionary<string, string> dict;
    public string CurrentLanguage = "en";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLanguage(CurrentLanguage);
        }
        else
            Destroy(gameObject);
    }

    public void LoadLanguage(string lang)
    {
        CurrentLanguage = lang;
        dict = new Dictionary<string, string>();

        foreach (var e in database.entries)
        {
            string text = lang == "ru" ? e.ru : e.en;
            dict[e.key] = text;
        }

        LocalizationEvents.InvokeLanguageChanged();
    }

    public string Get(string key)
    {
        return dict.TryGetValue(key, out var value) ? value : key;
    }
}