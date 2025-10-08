using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Battle
{
    public class AbilityButton : MonoBehaviour
    {
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] private SkillType _skillType;
        
        private SkillArrowToTarget _arrow;
        private Camera _mainCamera;
        private PlayerInputHandler _inputHandler;
        private bool _castStarted = false;
        
        private Image _spriteRenderer;

        private void Start()
        {
            _mainCamera = Camera.main;

            _spriteRenderer = GetComponent<Image>();
            
            _inputHandler = G.Player.GetComponent<PlayerInputHandler>();

            _inputHandler.InputActions.UI.LBM.started += ctx => CheckEnemyOnMouse();
            _inputHandler.InputActions.UI.Skill1.started += ctx => StartAbilityCast();
        }

        private void Update()
        {
            if (BattleRuler.Instance.IsFighting == false) return;
            
            if (_skillType == SkillType.None) return;
            
            if (_arrow == null) return;
            
            Vector3 mouseWorld = _mainCamera.ScreenToWorldPoint(_inputHandler.MousePosition);
            mouseWorld.z = 0;
            
            _arrow.SetTargetPosition(mouseWorld);
        }

        private void CheckEnemyOnMouse()
        {
            RaycastHit2D hitAbility = Physics2D.Raycast(_mainCamera.ScreenToWorldPoint(_inputHandler.MousePosition), Vector2.zero);
            
            if (hitAbility.collider != null && hitAbility.collider.TryGetComponent(out Enemy target) && _castStarted)
            {
                EndAbilityCast(target);
            }
            else
            {
                CancelAbilityCast();
            }
        }
        
        public void StartAbilityCast()
        {
            if (_castStarted) return;
            
            if (BattleRuler.Instance.IsFighting == false) return;
            
            if (_skillType == SkillType.None) return;
            
            GameObject arrowObj = Instantiate(_arrowPrefab, transform.position, Quaternion.identity, transform.parent);
            _arrow = arrowObj.GetComponent<SkillArrowToTarget>();

            _arrow.SetSource(transform);

            _castStarted = true;
        }

        public void CancelAbilityCast()
        {
            if (_arrow == null) return;
            
            _castStarted = false;
            
            Destroy(_arrow.gameObject);
        }

        public void EndAbilityCast(Enemy target)
        {
            if (_arrow == null) return;
            
            target.ApplyAbility(_skillType);
            
            _castStarted = false;

            Destroy(_arrow.gameObject);
        }
    }
}