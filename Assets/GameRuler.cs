using UnityEngine;

public class GameRuler : MonoBehaviour
{
    [SerializeField] private FullscreenTMPText _text;
    [SerializeField] private GameObject _map0;

    private void OnEnable()
    {
        if (GameState.State == 0)
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
    }

    private void Start()
    {
        if (GameState.State == 0)
        {
            _text.ShowText("ACT 0");
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
}
