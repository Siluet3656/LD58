using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class DeveloperIntro : MonoBehaviour
{
    [Header("Display Settings")]
    public float screenDuration = 3f;
    public float fadeDuration = 1f;
    public Color textColor = Color.white;
    [SerializeField] private GameObject languageSelectionPanel;
    
    [Header("Font Settings")]
    public TMP_FontAsset fontAsset;
    public int fontSize = 72;
    [Range(0, 1)] public float fontSpacing = 0.1f;

    private CanvasGroup canvasGroup;
    private TextMeshProUGUI textComponent;
    private Canvas canvas;

    private string firstScreenText;
    private string secondScreenText;
    private string thirdScreenText;

    public RandomSoundPlayer rsp;
    private async void Start()
    {
        // 1. Показываем панель выбора языка
        languageSelectionPanel.SetActive(true);
        
        // 2. Ждем выбора языка асинхронно
        string selectedLanguage = await WaitForLanguageSelectionAsync();
        
        // 3. Устанавливаем язык
        LocalizationManager.Instance.LoadLanguage(selectedLanguage);
        
        // 4. Инициализируем текст
        InitializeLocalizedText();
        
        rsp.PlayRandomSound();
        
        // 5. Создаем UI и запускаем последовательность
        CreateUIElements();
        StartCoroutine(ShowScreensSequence());
    }
    
    private Task<string> WaitForLanguageSelectionAsync()
    {
        var tcs = new TaskCompletionSource<string>();
        
        // Получаем кнопки
        Button[] buttons = languageSelectionPanel.GetComponentsInChildren<Button>();
        
        foreach (Button button in buttons)
        {
            // Определяем язык по имени кнопки или компоненту
            string languageCode = button.name; // или button.GetComponent<LanguageButton>().LanguageCode
            
            button.onClick.AddListener(() =>
            {
                languageSelectionPanel.SetActive(false);
                tcs.TrySetResult(languageCode);
            });
        }
        
        return tcs.Task;
    }
    
    private void InitializeLocalizedText()
    {
        firstScreenText = LocalizationManager.Instance.Get("firstScreenText");
        secondScreenText = LocalizationManager.Instance.Get("secondScreenText");
        thirdScreenText = LocalizationManager.Instance.Get("thirdScreenText");
    }

    void CreateUIElements()
    {
        // Create canvas (without GraphicRaycaster)
        GameObject canvasGO = new GameObject("IntroCanvas");
        canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        // Canvas scaler for responsiveness
        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        // Background (with raycastTarget disabled)
        GameObject bgGO = new GameObject("Background");
        bgGO.transform.SetParent(canvasGO.transform);
        Image bg = bgGO.AddComponent<Image>();
        bg.color = Color.black;
        bg.raycastTarget = false; // Disable click blocking
        SetFullRect(bg.rectTransform);

        // Text (with raycastTarget disabled)
        GameObject textGO = new GameObject("IntroText");
        textGO.transform.SetParent(canvasGO.transform);
        textComponent = textGO.AddComponent<TextMeshProUGUI>();
        textComponent.color = textColor;
        textComponent.font = fontAsset;
        textComponent.fontSize = fontSize;
        textComponent.characterSpacing = fontSpacing;
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.verticalAlignment = VerticalAlignmentOptions.Middle;
        textComponent.raycastTarget = false; // Disable click blocking
        SetFullRect(textComponent.rectTransform);

        // Canvas group for fade effects
        canvasGroup = canvasGO.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false; // Crucial: disable input blocking
    }

    void SetFullRect(RectTransform rect)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    IEnumerator ShowScreensSequence()
    {
        // Initial black screen
        yield return new WaitForSeconds(0.5f);

        // First screen
        textComponent.text = firstScreenText;
        yield return Fade(0, 1); // Fade in
        yield return new WaitForSeconds(screenDuration);
        yield return Fade(1, 0); // Fade out

        // Second screen
        textComponent.text = secondScreenText;
        yield return Fade(0, 1); // Fade in
        yield return new WaitForSeconds(screenDuration);
        yield return Fade(1, 0); // Fade out
        
        textComponent.text = thirdScreenText;
        yield return Fade(0, 1); // Fade in
        yield return new WaitForSeconds(screenDuration);
        yield return Fade(1, 0); // Fade out

        // Final delay before destruction
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}