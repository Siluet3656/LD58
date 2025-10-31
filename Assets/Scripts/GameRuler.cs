using System;
using UnityEngine;

public class GameRuler : MonoBehaviour
{
    [Header("TEST")]
    [SerializeField] private bool _isTest = false;
    [SerializeField] private int _startLevel = 0;
    [Header("MAIN")]
    [SerializeField] private FullscreenTMPText _text;
    [SerializeField] private GameObject _map0;
    [SerializeField] private GameObject _point2;
    [SerializeField] private GameObject _point3;
    [Header("MAPS")] 
    [SerializeField] private GameObject _act0;
    [SerializeField] private GameObject _act1;
    [SerializeField] private GameObject _act2;

    private Camera _camera;

    private void Awake()
    {
        G.GameRuler = this;
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        if (GameState.State == 0)
        {
            _text.OnActivate += ActivateMap;
        }
        
        if (GameState.State == 5)
        {
            _text.OnActivate += ActivateMap;
        }
        
        if (GameState.State == 10)
        {
            _text.OnActivate += ActivateMap;
        }
    }

    private void OnDisable()
    {
        if (GameState.State == 0)
        {
            _text.OnActivate -= ActivateMap;
        }
        
        if (GameState.State == 5)
        {
            _text.OnActivate -= ActivateMap;
        }
        
        if (GameState.State == 10)
        {
            _text.OnActivate -= ActivateMap;
        }
    }

    private void Start()
    {
        if (_isTest)
            GameState.State = _startLevel;
        
        if (GameState.State == 0)
        {
            _text.ShowText("ACT 0");
        }
        else if (GameState.State > 4 && GameState.State < 8)
        {
            _camera.transform.position = new Vector3(_point2.transform.position.x,_point2.transform.position.y, -10);
            _act0.SetActive(false);
            _act1.SetActive(true);
        }
        else if (GameState.State > 7 && GameState.State < 10)
        {
            _camera.transform.position = new Vector3(_point3.transform.position.x,_point3.transform.position.y, -10);
            _act0.SetActive(false);
            _act1.SetActive(true);
        }
        else
        {
            _map0.SetActive(true);
        }
    }

    private void ActivateMap()
    {
        _map0.SetActive(true);
    }

    public void GoActOne()
    {
        _camera.transform.position = _point2.transform.position;
        _text.ShowText("ACT 1");
        _act0.SetActive(false);
    }
}
