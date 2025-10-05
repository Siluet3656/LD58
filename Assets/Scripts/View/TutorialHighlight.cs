using UnityEngine;
using UnityEngine.UI;

public class TutorialHighlight : MonoBehaviour
{
    public Material holeMaterial;
    public RectTransform targetUI;
    [Range(0f, 1f)] public float radius = 0.15f;
    [Range(0f, 0.2f)] public float smoothness = 0.05f;

    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.material = new Material(holeMaterial);
    }

    void Update()
    {
        if (targetUI == null) return;
        
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetUI.position);
        Vector2 uv = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);

        image.material.SetVector("_Center", uv);
        image.material.SetFloat("_Radius", radius);
        image.material.SetFloat("_Smoothness", smoothness);
    }
}