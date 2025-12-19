using UnityEngine;
using GameAnalyticsSDK;
using System.Collections.Generic;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;
    private bool initialized;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (!initialized)
        {
            GameAnalytics.Initialize();
            initialized = true;
            Debug.Log("GameAnalytics initialized");
        }
    }

    // =========================
    // LEVEL EVENTS
    // =========================

    public void LevelStarted(int levelNumber, string levelName, PlayerBuild build)
    {
        string uniqueKey = $"unique_level_started_{levelNumber}";

        // Для уникального игрока
        if (!PlayerPrefs.HasKey(uniqueKey))
        {
            GameAnalytics.NewProgressionEvent(
                GAProgressionStatus.Start,
                $"{levelNumber}_{levelName}_unique"
            );
            PlayerPrefs.SetInt(uniqueKey, 1);
            PlayerPrefs.Save();
        }

        // Для всех попыток
        GameAnalytics.NewDesignEvent($"level_started:{levelNumber}_{levelName}");

        // Логируем билд
        if (build != null)
        {
            string buildStr = build.Serialize();
            GameAnalytics.NewDesignEvent($"level_build:{levelNumber}_{levelName}:{buildStr}");
            Debug.Log($"Level started with build: {buildStr}");
        }
    }

    public void LevelCompleted(int levelNumber, string levelName, PlayerBuild build)
    {
        string uniqueKey = $"unique_level_completed_{levelNumber}";

        // Уникальное событие
        if (!PlayerPrefs.HasKey(uniqueKey))
        {
            GameAnalytics.NewProgressionEvent(
                GAProgressionStatus.Complete,
                $"{levelNumber}_{levelName}_unique"
            );
            PlayerPrefs.SetInt(uniqueKey, 1);
            PlayerPrefs.Save();
        }

        // Повторные попытки
        GameAnalytics.NewProgressionEvent(
            GAProgressionStatus.Complete,
            $"{levelNumber}_{levelName}"
        );

        // Билд
        if (build != null)
        {
            string buildStr = build.Serialize();
            GameAnalytics.NewDesignEvent($"level_build:{levelNumber}_{levelName}:{buildStr}");
            Debug.Log($"Level completed with build: {buildStr}");
        }
    }

    public void LevelFailed(int levelNumber, string levelName, PlayerBuild build)
    {
        // Повторные попытки
        GameAnalytics.NewProgressionEvent(
            GAProgressionStatus.Fail,
            $"{levelNumber}_{levelName}"
        );

        // Билд
        if (build != null)
        {
            string buildStr = build.Serialize();
            GameAnalytics.NewDesignEvent($"level_build:{levelNumber}_{levelName}:{buildStr}");
            Debug.Log($"Level failed with build: {buildStr}");
        }
    }
}
