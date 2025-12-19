using UnityEngine;
using System.IO;

public static class SaveManager
{
    private const string SAVE_KEY = "GAME_SAVE";
    private const string FILE_NAME = "save.json";

    // ===== SAVE =====
    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);

#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
#else
        string path = GetFilePath();
        File.WriteAllText(path, json);
#endif
    }

    // ===== LOAD =====
    public static SaveData Load()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return null;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        return JsonUtility.FromJson<SaveData>(json);
#else
        string path = GetFilePath();
        if (!File.Exists(path))
            return null;

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
#endif
    }

    // ===== UTIL =====
    private static string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, FILE_NAME);
    }

    public static bool HasSave()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        return PlayerPrefs.HasKey(SAVE_KEY);
#else
        return File.Exists(GetFilePath());
#endif
    }

    public static void DeleteSave()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        PlayerPrefs.DeleteKey(SAVE_KEY);
#else
        if (File.Exists(GetFilePath()))
            File.Delete(GetFilePath());
#endif
    }
}