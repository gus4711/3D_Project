using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBase : BaseInfo
{
    //managers
    EventManager _eventManager;


    [SerializeField] Animator _aiAnimator;
    [SerializeField] Player _player;

    NavMeshAgent _agent;

    SphereCollider _sphereCollider;

    

    //스폰위치
    [SerializeField] Transform _SpawnPosition;
    public Transform spawnPosition { get { return _SpawnPosition; } }

    //죽음 시체 유지시간
    float _dieTime;
    [SerializeField] float _dieCoolTime = 3f;


    //플레이어와의 간격
    float _playerAiDistance;

    bool _attackStart = false;

    bool _isAttacking = false;



    //공격용 변수
    Vector3 _attackBoxP2 = new Vector3(1 , 1, 2);

    

    float _attackTime;
    [SerializeField] float _attackCoolTime = 5f;

    bool _attackMode;

   

    float _flowingTime = 0;
    [SerializeField] float _flowingCoolTime = 2f;


    //스턴
    bool _stern;
    float _sternTime;
    [SerializeField] float _sternCoolTIme = 3f;

    private void Awake()
    {
        //managers
        _eventManager = EventManager.Instance;
        _eventManager.OnPlayerAttackEvent += OnBeAttack;


        _agent = gameObject.GetComponent<NavMeshAgent>();
        _agent.stoppingDistance = 2f;
        _sternTime = 0;

        _sphereCollider = gameObject.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null && (!_stern))
        {
            _flowingTime += Time.deltaTime;
        }

        if (_flowingTime > _flowingCoolTime)
        {
            _player = null;

            _agent.SetDestination(_SpawnPosition.position);

            gameObject.transform.LookAt(_SpawnPosition);

            if (Vector3.Distance(gameObject.transform.position, _SpawnPosition.position) < _agent.stoppingDistance)
            {
                _aiAnimator.SetBool("walk", false);
                gameObject.GetComponent<SphereCollider>().enabled = true;
            }
        }

        if (_stern)
        {
            _sternTime += Time.deltaTime;
            if (_sternTime > _sternCoolTIme)
            {
                _sternTime = 0;
                _stern = false;
                _aiAnimator.SetBool("Dizzy", false);
            }
        }
        else
        {
            if (_player != null)
            {
                _playerAiDistance = Vector3.Distance(gameObject.transform.position, _player.transform.position);

                if (hp > 0)
                {
                    _attackTime += Time.deltaTime;
                    AutoMove();

                    if (_attackMode)
                    {
                        if (_attackTime > _attackCoolTime)
                        {
                            _attackTime = 0;
                            _aiAnimator.SetTrigger("attacked");
                            _flowingTime = 0;
                        }
                    }

                }
            }
        }

        
        if (hp < 1)
        {
            _player = null;
            _dieTime += Time.deltaTime;
            if (_dieTime > _dieCoolTime)
            {
                MonsterPullingManager.GetInstance().Destory(gameObject);
            }
        }


    }


    private void FixedUpdate()
    {

    }



    void AutoMove()
    {
        if (!CheckAttackMotion())
        {
            
            _agent.SetDestination(_player.transform.position);

            if (_playerAiDistance < _agent.stoppingDistance)
            {
                gameObject.transform.LookAt(_player.transform);
                _aiAnimator.SetBool("walk", false);
                _attackMode = true;
            }
            else
            {
                _aiAnimator.SetBool("walk", true);
                _attackMode = false;
            }
        }
        else
        {
            _agent.ResetPath();
        }
    }

    void TargetMove()
    {
        //추적 이동
    }


    public bool BeAttacked(Player player)
    {
        if(hp > 0)
        {
            int debugD = Random.Range(player.minDamage + player.addMinDamage, player.maxDamage + player.addMaxDamage);
            hp -= debugD;

            //데미지 창
            UIManager.GetInstance().ObjectHP.SetName(objectName);
            UIManager.GetInstance().ObjectHP.SetHpBar((float)hp / (float)maxHp);
            UIManager.GetInstance().ObjectHP.ViewHP();

            if (hp < 0)
            {
                _aiAnimator.SetBool("Die", true);
                _aiAnimator.SetTrigger("hit");
                gameObject.layer = LayerMask.NameToLayer("ImpossibleHit");
                //재료 드랍
                Debug.Log("object드랍");
                return true;
            }

            if (!_aiAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
            {
                //애니메이션
                _aiAnimator.SetTrigger("hit");
            }
            return false;
        }else
        {
            return true;
        }
    }

    public void OnBeAttack(Player player, AIBase aiBase)
    {
        if (aiBase == this)
        {
            if (hp > 0)
            {
                int debugD = Random.Range(player.minDamage + player.addMinDamage, player.maxDamage + player.addMaxDamage);
                hp -= debugD;

                //데미지 창
                UIManager.GetInstance().ObjectHP.SetName(objectName);
                UIManager.GetInstance().ObjectHP.SetHpBar((float)hp / (float)maxHp);
                UIManager.GetInstance().ObjectHP.ViewHP();

                if (hp < 0)
                {
                    _aiAnimator.SetBool("Die", true);
                    _aiAnimator.SetTrigger("hit");
                    gameObject.layer = LayerMask.NameToLayer("ImpossibleHit");
                    //재료 드랍
                    Debug.Log("object드랍");
                    player.QuestMonsterUpdate(aiBase);
                }

                if (!_aiAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack01"))
                {
                    //애니메이션
                    _aiAnimator.SetTrigger("hit");
                }
            }
            else
            {
                player.QuestMonsterUpdate(aiBase);
            }
        }
    }



    void AttackStart()
    {

    }

    void AttackEnd()
    {

    }


    void AttackMotion()
    {
        Collider[] hit;
        Debug.Log("attackMotion");



        hit = Physics.OverlapBox(gameObject.transform.position, _attackBoxP2, gameObject.transform.rotation, LayerMask.GetMask("Player"));


        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log("player hitttt");
            _stern = _player.BeAttacked(this);
            if (_stern)
            {
                _aiAnimator.SetBool("Dizzy", true);
            }
        }
        

        //RaycastHit[] hits;

        //hits = Physics.BoxCastAll(gameObject.transform.position, _attackBoxP2, gameObject.transform.forward, gameObject.transform.rotation, 5f, LayerMask.GetMask("Player"));

        //for (int i = 0; i < hits.Length; i++)
        //{
        //    Debug.Log("hit: " + hits[i].collider.name);
        //}


    }

    bool CheckAttackMotion()
    {
        if (_aiAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            return true;
        }else
        {
            return false;
        }
    }

    //영역안에 플레이어 접근
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && other.gameObject.layer != LayerMask.NameToLayer("ImpossibleHit"))
        {
            _flowingTime = 0;
            _player = other.gameObject.GetComponent<Player>();
            _sphereCollider.enabled = false;

            _player.GameOverEvent -= TargetGameOver;
            _player.GameOverEvent += TargetGameOver;
        }
    }

    private void TargetGameOver()
    {
        _player = null;

        _agent.SetDestination(_SpawnPosition.position);

        gameObject.transform.LookAt(_SpawnPosition);

        if (Vector3.Distance(gameObject.transform.position, _SpawnPosition.position) < _agent.stoppingDistance)
        {
            _aiAnimator.SetBool("walk", false);
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }

    public void AIInit()
    {
        _dieTime = 0;
        _attackTime = 0;
        _flowingTime = 0;
        _sternTime = 0;

        _sphereCollider.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("PossibleHit");
        _hp = _maxHp;
    }
}
