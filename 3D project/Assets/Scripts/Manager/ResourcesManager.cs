using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    //item list
    public GameObject[] itemObjects;
    public Sprite[] itemImage;

    private void Awake()
    {
        //item list
        itemObjects = Resources.LoadAll<GameObject>("Object");
        itemImage = Resources.LoadAll<Sprite>("Image");
    }
}
