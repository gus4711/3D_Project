using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    [SerializeField] GameObject[] _MonsterSpawn;
    AIBase[] _monsterAIBase;


    float[] _spawnTime;
    [SerializeField] float[] _spawnCoolTime;

    GameObject _tempGo;


    // Start is called before the first frame update
    void Start()
    {
        //각각의 몬스터 시간 체크
        _spawnTime = new float[_MonsterSpawn.Length];
        //_spawnCoolTime = new float[_MonsterSpawn.Length];
        _monsterAIBase = new AIBase[_MonsterSpawn.Length];

        for (int i = 0; i < _monsterAIBase.Length; i++)
        {
            _monsterAIBase[i] = _MonsterSpawn[i].GetComponent<AIBase>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _MonsterSpawn.Length; i++)
        {
            if (_MonsterSpawn[i].activeSelf)
            {//해당 몬스터 살아 있음
                _spawnTime[i] = 0;
                continue; 
            }else
            {
                _spawnTime[i] += Time.deltaTime;
            }
        }


        for (int i = 0; i < _spawnTime.Length; i++)
        {
            if (_spawnTime[i] > _spawnCoolTime[i])
            {
                _tempGo = MonsterPullingManager.GetInstance().GetObject(_monsterAIBase[i].objectName);
                _tempGo.transform.position = _monsterAIBase[i].spawnPosition.position;
                _tempGo.transform.rotation = _monsterAIBase[i].spawnPosition.rotation;
                _monsterAIBase[i].AIInit();
                _tempGo.SetActive(true);
                
            }
        }
    }
}
