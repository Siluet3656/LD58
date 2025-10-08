using UnityEngine;
using UnityEngine.UI;
using Data;

public class costcolor : MonoBehaviour
{
    [SerializeField] private SkillType _skillType;

    Text _text;
    
    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.color = G.SkillResources.HasEnoughResources(AbilityDataCms.Instance.GetSpellConfig(_skillType).cost) ? Color.white : Color.red;
    }
}
