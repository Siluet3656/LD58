using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
public class OverlayHoleAuto : MonoBehaviour
{
    [Header("Hole Settings")]
    [Tooltip("UI-элемент, под который будет подстраиваться прозрачное окно.")]
    public RectTransform target;

    [Tooltip("Дополнительный масштаб окна (1 = ровно по элементу).")]
    [Range(0.8f, 2f)]
    public float holeScale = 1.0f;

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
    private Canvas canvas;
    private Camera uiCamera;
    private float timeAccumulator;

    private static readonly int HoleCenter = Shader.PropertyToID("_HoleCenter");
    private static readonly int HoleSize = Shader.PropertyToID("_HoleSize");
    private static readonly int HighlightIntensity = Shader.PropertyToID("_HighlightIntensity");

    void OnEnable()
    {
        overlayImage = GetComponent<Image>();
        canvas = overlayImage.canvas;

        if (overlayImage.material == null)
        {
            Debug.LogWarning("OverlayHoleWithHighlight: У Image нет материала! Назначь материал с шейдером UI/TransparentHoleGlow.");
            enabled = false;
            return;
        }

        // создаём инстанс материала, чтобы не портить оригинал
        overlayMaterial = Application.isPlaying ? Instantiate(overlayImage.material) : overlayImage.material;
        overlayImage.material = overlayMaterial;

        // если Canvas в режиме Screen Space - Camera, берём ссылку на камеру
        if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceCamera)
            uiCamera = canvas.worldCamera;
    }

    void Update()
    {
        if (overlayMaterial == null || canvas == null)
            return;

        if (target != null)
            UpdateHole();

        UpdateBlink();
    }

    private void UpdateHole()
    {
        Vector3[] corners = new Vector3[4];
        target.GetWorldCorners(corners);

        Vector2 min, max;

        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && uiCamera != null)
        {
            min = RectTransformUtility.WorldToScreenPoint(uiCamera, corners[0]);
            max = RectTransformUtility.WorldToScreenPoint(uiCamera, corners[2]);
        }
        else
        {
            min = RectTransformUtility.WorldToScreenPoint(null, corners[0]);
            max = RectTransformUtility.WorldToScreenPoint(null, corners[2]);
        }

        float screenW = Screen.width;
        float screenH = Screen.height;

        Vector2 centerUV = new Vector2((min.x + max.x) / (2f * screenW), (min.y + max.y) / (2f * screenH));
        Vector2 sizeUV = new Vector2((max.x - min.x) / screenW, (max.y - min.y) / screenH) * holeScale;

        overlayMaterial.SetVector(HoleCenter, new Vector4(centerUV.x, centerUV.y, 0, 0));
        overlayMaterial.SetVector(HoleSize, new Vector4(sizeUV.x, sizeUV.y, 0, 0));
    }

    private void UpdateBlink()
    {
        timeAccumulator += Application.isPlaying ? Time.deltaTime * blinkSpeed : Time.unscaledDeltaTime * blinkSpeed;
        float t = Mathf.Repeat(timeAccumulator + phaseOffset, 1f);

        float pulse = blinkCurve.Evaluate(Mathf.Sin(t * Mathf.PI * 2) * 0.5f + 0.5f);
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, pulse);

        overlayMaterial.SetFloat(HighlightIntensity, intensity);
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying)
            Update();
    }
#endif

    void OnDestroy()
    {
        if (Application.isPlaying && overlayMaterial != null)
            Destroy(overlayMaterial);
    }
}
