using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float _fadeDuration = 1f;          
    [SerializeField] private Color _fadeColor = Color.black;    

    private Image _fadeImage;
    private bool _isFading = false;

    void Awake()
    {
        G.ScreenFader = this;
        
        _fadeImage = GetComponent<Image>();
        if (_fadeImage == null)
        {
            _fadeImage = gameObject.AddComponent<Image>();
        }

        _fadeImage.color = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, 1f);
        _fadeImage.rectTransform.anchorMin = Vector2.zero;
        _fadeImage.rectTransform.anchorMax = Vector2.one;
        _fadeImage.rectTransform.offsetMin = Vector2.zero;
        _fadeImage.rectTransform.offsetMax = Vector2.zero;
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        if (_isFading) yield break;
        _isFading = true;

        float timer = 0f;
        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / _fadeDuration);
            _fadeImage.color = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, alpha);
            yield return null;
        }

        _fadeImage.color = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, 0f);
        _fadeImage.gameObject.SetActive(false);

        _isFading = false;
    }

    public IEnumerator FadeOut()
    {
        if (_isFading) yield break;
        _isFading = true;

        _fadeImage.gameObject.SetActive(true);
        float timer = 0f;

        while (timer < _fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / _fadeDuration);
            _fadeImage.color = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, alpha);
            yield return null;
        }

        _fadeImage.color = new Color(_fadeColor.r, _fadeColor.g, _fadeColor.b, 1f);
        _isFading = false;
    }
}
