using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatPanel : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
