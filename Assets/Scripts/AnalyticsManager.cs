using System.Collections;
using UnityEngine;
using GameAnalyticsSDK;

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
#if UNITY_WEBGL
            Debug.Log("Initializing GameAnalytics for WebGL");
            GameAnalytics.Initialize();
#elif UNITY_STANDALONE_WIN
            Debug.Log("Initializing GameAnalytics for Windows");
            GameAnalytics.Initialize();
#endif

            initialized = true;
            Debug.Log("GameAnalytics initialized");
            StartCoroutine(SendEventWhenReady());
        }
    }
    
    IEnumerator SendEventWhenReady()
    {
        while (!GameAnalytics.Initialized)
            yield return null;

        GameAnalytics.NewDesignEvent("test_event_initialized");
        Debug.Log("GA test event sent");
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
            string buildStr = build.SerializeForGA();
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
    }
}
