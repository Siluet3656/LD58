using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
public class OverlayHoleAuto : MonoBehaviour
{
    [Tooltip("UI-элемент, под который будет подстраиваться прозрачное окно.")]
    public RectTransform target;

    [Tooltip("Дополнительный масштаб окна (1 = ровно по элементу).")]
    [Range(0.8f, 2f)]
    public float holeScale = 1.0f;

    private Image overlayImage;
    private Material overlayMaterial;
    private Canvas canvas;
    private Camera uiCamera;

    void OnEnable()
    {
        overlayImage = GetComponent<Image>();
        canvas = overlayImage.canvas;

        if (overlayImage.material == null)
        {
            Debug.LogWarning("OverlayHoleAuto: У Overlay Image нет материала! Назначь материал с шейдером UI/TransparentHole.");
            return;
        }

        // создаём инстанс материала (чтобы не портить оригинал)
        overlayMaterial = Application.isPlaying ? Instantiate(overlayImage.material) : overlayImage.material;
        overlayImage.material = overlayMaterial;

        // если Canvas в режиме Screen Space - Camera, берём ссылку на камеру
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            uiCamera = canvas.worldCamera;
    }

    void Update()
    {
        if (overlayMaterial == null || target == null || canvas == null)
            return;

        UpdateHole();
    }

    private void UpdateHole()
    {
        Vector3[] corners = new Vector3[4];
        target.GetWorldCorners(corners);

        Vector2 min, max;

        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && uiCamera != null)
        {
            // конвертируем мировые точки в экранные с учётом камеры
            min = RectTransformUtility.WorldToScreenPoint(uiCamera, corners[0]);
            max = RectTransformUtility.WorldToScreenPoint(uiCamera, corners[2]);
        }
        else
        {
            // для Overlay или Camera = null
            min = RectTransformUtility.WorldToScreenPoint(null, corners[0]);
            max = RectTransformUtility.WorldToScreenPoint(null, corners[2]);
        }

        float screenW = Screen.width;
        float screenH = Screen.height;

        // преобразуем координаты в UV (0..1)
        Vector2 centerUV = new Vector2((min.x + max.x) / (2f * screenW), (min.y + max.y) / (2f * screenH));
        Vector2 sizeUV = new Vector2((max.x - min.x) / screenW, (max.y - min.y) / screenH) * holeScale;

        // передаём в материал
        overlayMaterial.SetVector("_HoleCenter", new Vector4(centerUV.x, centerUV.y, 0, 0));
        overlayMaterial.SetVector("_HoleSize", new Vector4(sizeUV.x, sizeUV.y, 0, 0));
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!Application.isPlaying)
            Update();
    }
#endif
}
