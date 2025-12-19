using UnityEngine;

public class SoundBank : MonoBehaviour
{
    public RandomSoundPlayer DropSound;
    public RandomSoundPlayer PickupSound;
    public RandomSoundPlayer PLaceSound;
    public RandomSoundPlayer WrongSound;
    public RandomSoundPlayer DeleteSound;
    public RandomSoundPlayer ReflectSound;
    public RandomSoundPlayer TargetSwitchSound;

    private void Awake()
    {
        G.SoundBank = this;
    }
}
