using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TMPTextAnimator : MonoBehaviour
{
    [Header("Typing Settings")]
    [Tooltip("Задержка между символами")]
    public float charDelay = 0.04f;

    [Tooltip("Использовать эффект печатания")]
    public bool useTypewriter = true;

    [Header("Fade Settings")]
    public bool useFadeIn = false;
    public float fadeDuration = 0.5f;

    [Header("Scale Settings")]
    public bool useScaleIn = false;
    public float scaleFrom = 0.8f;
    public float scaleDuration = 0.3f;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip startSound;

    private TMP_Text tmpText;
    private Coroutine animationCoroutine;

    private void Awake()
    {
        tmpText = GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Запуск анимации текста
    /// </summary>
    public void StartTextAnimation(string text)
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        animationCoroutine = StartCoroutine(AnimateText(text));
    }

    private IEnumerator AnimateText(string text)
    {
        tmpText.text = text;
        tmpText.ForceMeshUpdate();

        // Звук — ТОЛЬКО ОДИН РАЗ В НАЧАЛЕ
        if (audioSource && startSound)
            audioSource.PlayOneShot(startSound);

        if (useFadeIn)
            tmpText.alpha = 0;

        if (useScaleIn)
            transform.localScale = Vector3.one * scaleFrom;

        if (useTypewriter)
        {
            tmpText.maxVisibleCharacters = 0;
            int totalChars = tmpText.textInfo.characterCount;

            for (int i = 0; i <= totalChars; i++)
            {
                tmpText.maxVisibleCharacters = i;
                yield return new WaitForSeconds(charDelay);
            }
        }
        else
        {
            tmpText.maxVisibleCharacters = tmpText.textInfo.characterCount;
        }

        // Fade In
        if (useFadeIn)
        {
            float t = 0;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                tmpText.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
                yield return null;
            }
            tmpText.alpha = 1;
        }

        // Scale In
        if (useScaleIn)
        {
            float t = 0;
            while (t < scaleDuration)
            {
                t += Time.deltaTime;
                transform.localScale = Vector3.Lerp(
                    Vector3.one * scaleFrom,
                    Vector3.one,
                    t / scaleDuration
                );
                yield return null;
            }
            transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// Мгновенно показать текст без анимации
    /// </summary>
    public void ShowInstant(string text)
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);

        tmpText.text = text;
        tmpText.maxVisibleCharacters = int.MaxValue;
        tmpText.alpha = 1;
        transform.localScale = Vector3.one;
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
}
