using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Map : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
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
    
    public void LoadSceneMode(int id)
    {
        StartCoroutine(LoadSceneWithFade(id));
    }
}
