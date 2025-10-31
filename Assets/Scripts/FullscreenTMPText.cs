using System;
using System.Collections;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class FullscreenTMPText : MonoBehaviour
{
    [Header("Параметры текста")]
    [TextArea(3, 10)]
    public string defaultMessage = "Пример текста";
    public Color textColor = Color.white;
    public float fontSize = 80f;
    public float fadeDuration = 1f;
    public TMP_FontAsset font;

    [Header("Фон")]
    [Tooltip("Цвет заднего фона")]
    public Color backgroundColor = new Color(0f, 0f, 0f, 1f);

    [Header("Звук")]
    public AudioClip fadeSound;
    [Range(0f, 1f)] public float soundVolume = 1f;

    [Header("Other")] public GameObject textClick;

    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private TextMeshProUGUI tmpText;
    private AudioSource audioSource;
    private bool isVisible = false;
    private PlayerControlls playerControlls;

    private Image backgroundImage; // новый элемент — фон

    void Awake()
    {
        // Создаём Canvas
        canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1; // поверх всего

        // Добавляем CanvasGroup для плавного фейда
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        // === СОЗДАЁМ ФОН ===
        GameObject bgObj = new GameObject("Background");
        bgObj.transform.SetParent(transform, false);

        RectTransform bgRect = bgObj.AddComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;

        backgroundImage = bgObj.AddComponent<Image>();
        backgroundImage.color = backgroundColor;
        backgroundImage.raycastTarget = false;

        // === СОЗДАЁМ ТЕКСТ ===
        GameObject textObj = new GameObject("FullscreenTMPText");
        textObj.transform.SetParent(transform, false);

        RectTransform rect = textObj.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        tmpText = textObj.AddComponent<TextMeshProUGUI>();
        tmpText.alignment = TextAlignmentOptions.Center;
        tmpText.fontSize = fontSize;
        tmpText.color = textColor;
        tmpText.text = "";
        tmpText.font = font;

        // === ИСТОЧНИК ЗВУКА ===
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = soundVolume;

        // === ИНИЦИАЛИЗАЦИЯ INPUT SYSTEM ===
        playerControlls = new PlayerControlls();
    }

    private void OnEnable()
    {
        playerControlls.Enable();
        playerControlls.UI.LBM.performed += Check;
    }

    private void OnDisable()
    {
        playerControlls.UI.LBM.performed -= Check;
        playerControlls.Disable();
    }

    private void Check(InputAction.CallbackContext ctx)
    {
        if (isVisible)
        {
            OnActivate?.Invoke();
            textClick.SetActive(false);
            StartCoroutine(FadeOut());

            if (GameState.State == 5)
            {
                G.Map.Animator.SetTrigger("1-1");
            }
        }
    }

    /// <summary>
    /// Показать текст на экране
    /// </summary>
    public void ShowText(string message)
    {
        textClick.SetActive(true);
        StopAllCoroutines();
        tmpText.text = string.IsNullOrEmpty(message) ? defaultMessage : message;
        canvasGroup.alpha = 1f;
        isVisible = true;
    }

    private IEnumerator FadeOut()
    {
        isVisible = false;

        if (fadeSound && audioSource)
            audioSource.PlayOneShot(fadeSound, soundVolume);

        float t = 0f;
        float startAlpha = canvasGroup.alpha;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(startAlpha, 0f, t / fadeDuration);
            canvasGroup.alpha = a;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        tmpText.text = "";
    }
    
    public Action OnActivate;
}
