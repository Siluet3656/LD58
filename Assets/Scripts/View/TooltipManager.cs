using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class TooltipManager : MonoBehaviour
    {
        public static TooltipManager Instance;

        [Header("UI References")]
        [SerializeField] private GameObject _tooltipObject;
        [SerializeField] private Text _tooltipTitle;
        [SerializeField] private Text _tooltipText;
        [SerializeField] private Vector2 _offset = new Vector2(20f, -20f);
        [SerializeField] private float _closeDelay = 0.15f;

        private RectTransform _canvasRect;
        private RectTransform _tooltipRect;
        private Vector2 _mousePosition;

        private float _closeTimer = 0f;
        private bool _isShowing = false;
        private bool _hideRequested = false; 

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            _tooltipRect = _tooltipObject.GetComponent<RectTransform>();
            _tooltipObject.SetActive(false);
        }

        private void Update()
        {
            Vector2 anchoredPos;
            Camera cam = null;
            Canvas canvas = _canvasRect.GetComponent<Canvas>();
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                cam = canvas.worldCamera;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRect,
                _mousePosition + _offset,
                cam,
                out anchoredPos
            );

            _tooltipRect.anchoredPosition = anchoredPos;
            ClampTooltipToCanvas();
            
            if (_isShowing && _hideRequested)
            {
                _closeTimer += Time.deltaTime;
                if (_closeTimer >= _closeDelay)
                {
                    _tooltipObject.SetActive(false);
                    _isShowing = false;
                    _hideRequested = false;
                    _closeTimer = 0f;
                }
            }
        }

        private void ClampTooltipToCanvas()
        {
            Vector2 pivot = _tooltipRect.pivot;
            Vector2 size = _tooltipRect.sizeDelta;
            Vector2 pos = _tooltipRect.anchoredPosition;

            Vector2 minBounds = -_canvasRect.sizeDelta / 2f;
            Vector2 maxBounds = _canvasRect.sizeDelta / 2f;

            float halfW = size.x * (1 - pivot.x);
            float halfH = size.y * (1 - pivot.y);

            pos.x = Mathf.Clamp(pos.x, minBounds.x + halfW, maxBounds.x - halfW);
            pos.y = Mathf.Clamp(pos.y, minBounds.y + halfH, maxBounds.y - halfH);

            _tooltipRect.anchoredPosition = pos;
        }

        public void SetMousePosition(Vector2 mousePosition)
        {
            _mousePosition = mousePosition;
        }

        public void ShowTooltip(string title, string description)
        {
            _tooltipTitle.text = title;
            _tooltipText.text = description;
            _tooltipObject.SetActive(true);
            _isShowing = true;
            _hideRequested = false;
            _closeTimer = 0f;
        }

        public void HideTooltip()
        {
            if (_isShowing)
            {
                _hideRequested = true; 
                _closeTimer = 0f;
            }
        }
    }
}
