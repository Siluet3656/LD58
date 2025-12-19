using System.Collections.Generic;

[System.Serializable]
public class PlayerBuild
{
    public List<string> souls = new List<string>();

    public string Serialize()
    {
        return string.Join(",", souls);
    }
}