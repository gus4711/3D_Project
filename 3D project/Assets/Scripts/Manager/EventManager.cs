using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void OnPlayerAttackDelegate(Player player, AIBase aiBase);
    public event OnPlayerAttackDelegate OnPlayerAttackEvent;

    public delegate void OnDestoryObjectDelegate(Player player, ObjectController objectController);
    public event OnDestoryObjectDelegate OnDestoryObjectEvent;

    public delegate void OnAttackHitSoundDelegate(Player player, ObjectController objectController);
    public event OnAttackHitSoundDelegate OnAttackHitSoundEvent;

    public void OnPlayerAttack(Player player, AIBase aiBase)
    {
        if(OnPlayerAttackEvent != null)
            OnPlayerAttackEvent(player, aiBase);
    }

    public void OnDestroyObject(Player player, ObjectController objectController)
    {
        if (OnDestoryObjectEvent != null)
            OnDestoryObjectEvent(player, objectController);
    }


}
