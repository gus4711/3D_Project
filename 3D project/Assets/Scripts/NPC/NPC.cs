using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System;

public class NPC : MonoBehaviour
{
    [SerializeField] string _npcName;
    public string npcName { get { return _npcName; } }
    // Start is called before the first frame update

    //managers
    EventManager _eventManager;
    UIManager _UIManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _UIManager = UIManager.Instance;

        _eventManager.OnInteractionEvent += OnInteraction;
    }

    private void OnInteraction(Player player, GameObject go)
    {
        if (gameObject == go)
        {
            _UIManager.NpcTalk(_npcName, player);
        }
    }
}
