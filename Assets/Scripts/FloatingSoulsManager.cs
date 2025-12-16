using System.Collections.Generic;
using Battle;
using Data;
using UnityEngine;
using UnityEngine.UI;

public class FloatingSoulsManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _soulsPositions = new List<Transform>();
    [SerializeField] private List<FloatingSoul> _floatingSouls = new List<FloatingSoul>();
    private List<SoulPlace> _soulPlaces = new List<SoulPlace>();

    private void Awake()
    {
        G.SoulsManager = this;
        _soulPlaces.AddRange(FindObjectsOfType<SoulPlace>());
    }

    public void StartFloat()
    {
        for (int i = 0; i < _soulPlaces.Count; i++)
        {
            if (_soulPlaces[i].SoulType != SoulType.None)
            {
                _floatingSouls[i].transform.position = _soulPlaces[i].transform.position;
                _floatingSouls[i].gameObject.SetActive(true);
                _floatingSouls[i].StartMoveToPosition(_soulsPositions[i].position);
                _floatingSouls[i].GetComponent<Image>().sprite =
                    SoulDataCms.Instance.GetSpellConfig(_soulPlaces[i].SoulType).floating;
            }
        }
    }
}