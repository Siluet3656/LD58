using System.Collections.Generic;
using UnityEngine;

public class SpawnCrystals : MonoBehaviour
{
    [SerializeField] List<GameObject> _crystals = new List<GameObject>();
    [SerializeField] List<GameObject> _shadows = new List<GameObject>();

    private bool _grow = false;

    private void Awake()
    {
        G.SpawnCrystals = this;
    }

    public void SpawnCrystal()
    {
        _grow = true;
    }

    private void Update()
    {
        if (_grow)
        {
            foreach (var crystal in _crystals)
            {
                crystal.transform.localScale = new Vector3(Mathf.Lerp(crystal.transform.localScale.x,1f,2f * Time.deltaTime), Mathf.Lerp(crystal.transform.localScale.y,1f,2f * Time.deltaTime),0);
            }

            foreach (var shadow in _shadows)
            {
                shadow.transform.localPosition = new Vector3(0,-2,1);
            }
        }
    }
}
