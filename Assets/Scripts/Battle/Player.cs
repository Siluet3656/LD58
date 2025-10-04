using UnityEngine;

namespace Battle
{
    public class Player : MonoBehaviour
    {
        private void Awake()
        {
            G.Player = this;
        }
    }
}
