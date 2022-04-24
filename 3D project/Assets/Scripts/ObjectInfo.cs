using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour
{
    public int itemIndex;
    public string objectName;
    public int count = 1;

    [SerializeField] //내구성
    protected int _durability;
    

    [SerializeField] //장비템인가
    protected bool _isWeapon;

    [SerializeField] int _minDamage;
    public int minDamage { get { return _minDamage; } }
    [SerializeField] int _maxDamage;
    public int maxDamage { get { return _maxDamage; } }

    public bool isWeapon { get { return _isWeapon; } }

    public bool Mulipleobject;
    public bool IsPossibleUse;
    public Sprite itemImage;

    //managers
    EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;

        _eventManager.OnInteractionEvent += OnInteraction;
    }

    private void OnInteraction(Player player, GameObject go)
    {
        if (go.GetComponent<ObjectInfo>() == this)
        {
            Destroy(gameObject);
        }
    }
}
