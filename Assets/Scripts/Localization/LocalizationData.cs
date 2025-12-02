using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationDB", menuName = "Localization/Database")]
public class LocalizationDB : ScriptableObject
{
    public List<Entry> entries;
}

[Serializable]
public class Entry
{
    public string key;
    [TextArea(2,10)]
    public string en;
    [TextArea(2,10)]
    public string ru;
}