using UnityEngine;

public class FightButton : MonoBehaviour
{
    [SerializeField] private RandomSoundPlayer _fightStartSoundPlayer;
    [SerializeField] private RandomSoundPlayer _highlightSoundPlayer;

    public void PlayFightStartSound()
    {
        _fightStartSoundPlayer.PlayRandomSound();
    }

    public void PlayHighlightSound()
    {
        _highlightSoundPlayer.PlayRandomSound();
    }
}
