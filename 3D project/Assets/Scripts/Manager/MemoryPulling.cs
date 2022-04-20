using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPulling
{
    private GameObject gameObject;

    private Queue<GameObject> objectQueue = new Queue<GameObject>();
    private Dictionary<string, Queue<GameObject>> dictionaryObject = new Dictionary<string, Queue<GameObject>>();


    public void SetMonsterObject(GameObject go)
    {
        gameObject = go;
    }


    //사용을 다한 오브젝트를 반납.
    public void DestroyObject(GameObject o)
    {
        objectQueue.Enqueue(o);
        o.SetActive(false);
    }

    /*
    리스트에서 미사용중인 오브젝트를 가져온다. 
    가져온 후 미사용은 사용중으로 바뀌며 그 오브젝트를 return해 준다.
    보류: 미사용중인 오브젝트가 없을 경우 새로운 오브젝트를 생성하고 추가하고 건내준다.
    */
    public GameObject GetObject()
    {
        if (objectQueue.Count == 0)
        {
            return GameObject.Instantiate(gameObject);
        }
        return objectQueue.Dequeue();
    }

    public int GetQueueCount()
    {
        return objectQueue.Count;
    }
}
