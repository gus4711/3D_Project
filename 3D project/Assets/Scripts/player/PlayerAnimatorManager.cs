using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public delegate void HitHandler();
    public event HitHandler HitEvent;

    public delegate void AttackStartHandler();
    public event AttackStartHandler AttackStartEvent;

    public delegate void AttackEndHandler();
    public event AttackEndHandler AttackEndEvent;

    void Hit()
    {
        HitEvent();
    }

    void AttackStart()
    {
        AttackStartEvent();
    }

    void AttackEnd()
    {
        AttackEndEvent();
    }
}
