using System.Collections.Generic;
using Battle;
using EntityResources;
using Prepare;
using View;

public static class G
{
    public static readonly List<Enemy> Enemies = new List<Enemy>();
    public static Player Player;
    public static AbilityDrag AbilityDrag;
    public static SkillResources SkillResources;
    public static readonly List<SoulPlace> SoulPlaces = new List<SoulPlace>();
    public static SoulChecker SoulChecker;
    public static Hp PlayerHp;
    public static PlayerAttack PlayerAttack;
    public static PlayerView  PlayerView;
}