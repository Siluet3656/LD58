using System;
using UnityEngine;
using System.Collections;

public class Steps : MonoBehaviour
{
    [SerializeField] private RandomSoundPlayer _soundPlayer;
    [SerializeField] private float _soundDuration = 2.0f;

    private void Awake()
    {
        G.Steps = this;
    }

    public void GoSteps()
    {
        StartCoroutine(PlaySoundForDuration(_soundDuration));
    }
    
    private IEnumerator PlaySoundForDuration(float duration)
    {
        _soundPlayer.PlayRandomSound();
        yield return new WaitForSeconds(duration);
        _soundPlayer.StopPlay();
    }
}