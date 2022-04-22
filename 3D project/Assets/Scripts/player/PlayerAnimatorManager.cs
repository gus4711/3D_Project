using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public delegate void HitDelegate();
    public event HitDelegate HitEvent;

    public delegate void AttackStartDelegate();
    public event AttackStartDelegate AttackStartEvent;

    public delegate void AttackEndDelegate();
    public event AttackEndDelegate AttackEndEvent;



    void OnHit()
    {
        if (HitEvent != null)
        {
            HitEvent();
        }
    }

    void OnAttackStart()
    {
        if (AttackStartEvent != null)
        {
            AttackStartEvent();
        }   
    }

    void OnAttackEnd()
    {
        if (AttackEndEvent != null)
        {
            AttackEndEvent();
        }  
    }
}
