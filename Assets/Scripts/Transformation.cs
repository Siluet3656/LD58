using UnityEngine;

public class Transformation : MonoBehaviour
{
    [SerializeField] private GameObject _pureSoul;
    [SerializeField] private GameObject _original;
    [SerializeField] private RandomSoundPlayer _transitionSoundPlayer;
    [SerializeField] private Animator _transitionAnimator;
    [SerializeField] private Animator _pureSoulAnimator;
    
    public void GoTransformation()
    {
        _transitionSoundPlayer.PlayRandomSound();
        _transitionAnimator.SetTrigger("Go");
    }

    public void PUKPIUK()
    {
        _pureSoul.SetActive(true);
        _pureSoulAnimator.SetTrigger("DialogueEnd");
    }
}
