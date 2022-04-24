using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public delegate void OnPlayerAttackDelegate(Player player, AIBase aiBase);
    public event OnPlayerAttackDelegate OnPlayerAttackEvent;

    public delegate void OnHitDelegate(Player player, ObjectController objectController);
    public event OnHitDelegate OnDestoryObjectEvent;

    public event OnHitDelegate OnAttackHitSoundEvent;

    public delegate void OnGetItemDelegate(Player player, ObjectInfo objectInfo);
    public event OnGetItemDelegate OnGetItemEvent;



    public delegate void OnInteractionDelegate(Player player, GameObject go);
    public event OnInteractionDelegate OnInteractionEvent;


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

    public void OnGetItem(Player player, ObjectInfo go)
    {
        if (OnGetItemEvent != null)
            OnGetItemEvent(player, go);
    }

    public void OnInteraction(Player player, GameObject go)
    {
        if (OnInteractionEvent != null)
            OnInteractionEvent(player, go);
    }

}
