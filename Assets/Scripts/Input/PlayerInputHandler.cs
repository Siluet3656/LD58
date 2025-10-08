using System;
using UnityEngine;
using Battle;
using Prepare;
using View;

namespace Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private Camera _mainCamera;
        private PlayerControlls _inputActions;
        private PlayerTargeting _targeting;
        private AbilityDrag _hand;
        private Vector2 _mousePosition;

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
            _inputActions.UI.RBM.started += ctx => OnRightMouseButtonClicked();
            
            _hand = FindObjectsOfType<AbilityDrag>()[0];
        }

        private void Update()
        {
            _mousePosition = _inputActions.UI.MousePosition.ReadValue<Vector2>();
            
            _hand.SetPoint(_mainCamera.ScreenToWorldPoint(_mousePosition));
            
            TooltipManager.Instance.SetMousePosition(_mousePosition);
        }

        private void OnEnable() => _inputActions.Enable();
        private void OnDisable() => _inputActions.Disable();
        
        private void OnLeftMouseButtonClicked()
        {
            Ray raySpellButton = _mainCamera.ScreenPointToRay(_inputActions.UI.MousePosition.ReadValue<Vector2>());
            RaycastHit2D hitSpellButton = Physics2D.Raycast(raySpellButton.origin, raySpellButton.direction, Mathf.Infinity, LayerMask.GetMask("AbilityButton"));
            
            Ray raySoulButton = _mainCamera.ScreenPointToRay(_inputActions.UI.MousePosition.ReadValue<Vector2>());
            RaycastHit2D hitSoulButton = Physics2D.Raycast(raySoulButton.origin, raySoulButton.direction, Mathf.Infinity, LayerMask.GetMask("SoulButton"));
            
            Ray rayTargetable = _mainCamera.ScreenPointToRay(_inputActions.UI.MousePosition.ReadValue<Vector2>());
            RaycastHit2D hitTargetable = Physics2D.Raycast(rayTargetable.origin, rayTargetable.direction, Mathf.Infinity, LayerMask.GetMask("Enemy"));
            
            if (_hand.CheckDraggingStatus())
            {
                _hand.TryToDropASpell(hitSpellButton);
            }
            
            if (_hand.CheckSoulDraggingStatus())
            {
                _hand.TryToDropASpell(hitSoulButton);
            }

            if (hitSpellButton.collider == null)
            {
                _targeting.OnMouseTargetSelect(hitTargetable);
            }

            foreach (var VARIABLE in G.ClickFloatingTexts)
            {
                VARIABLE.OnLBM();
            }

            BattleRuler.Instance.IsLBM = true;
        }

        private void OnRightMouseButtonClicked()
        {
            Ray raySoulButton = _mainCamera.ScreenPointToRay(_inputActions.UI.MousePosition.ReadValue<Vector2>());
            RaycastHit2D hitSoulButton = Physics2D.Raycast(raySoulButton.origin, raySoulButton.direction, Mathf.Infinity, LayerMask.GetMask("SoulButton"));

            if (hitSoulButton. collider == null) return;
            
            SoulPlace soulPlace = hitSoulButton.collider.GetComponent<SoulPlace>();

            if (soulPlace != null)
            {
                soulPlace.RemoveSpell();
            }
        }
        
        public Vector2 MousePosition => _mousePosition;
        public PlayerControlls InputActions => _inputActions;
    }
}