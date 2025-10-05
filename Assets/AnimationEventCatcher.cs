using System;
using UnityEngine;

public class AnimationEventCatcher : MonoBehaviour
{
    public Action OnAttackEnd;
    public void CatchAttackEnd()
    {
        OnAttackEnd?.Invoke();
    }
}
