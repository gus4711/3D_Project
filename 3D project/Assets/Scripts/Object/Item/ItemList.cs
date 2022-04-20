using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListManager : Singleton<ItemListManager>
{
    Dictionary<string, GameObject> itemListDictionary = new Dictionary<string, GameObject>();
    Dictionary<int, GameObject> itemListIndexDictionary = new Dictionary<int, GameObject>();
    Dictionary<string, Sprite> itemImageListDictionary = new Dictionary<string, Sprite>();

    GameObject[] itemObjects;
    
    Sprite[] itemImage;


    private void Awake()
    {
        itemObjects = Resources.LoadAll<GameObject>("Object");
        itemImage = Resources.LoadAll<Sprite>("Image");

        foreach (GameObject go in itemObjects)
        {
            itemListDictionary.Add(go.GetComponent<ObjectInfo>().objectName, go);
            itemListIndexDictionary.Add(go.GetComponent<ObjectInfo>().itemIndex, go);
        }

        foreach (Sprite sp in itemImage)
        {
            itemImageListDictionary.Add(sp.name, sp);
        }
    }

    public GameObject FindItem(string s)
    {
        if (itemListDictionary.ContainsKey(s))
        {
            return itemListDictionary[s];
        }
        else
        {
            return null;
        }
    }

    public T FindItem<T>(string s)
    {
        if (itemListDictionary.ContainsKey(s))
        {
            return itemListDictionary[s].GetComponent<T>();
        }
        else
        {
            return default(T);
        }
    }

    public GameObject FindItem(int index)
    {
        if (itemListIndexDictionary.ContainsKey(index))
        {
            return itemListIndexDictionary[index];
        }
        else
        {
            return null;
        }
    }

    public Sprite FindSprite(string s)
    {
        if (itemImageListDictionary.ContainsKey(s))
        {
            return itemImageListDictionary[s];
        }
        else
        {
            return null;
        }
    }

    public GameObject GetItem(string s)
    {
        if (itemListDictionary.ContainsKey(s))
        {
            return GameObject.Instantiate<GameObject>(itemListDictionary[s]);
        }
        else
        {
            return null;
        }
    }
    public GameObject GetItem(string s, Transform parent)
    {
        if (itemListDictionary.ContainsKey(s))
        {
            return GameObject.Instantiate<GameObject>(itemListDictionary[s], parent);
        }
        else
        {
            return null;
        }
    }

    public T GetItem<T>(string s, Transform parent)
    {
        if (itemListDictionary.ContainsKey(s))
        {
            return GameObject.Instantiate<GameObject>(itemListDictionary[s], parent).GetComponent<T>();
        }
        else
        {
            return default(T);
        }
    }
}

