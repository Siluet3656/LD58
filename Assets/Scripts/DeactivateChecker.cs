using UnityEngine;

public class DeactivateChecker : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectToFollow;
    void Update()
    {
        if (_gameObjectToFollow.activeSelf != gameObject.activeSelf)
        {
            gameObject.SetActive(gameObject.activeSelf);
        }
    }
}
