using System;

public static class LocalizationEvents
{
    public static Action onLanguageChanged;
    public static void InvokeLanguageChanged() => onLanguageChanged?.Invoke();
}