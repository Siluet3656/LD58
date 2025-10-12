using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
public class OverlayHighlightBlink : MonoBehaviour
{
    [Header("Blink Settings")]
    [Tooltip("Минимальная интенсивность свечения.")]
    public float minIntensity = 0.8f;

    [Tooltip("Максимальная интенсивность свечения.")]
    public float maxIntensity = 2.0f;

    [Tooltip("Скорость мигания (в герцах, т.е. 1 = один цикл в секунду).")]
    public float blinkSpeed = 1.0f;

    [Tooltip("Сдвиг фазы, если несколько оверлеев должны мигать не синхронно.")]
    public float phaseOffset = 0f;

    [Tooltip("Плавная кривая изменения яркости (по умолчанию — синус).")]
    public AnimationCurve blinkCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Image overlayImage;
    private Material overlayMaterial;
    private float timeAccumulator;

    void OnEnable()
    {
        overlayImage = GetComponent<Image>();
        if (overlayImage.material == null)
        {
            Debug.LogWarning("OverlayHighlightBlink: У Image нет материала! Назначь материал с шейдером UI/TransparentHoleGlow.");
            enabled = false;
            return;
        }

        // Создаём отдельный инстанс материала, чтобы не влиять на другие Image
        overlayMaterial = Application.isPlaying ? Instantiate(overlayImage.material) : overlayImage.material;
        overlayImage.material = overlayMaterial;
    }

    void Update()
    {
        if (overlayMaterial == null)
            return;

        timeAccumulator += Time.deltaTime * blinkSpeed;
        float t = Mathf.Repeat(timeAccumulator + phaseOffset, 1f);

        // Получаем значение от 0 до 1 через кривую (по сути, плавная синусоида)
        float pulse = blinkCurve.Evaluate(Mathf.Sin(t * Mathf.PI * 2) * 0.5f + 0.5f);

        float intensity = Mathf.Lerp(minIntensity, maxIntensity, pulse);

        overlayMaterial.SetFloat("_HighlightIntensity", intensity);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying)
            Update();
    }
#endif
}
