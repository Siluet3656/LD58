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
    [SerializeField] private GameObject _point4;
    [SerializeField] private GameObject _point5;
    [Header("MAPS")] 
    [SerializeField] private GameObject _act0;
    [SerializeField] private GameObject _act1;
    [SerializeField] private GameObject _act2;
    [SerializeField] private GameObject _act3;

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
            _text.ShowText(LocalizationManager.Instance.Get("ACT0"));
        }
        else if (GameState.State > 4 && GameState.State < 8)
        {
            _camera.transform.position = new Vector3(_point2.transform.position.x,_point2.transform.position.y, -10);
            _act0.SetActive(false);
            _act1.SetActive(true);
        }
        else if (GameState.State > 7 && GameState.State <= 11)
        {
            _camera.transform.position = new Vector3(_point3.transform.position.x,_point3.transform.position.y, -10);
            _act0.SetActive(false);
            _act1.SetActive(true);
        }
        else if (GameState.State > 11 && GameState.State <= 17)
        {
            _camera.transform.position = _point4.transform.position;
            _act0.SetActive(false);
            _act1.SetActive(false);
            _act2.SetActive(true);
        }
        else if (GameState.State > 17 && GameState.State <= 21)
        {
            _camera.transform.position = _point5.transform.position;
            _act0.SetActive(false);
            _act1.SetActive(false);
            _act2.SetActive(false);
            _act3.SetActive(true);
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
        _text.ShowText(LocalizationManager.Instance.Get("ACT1"));
        _act0.SetActive(false);
    }

    public void GoActTwo()
    {
        _camera.transform.position = _point4.transform.position;
        _text.ShowText(LocalizationManager.Instance.Get("ACT2"));
        _act1.SetActive(false);
    }

    public void GoActThree()
    {
        _camera.transform.position = _point5.transform.position;
        _text.ShowText(LocalizationManager.Instance.Get("ACT3"));
        _act2.SetActive(false);
    }
}
