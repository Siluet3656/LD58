using UnityEngine;
using Battle;
using Prepare;
using View;

namespace Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] AbilityButton[] _abilityButtons;
        
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
            
            _inputActions.UI.Skill1.started += ctx => ChooseAbility(SkillType.Strike);
            _inputActions.UI.Skill2.started += ctx => ChooseAbility(SkillType.Punch);
            _inputActions.UI.Skill3.started += ctx => ChooseAbility(SkillType.Shield);
            _inputActions.UI.Skill4.started += ctx => ChooseAbility(SkillType.Beam);
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
            
            RaycastHit2D hitAbility = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(MousePosition), Vector2.zero);

            if (BattleRuler.Instance.IsCastStarted)
            {
                foreach (AbilityButton button in _abilityButtons)
                {
                    if (hitAbility.collider != null && hitAbility.collider.TryGetComponent(out Enemy target) && button.IsArrowExists)
                    {
                        button.EndAbilityCast(target);
                        return;
                    }
                    else
                    {
                        button.CancelAbilityCast();
                    }
                }
            }
            
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
        
        private void ChooseAbility(SkillType skillType)
        {
            if (BattleRuler.Instance.IsFighting == false) return;
            
            switch (skillType)
            {
                case SkillType.None:
                    break;
                case SkillType.Strike:
                    _abilityButtons[0].StartAbilityCast();
                    break;
                case SkillType.Punch:
                    _abilityButtons[1].StartAbilityCast();
                    break;
                case SkillType.Shield:
                    G.PlayerHp.ApplyShield();
                    break;
                case SkillType.Beam:
                    G.PlayerAttack.SoulBeam();
                    break;
            }
        }
        
        public Vector2 MousePosition => _mousePosition;
    }
}