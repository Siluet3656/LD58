using UnityEngine;
using System.Collections;

public class MusicSequencePlayer : MonoBehaviour
{
    public AudioClip[] musicTracks; 
    public AudioSource audioSource;
    public float fadeDuration = 3f;
    public float segmentDuration = 30f;
    public float volume = 0.1f;

    private int[] playSequence = new int[] { 0, 1, 0, 1, 0, 0 }; 
    private int currentSegment = 0;
    private bool isPlaying = false;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        audioSource.loop = false;
        StartPlayback();
    }

    public void StartPlayback()
    {
        if (!isPlaying && musicTracks.Length >= 2)
        {
            isPlaying = true;
            currentSegment = 0;
            StartCoroutine(PlaySequence());
        }
    }

    private IEnumerator PlaySequence()
    {
        while (currentSegment < playSequence.Length)
        {
            int trackIndex = playSequence[currentSegment];
            AudioClip currentTrack = musicTracks[trackIndex];
            
            yield return StartCoroutine(FadeInAndPlay(currentTrack));
            
            float playTime = segmentDuration;
            if (currentSegment < playSequence.Length - 1) 
            {
                playTime -= fadeDuration;
            }

            yield return new WaitForSeconds(playTime);

            if (currentSegment < playSequence.Length - 1)
            {
                yield return StartCoroutine(FadeOut());
            }

            currentSegment++;
        }
    }

    private IEnumerator FadeInAndPlay(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.time = 0f;
        audioSource.volume = 0f;
        audioSource.Play();

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volume, timer / fadeDuration);
            yield return null;
        }

        audioSource.volume = volume;
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }

    void OnDestroy()
    {
        StopAllCoroutines();
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}