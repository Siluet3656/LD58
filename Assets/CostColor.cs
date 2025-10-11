using UnityEngine;
using UnityEngine.UI;
using Data;

public class CostColor : MonoBehaviour
{
    [SerializeField] private SkillType _skillType;
    [SerializeField] private Color _colorAvailable = Color.green;
    [SerializeField] private Color _colorUnavailable = Color.red;

    Text _text;
    
    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.color = G.SkillResources.HasEnoughResources(AbilityDataCms.Instance.GetSpellConfig(_skillType).cost) ? _colorAvailable : _colorUnavailable;
    }
}
