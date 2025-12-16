using System;
using UnityEngine;

public class FloatingSoul : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float _amplitude = 0.5f;
    [SerializeField] private float _frequency = 2f;
    [SerializeField] private float _initialPhase = 0f; 
    
    [Header("Направление движения")]
    [SerializeField] private bool _moveOnXAxis = true;
    [SerializeField] private bool _moveOnYAxis = false;
    [SerializeField] private bool _moveOnZAxis = false;
    
    private Vector3 _startPosition;
    private float _timer = 0f;
    
    private bool _isMoving = false;
    private bool _isAtPosition = false;
    private Vector3 _positionToMove;
    private float _toPositionSpeed = 3f;
    
    private ExistAndDestroy _pop;

    private void Awake()
    {
        _pop = Resources.Load<ExistAndDestroy>("Prefabs/FloatingSoulPOP");
    }

    void Update()
    {
        if (_isMoving)
        {
            if (CheckRangeToPosition())
            {
                _isMoving = false;
                _isAtPosition = true;
            }
            
            transform.position = Vector3.Lerp(transform.position, _positionToMove, _toPositionSpeed * Time.deltaTime);
        }
        
        if (_isAtPosition)
        {
            _timer += Time.deltaTime * _frequency;
        
            float sineValue = Mathf.Sin(_timer);
        
            Vector3 offset = Vector3.zero;
        
            if (_moveOnXAxis) offset.x = sineValue * _amplitude;
            if (_moveOnYAxis) offset.y = sineValue * _amplitude;
            if (_moveOnZAxis) offset.z = sineValue * _amplitude;
        
            transform.position = _startPosition + offset;
        }
    }

    private void OnDisable()
    {
        foreach (var floatingSoul in G.SoulsManager.FloatingSouls)
        {
            if (floatingSoul.isActiveAndEnabled)
                Instantiate(_pop, floatingSoul.transform.position, Quaternion.identity, null);
        }
    }

    private bool CheckRangeToPosition()
    {
        return Vector3.Distance(transform.position, _positionToMove) < 0.1f;
    }

    public void StartMoveToPosition(Vector3 position)
    {
        _positionToMove = position;
        _startPosition = position;
        _timer = _initialPhase;
        _isMoving = true;
    }
}