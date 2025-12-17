using UnityEngine;

public class SoundBank : MonoBehaviour
{
    public RandomSoundPlayer DropSound;
    public RandomSoundPlayer PickupSound;
    public RandomSoundPlayer PLaceSound;
    public RandomSoundPlayer WrongSound;
    public RandomSoundPlayer DeleteSound;

    private void Awake()
    {
        G.SoundBank = this;
    }
}
