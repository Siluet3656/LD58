using System;
using UnityEngine;

public class AnimationEventCatcher : MonoBehaviour
{
    public Action OnAttackEnd;
    public Action OnDeathAnimEnd;
    public Action OnShieldAnimEnd;
    public void CatchAttackEnd()
    {
        OnAttackEnd?.Invoke();
    }

    public void CatchDeathAnimEnd()
    {
        OnDeathAnimEnd?.Invoke();
    }

    public void CatchShieldAnimEnd()
    {
        OnShieldAnimEnd?.Invoke();
    }
}
