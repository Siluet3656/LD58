﻿using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeveloperIntro : MonoBehaviour
{
    [Header("Display Settings")]
    public float screenDuration = 3f;
    public float fadeDuration = 1f;
    public Color textColor = Color.white;

    [Header("Content")]
    [TextArea]
    public string firstScreenText = "Studio";
    [TextArea]
    public string secondScreenText = "Developer Name";
    [TextArea]
    public string thirdScreenText = "Something";
    
    [Header("Font Settings")]
    public TMP_FontAsset fontAsset;
    public int fontSize = 72;
    [Range(0, 1)] public float fontSpacing = 0.1f;

    private CanvasGroup canvasGroup;
    private TextMeshProUGUI textComponent;
    private Canvas canvas;


    public GameObject bg;
    void Start()
    {
        CreateUIElements();
        StartCoroutine(ShowScreensSequence());
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