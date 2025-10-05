using UnityEngine;
using System.Collections;

public class MusicSequencePlayer : MonoBehaviour
{
    public AudioClip[] musicTracks; // 0: Track1, 1: Track2, 2: Track3, 3: Track4
    public AudioSource audioSource;
    public float fadeDuration = 3f;
    public float segmentDuration = 30f;
    public float volume = 0.1f;

    private int[] playSequence = new int[] { 3, 1, 3, 1, 0, 2 }; // 1,2,1,3,1,4 (индексы 0-3)
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
        if (!isPlaying && musicTracks.Length >= 4)
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

            // Запускаем трек с плавным затуханием
            yield return StartCoroutine(FadeInAndPlay(currentTrack));

            // Ждем основное время сегмента (30 секунд минус время затухания)
            float playTime = segmentDuration;
            if (currentSegment < playSequence.Length - 1) // Для всех кроме последнего
            {
                playTime -= fadeDuration;
            }

            yield return new WaitForSeconds(playTime);

            // Для всех кроме последнего сегмента - плавное затухание
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