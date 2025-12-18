using TMPro;
using UnityEngine;

//[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _header;
    [SerializeField] private TextMeshProUGUI _content;
    [SerializeField] private RectTransform _rectTransform;

    private Vector2 _mousePosition;
    
    private void Awake()
    {
        //gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateTooltip();
    }

    private void UpdateTooltip()
    {
        var normalizedPosition = new Vector2(_mousePosition.x / Screen.width, _mousePosition.y / Screen.height);
        var pivot = CalculatePivot(normalizedPosition);
        _rectTransform.pivot = pivot;
        transform.position = _mousePosition;
    }
    
    private Vector2 CalculatePivot(Vector2 normalizedPosition)
    {
        var pivotTopLeft = new Vector2(0f, 1f);
        var pivotTopRight = new Vector2(1f, 1f);
        var pivotBottomLeft = new Vector2(0f, 0f);
        var pivotBottomRight = new Vector2(1f, 0f);
        
        float centerThreshold = 0.25f;
    
        float distFromCenterX = Mathf.Abs(normalizedPosition.x - 0.5f);
        float distFromCenterY = Mathf.Abs(normalizedPosition.y - 0.5f);
        
        if (distFromCenterX < centerThreshold && distFromCenterY < centerThreshold)
        {
            return pivotBottomLeft;
        }
        
        if (normalizedPosition.x < 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopLeft;
        }
        else if (normalizedPosition.x > 0.5f && normalizedPosition.y >= 0.5f)
        {
            return pivotTopRight;
        }
        else if (normalizedPosition.x <= 0.5f && normalizedPosition.y < 0.5f)
        {
            return pivotBottomLeft;
        }
        else
        {
            return pivotBottomRight;
        }

    }

    public void SetText(string header, string content)
    {
        _header.text = header;
        _content.text = content;
        
        UpdateTooltip();
    }

    public void SetMousePosition(Vector2 mousePosition)
    {
        _mousePosition = mousePosition;
    }
}
