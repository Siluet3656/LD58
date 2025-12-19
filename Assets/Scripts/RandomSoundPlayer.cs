using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] soundClips; 
    public AudioSource audioSource;
    
    private float nextPlayTime;

    [Tooltip("Минимальная задержка между звуками")]
    public float minDelay = 0.7f;
    
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;
    public float minValue = 0.7f;
    public float maxValue = 1f;

    public bool Play;
    
    void Start()
    {
        Play = false;
        
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
       
        nextPlayTime = Time.time + minDelay;
    }

    void SetPlay()
    {
        Play = true;
    }

    public void StopPlay()
    {
        //StartCoroutine(FadeOutAudio(audioSource, 0.5f));
        audioSource.Stop();
    }

    public void PlayRandomSound()
    {
        if (soundClips.Length == 0 || audioSource == null) return;
        
        int randomIndex = Random.Range(0, soundClips.Length);
        AudioClip clip = soundClips[randomIndex];
        
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;
        
        float randomVolume = Random.Range(minValue, maxValue);
        
        audioSource.PlayOneShot(clip, randomVolume);
        
        Play = false;
    }
    
    public void PlayRandomSoundFromTime(float startTime)
    {
        if (soundClips.Length == 0 || audioSource == null) return;
    
        int randomIndex = Random.Range(0, soundClips.Length);
        AudioClip clip = soundClips[randomIndex];
        
        audioSource.time = Mathf.Clamp(startTime, 0f, clip.length - 0.1f);
        
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        float randomVolume = Random.Range(minValue, maxValue);
    
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.PlayOneShot(clip, randomVolume);
        
        Play = false;
    }
    
    private IEnumerator FadeOutAudio(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
    
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
    
        audioSource.Stop();
        audioSource.volume = startVolume;
    }
    
}