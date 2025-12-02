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
                //_animator.SetTrigger("0-1");
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
            case 11:
                _animator.SetTrigger("2-1");
                break;
            case 12:
                _animator.SetTrigger("2-2");
                break;
            case 13:
                _animator.SetTrigger("2-3");
                break;
            case 14:
                _animator.SetTrigger("2-4");
                break;
            case 15:
                _animator.SetTrigger("2-5");
                break;
            case 16:
                _animator.SetTrigger("2-6");
                break;
            case 17:
                _animator.SetTrigger("Shiza");
                break;
            case 18:
                _animator.SetTrigger("3-1");
                break;
            case 19:
                _animator.SetTrigger("3-2");
                break;
            case 20:
                _animator.SetTrigger("3-3");
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

        switch (id)
        {
            case 1:
                G.GameRuler.GoActOne();
                break;
            case 2:
                G.GameRuler.GoActTwo();
                break;
            case 3:
                G.GameRuler.GoActThree();
                break;
        }
        
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
        StartCoroutine(GoActWithFade(1));
    }

    public void IncrementGameState2()
    {
        StartCoroutine(GoActWithFade(2));
    }

    public void IncrementGameState3()
    {
        StartCoroutine(GoActWithFade(3));
    }
}
