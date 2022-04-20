using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPullingManager:Singleton<MonsterPullingManager>
{
    Dictionary<string, GameObject> _prefabList = new Dictionary<string, GameObject>();

    Dictionary<string, Queue<GameObject>> _PullingLIst = new Dictionary<string, Queue<GameObject>>();

    void Init()
    {

    }

    //object 가져오기
    //없으면 새로 생성하여 반환. 있으면 있는 큐에서 반환.
    public GameObject GetObject(string objectName)
    {
        if (_PullingLIst.ContainsKey(objectName))
        {
            if (_PullingLIst[objectName].Count == 0)
            {
                return GameObject.Instantiate(_prefabList[objectName]);
            }else
            {
                return _PullingLIst[objectName].Dequeue();
            }
        }else
        {
            _prefabList.Add(objectName, Resources.Load<GameObject>(""));
            return GameObject.Instantiate(_prefabList[objectName]);
        }
    }

    public void Destory(GameObject go)
    {
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        if (_PullingLIst.ContainsKey(go.GetComponent<AIBase>().objectName))
        {
            _PullingLIst[go.GetComponent<AIBase>().objectName].Enqueue(go);
        }else
        {
            _PullingLIst.Add(go.GetComponent<AIBase>().objectName, new Queue<GameObject>());
            _PullingLIst[go.GetComponent<AIBase>().objectName].Enqueue(go);
        }
        
        go.SetActive(false);
    }

}
