using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Map : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    private void Awake()
    {
        G.Map = this;
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        switch (GameState.State)
        {
            case 0:
                _animator.SetTrigger("0-1");
                break;
            case 1:
                _animator.SetTrigger("0-2");
                break;
            case 2:
                _animator.SetTrigger("0-3");
                break;
            case 3:
                _animator.SetTrigger("0-4");
                break;
            case 4:
                _animator.SetTrigger("City");
                break;
            case 5:
                _animator.SetTrigger("1-1");
                break;
            case 6:
                _animator.SetTrigger("1-2");
                break;
            case 7:
                _animator.SetTrigger("1-3");
                break;
            case 8:
                _animator.SetTrigger("1-4");
                break;
            case 9:
                _animator.SetTrigger("1-5");
                break;
            case 10:
                _animator.SetTrigger("Church");
                break;
        }
    }
    
    private void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    private IEnumerator LoadSceneWithFade(int id)
    {
        yield return StartCoroutine(G.ScreenFader.FadeOut());
        LoadScene(id);
    }
    
    private IEnumerator GoActWithFade(int id)
    {
        yield return StartCoroutine(G.ScreenFader.FadeOut());
        GameState.State++;
        G.GameRuler.GoActOne();
        G.ScreenFader.Clear();
    }

    public Animator Animator => _animator;
    
    public void LoadSceneMode(int id)
    {
        StartCoroutine(LoadSceneWithFade(id));
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }

    public void IncrementGameState()
    {
        StartCoroutine(GoActWithFade(0));
    }
}
