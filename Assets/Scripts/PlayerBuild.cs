using System.Collections.Generic;

[System.Serializable]
public class PlayerBuild
{
    public List<string> souls = new List<string>();

    // Обычная сериализация (для логов)
    public string Serialize()
    {
        return string.Join(",", souls);
    }

    // Сериализация для GameAnalytics (только допустимые символы)
    public string SerializeForGA()
    {
        // Заменяем запрещенные символы
        var sanitized = new List<string>();
        foreach (var t in souls)
        {
            string s = t.Replace(" ", "_")
                .Replace(",", "-")
                .Replace(":", "-");
            sanitized.Add(s);
        }
        return string.Join("-", sanitized);
    }
}