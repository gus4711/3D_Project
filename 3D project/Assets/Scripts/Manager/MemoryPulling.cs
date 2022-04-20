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


    //����� ���� ������Ʈ�� �ݳ�.
    public void DestroyObject(GameObject o)
    {
        objectQueue.Enqueue(o);
        o.SetActive(false);
    }

    /*
    ����Ʈ���� �̻������ ������Ʈ�� �����´�. 
    ������ �� �̻���� ��������� �ٲ�� �� ������Ʈ�� return�� �ش�.
    ����: �̻������ ������Ʈ�� ���� ��� ���ο� ������Ʈ�� �����ϰ� �߰��ϰ� �ǳ��ش�.
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
