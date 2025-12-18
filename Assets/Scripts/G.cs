using System.Collections.Generic;
using Battle;
using Effects;
using EntityResources;
using Prepare;
using View;

public static class G
{
    public static readonly List<Enemy> Enemies = new List<Enemy>();
    public static Player Player;
    public static AbilityDrag AbilityDrag;
    public static SkillResources SkillResources;
    public static readonly Dictionary<int, SoulType> SoulPlaces = new Dictionary<int, SoulType>();
    public static SoulChecker SoulChecker;
    public static Hp PlayerHp;
    public static PlayerAttack PlayerAttack;
    public static PlayerView  PlayerView;
    public static SmoothSlideY SmoothSlideY;
    public static readonly List<FloatingTextClick>  ClickFloatingTexts = new List<FloatingTextClick>();
    public static ScreenFader ScreenFader;
    public static GameRuler GameRuler;
    public static Map Map;
    public static FloatingSoulsManager SoulsManager;
    public static SoundBank SoundBank;
    public static VictoriaEscape VictoriaEscape;
    public static Steps Steps;
    public static SpawnCrystals SpawnCrystals;
}