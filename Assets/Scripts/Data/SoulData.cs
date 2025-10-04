﻿using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Soul", menuName = "Battle/Soul")]
    public class SoulData : ScriptableObject
    {
        public string soulName;
        public Sprite icon;
        [TextArea]
        public string description;
        public SoulType soulType;
        public int cost;
    }
}