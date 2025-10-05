using System;
using UnityEngine;

public class AnimationEventCatcher : MonoBehaviour
{
    public Action OnAttackEnd;
    public Action OnDeathAnimEnd;
    public void CatchAttackEnd()
    {
        OnAttackEnd?.Invoke();
    }

    public void CatchDeathAnimEnd()
    {
        OnDeathAnimEnd?.Invoke();
    }
}
