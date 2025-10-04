using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Battle/Ability")]
    public class AbilityData : ScriptableObject
    {
        public string abilityName;
        public Sprite icon;
        [TextArea]
        public string description;
        public SkillType skillType;
    }
}