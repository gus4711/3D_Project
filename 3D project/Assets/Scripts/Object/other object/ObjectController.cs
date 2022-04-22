using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : ObjectControllerBase
{
    EventManager _eventManager;
    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _eventManager.OnDestoryObjectEvent += BeAttacked;
    }
}
