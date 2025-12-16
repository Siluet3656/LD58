using System.Collections;
using UnityEngine;

public class ExistAndDestroy : MonoBehaviour
{
    [SerializeField] private float _existTIme = 1f;

    private void Awake()
    {
        StartCoroutine(Exist());
    }

    private IEnumerator Exist()
    {
        yield return new WaitForSeconds(_existTIme);
        Destroy(gameObject);
    }
}
