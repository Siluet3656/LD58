using UnityEngine;

public class VictoriaEscape : MonoBehaviour
{
    [SerializeField] private Transform _escapePoint;
    [SerializeField] private SpriteRenderer _victoria;
    [SerializeField] private float _escapeDistance;

    private bool _isEscaping = false;

    private void Awake()
    {
        G.VictoriaEscape = this;
    }

    private void Update()
    {
        if (_isEscaping)
            transform.position = Vector3.MoveTowards(transform.position, _escapePoint.position, Time.deltaTime * _escapeDistance);
        
        if (Vector3.Distance(transform.position, _escapePoint.position) <= 0.1f)
            gameObject.SetActive(false);
    }

    public void Escape()
    {
        _isEscaping = true;
    }

    public void Flip()
    {
        _victoria.flipX = !_victoria.flipX;
    }

    public void Skip()
    {
        gameObject.SetActive(false);
    }
}
