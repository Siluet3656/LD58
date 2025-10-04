using UnityEngine;
using Battle;

namespace Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private Camera _mainCamera;
        private PlayerControlls _inputActions;
        private PlayerTargeting _targeting;

        private void Awake()
        {
            _mainCamera = Camera.main;
            
            if (_mainCamera == null)
            {
                Debug.LogError("NO CAMERA!!!");
            }
            
            _targeting = GetComponent<PlayerTargeting>();
            
            _inputActions = new PlayerControlls();
            
            _inputActions.UI.LBM.started += ctx => OnLeftMouseButtonClicked();
        }
        
        private void OnEnable() => _inputActions.Enable();
        private void OnDisable() => _inputActions.Disable();
        
        private void OnLeftMouseButtonClicked()
        {
            Ray rayTargetable = _mainCamera.ScreenPointToRay(_inputActions.UI.MousePosition.ReadValue<Vector2>());
            RaycastHit2D hitTargetable = Physics2D.Raycast(rayTargetable.origin, rayTargetable.direction, Mathf.Infinity, LayerMask.GetMask("Enemy"));

            _targeting.OnMouseTargetSelect(hitTargetable);
        }
    }
}