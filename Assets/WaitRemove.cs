using UnityEngine;
using Prepare;

public class WaitRemove : MonoBehaviour
{
   [SerializeField] private GameObject _textMeshPro;
   [SerializeField] private GameObject _button;
   [SerializeField] private SoulChecker _soulChecker;
   
   private void Update()
   {
      if (_soulChecker.TotalSouls == 0)
      {
         _textMeshPro.SetActive(false);
         _button.SetActive(true);
      }
   }
}
