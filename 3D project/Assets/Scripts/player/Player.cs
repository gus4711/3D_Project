using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseInfo
{
    //managers
    EventManager _eventManager;

    //EVENT//

    public delegate void HMPSHandler(Player playerScript);
    public event HMPSHandler OnHMPSEvent;

    public delegate bool getItemsNameHandler(List<ResultObject> goName);
    public event getItemsNameHandler GetItemsNameEvent;

    public delegate void QuestItemUpdateHandler(Player player, ObjectInfo go, bool allCheck);
    public event QuestItemUpdateHandler QuestItemUpdateEvent;

    public delegate void QuestItemCheckHandler(Player player, CollectionQuest collectionQuest);
    public event QuestItemCheckHandler QuestItemCheck;

    public delegate void RemoveQuestItemHandler(JsonQuest quest);
    public event RemoveQuestItemHandler RemoveQuestItem;

    public delegate void NpcTalkHandler(string npcName, Player player);
    public event NpcTalkHandler OnNpcTalkEvent;

    public delegate void GameOverHandler();
    public event GameOverHandler GameOverEvent;

    public delegate void UseItemhandler();
    public event UseItemhandler UseItemEvent;


    ////////////////////


    public Transform playerHead;
    public GameObject playerBody;
    public float moveSpeed = 80f;
    public float runSpeed = 2f;

    
    public Animator armAnimator;

    public GameObject weapon;

    

    //에니메이션 스크립트 연결
    [SerializeField] PlayerAnimatorManager playerAnimator;
    
    //Camera
    float _AngleX;
    float _AngleY;

    //player move
    float _frontAndBack;
    float _leftAndRight;
    Rigidbody _userRigidbody;
    Vector3 _moveVector;
    Vector3 _cameraCenter = new Vector3(0.5f, 0.5f, 0);
    CharacterController _userController;

    TerrainDetector _terrainDectector;

    float _walkingTime;
    float _walkingCT;
    [SerializeField] float _walkingCoolTime;

    //뛰기
    bool _isRun = false;




    //마우스 조작
    bool _mouseLeftDown = false;
    bool _mouseRightDown = false;
    bool _isHaveWeapon = false;

    //앉기
    bool _isSitDown = false;


    //이동 초기화용
    Vector3 _VectorInit = new Vector3();

    //점프
    bool _isJump = false;
    bool _oneRunJump = false;
    [SerializeField] float _gravityAlpha = 0.1f;
    [SerializeField] float _jumpAlpha = 0.1f;
    [SerializeField] float _jumpMaxSpeed = 10f;

    //공격
    public float _attackCoolTime = 1f; //1 second
    float _attackTime;
    GameObject TargetObject;
    bool _isAttacking;

    //방어
    float _shieldTime;
    [SerializeField] float _shieldCoolTime;
    bool _isShieldOn;
    bool _isParrying = false;
    float _parryingTime;
    [SerializeField] float _parryingCoolTime;

    //스테미나 관련
    float _staminaTime;
    [SerializeField]
    float _staminaTimeAlpha;
    [SerializeField]
    float _stCoolTime;

    float _staminaChargeTime;
    [SerializeField]
    float _staminaChargeTimeAlpha;
    [SerializeField]
    float _sctCoolTime;


    //파밍, 아이템 사용
    bool _isActionKey_E = false;
    bool _isgetItemOneClick = true;

    bool _useItem;


    //장착 아이템
    Slot _equipmentItemSlot;
    Slot _equipmentTempSlot;

    //퀘스트
    List<CollectionQuest> _CollectionQuestList;
    public List<CollectionQuest> collectionQuestList { get { return _CollectionQuestList; } }


    private void Awake()
    {
        //managers
        _eventManager = EventManager.Instance;


        _CollectionQuestList = new List<CollectionQuest>();
        

        _userController = gameObject.GetComponent<CharacterController>();

        //공격 애니메이션 이벤트 연결
        playerAnimator.HitEvent += OnTargetAttack;
        playerAnimator.AttackStartEvent += OnAttackStart;
        playerAnimator.AttackEndEvent += OnAttackEnd;

        //공격 이벤트

         _userRigidbody = playerBody.GetComponent<Rigidbody>();

        if (weapon.transform.childCount > 0)
        {
            armAnimator.SetBool("HaveWeapon", weapon.transform.GetChild(0).GetComponent<ObjectInfo>().isWeapon);
            armAnimator.SetFloat("haveItem", (armAnimator.GetBool("HaveWeapon")) ? 1 : 0);
        }
        _terrainDectector = new TerrainDetector();

    }

    void Update()
    {
        _attackTime += Time.deltaTime;
        if (UIManager.GetInstance().isControlCharacter)
        {
            CameraUpdate();
            InputKey();
            characterController();
            KeyActionE();
            UseItemCheck();
        }else
        {
            Init();
        }
        StaminaAutoCharge();
    }

    private void Init()
    {
        _mouseLeftDown = false;
        _mouseRightDown = false;
        _isSitDown = false;
        _isRun = false;
        _isActionKey_E = false;
        _useItem = false;
    }

    private void FixedUpdate() {
        MouseControl();
        SitDownAction();
    }

    private void SitDownAction()
    {
        if (_isSitDown)
        {
            _userController.height = 1f;
        }
        else
        {
            _userController.height = 2f;
        }
        
    }

    //카메라 시선 처리
    private void CameraUpdate(){

        _AngleX += Input.GetAxis("Mouse X");
        _AngleY += Input.GetAxis("Mouse Y");
        _AngleY = Mathf.Clamp(_AngleY, -90f, 90f);

        playerBody.transform.rotation = Quaternion.Euler(0, _AngleX, 0);
        playerHead.rotation = Quaternion.Euler(-_AngleY, playerBody.transform.rotation.eulerAngles.y, 0);

        //characterneck.rotation = Quaternion.Euler(defaultNeck.x, defaultNeck.y, -AngleY / 4 + defaultNeck.z);
        //characterHead.rotation = Quaternion.Euler(defaultHead.x, defaultHead.y, -AngleY / 4 + defaultHead.z);


        _moveVector = new Vector3();
    }
    //움직임 입력키 처리
    private void InputKey(){

        //움직임
        _frontAndBack = Input.GetAxis("Vertical");
        _leftAndRight = Input.GetAxis("Horizontal");

        //좌클릭
        if (Input.GetMouseButtonDown(0))
        {
            _mouseLeftDown = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            _mouseLeftDown = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            _mouseRightDown = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _mouseRightDown = false;
        }

        //////////////컨트롤
        //앉기
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _isSitDown = true;
        }
        else
        {
            _isSitDown = false;
        }

        //뛰기
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isRun = true;
        }
        else
        {
            _isRun = false;
        }

        //파밍
        if (Input.GetKey(KeyCode.E))
        {
            _isActionKey_E = true;
        }
        else
        {
            _isActionKey_E = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _useItem = true;
        }
        else
        {
            _useItem = false;
        }

        //점프
        if (Input.GetKey(KeyCode.Space))
        {
            _isJump = true;
        }
        else
        {
            //_isJump = false;
        }
    }
    //움직임 처리

    float _lastGVelocity = 0;
    float _G_velocity = 0;
    

    private void characterController()
    {
        _moveVector = Vector3.zero;

        _moveVector.y = _lastGVelocity;

        if (_frontAndBack > 0)
            _moveVector += playerBody.transform.forward * _frontAndBack;
        else if (_frontAndBack < 0)
            _moveVector -= playerBody.transform.forward * -_frontAndBack;

        if (_leftAndRight > 0)
            _moveVector += playerBody.transform.right * _leftAndRight;
        else if (_leftAndRight < 0)
            _moveVector -= playerBody.transform.right * -_leftAndRight;


        _moveVector = _moveVector * moveSpeed;

        if (_isSitDown)
        {
            _moveVector = _moveVector * 0.5f;
        }
        else if(_isRun)
        {
            _moveVector = _moveVector * 2f;
        }

        if (!_userController.isGrounded)
        {
            _G_velocity = _lastGVelocity - 1f * _gravityAlpha;
        }else
        {
            _G_velocity = 0;
        }
        _moveVector.y = _G_velocity;

        //점프 보류-> 1회점프 처리 안되어 있음
        //if (_isJump)
        //{
        //    _moveVector.y += 1f * _jumpAlpha;
        //    if (_moveVector.y > _jumpMaxSpeed)
        //    {
        //        _moveVector.y = _jumpMaxSpeed;
        //        _isJump = false;
        //    }
        //}
        _userController.Move(_moveVector* Time.deltaTime);
        _lastGVelocity = _moveVector.y;

        //걷기 사운드
        //Debug.Log();

        if (_moveVector.x != 0 || _moveVector.z != 0)
        {
            WalkingSound();
        }

    }

    Ray ray;
    RaycastHit raycasthit;


    private void MouseControl()
    {
        _shieldTime += Time.deltaTime;
        if (_mouseLeftDown)
        {
            if(stamina > 0 && !_isAttacking)
            {
                //스테미나
                stamina -= 5;
                if (stamina < 0)
                {
                    stamina = 0;
                }
                OnHMPSEvent(this);
                _staminaTime = 0;

                _attackTime = 0;
                

                ray = Camera.main.ViewportPointToRay(_cameraCenter);
                if (Physics.Raycast(ray, out raycasthit,2f, 1<<LayerMask.NameToLayer("PossibleHit")))
                {
                    armAnimator.SetBool("isHit", true);
                }else
                {
                    armAnimator.SetBool("isHit", false);
                }


                //모션
                armAnimator.SetTrigger("attack");
                _isAttacking = true;
            }
        }
        else if (_mouseRightDown)
        {
            if (stamina > 0 && !_isAttacking && _isHaveWeapon) //공격중x, 무기o
            {
                _isShieldOn = true;
                if (_shieldTime > _shieldCoolTime)
                {
                    _shieldTime = 0;

                    //스테미나 감소
                    stamina -= 1;
                    if (stamina < 0)
                    {
                        stamina = 0;
                    }
                    OnHMPSEvent(this);
                    _staminaTime = 0;
                }
                _parryingTime += Time.deltaTime;
                if (_parryingTime < _parryingCoolTime) //패링 시간
                {
                    _isParrying = true;
                }else
                {
                    _isParrying = false;
                }
                armAnimator.SetBool("guard", true);

            }
            else
            {
                armAnimator.SetBool("guard", false);
                _isShieldOn = false;
                _isParrying = false;
                _parryingTime = 0;
            }
        }
        else
        {
            armAnimator.SetBool("guard", false);
            _isShieldOn = false;
            _isParrying = false;
            _parryingTime = 0;
        }
    }

    
    void OnTargetAttack()
    {
        TargetObject = raycasthit.collider.gameObject;

        if (TargetObject.tag == "Object")
        {
            //TargetObject.GetComponent<ObjectController>().BeAttacked(this);

            _eventManager.OnDestroyObject(this, TargetObject.GetComponent<ObjectController>());

            if (TargetObject.GetComponent<ObjectController>().objectName == "Tree")
            {
                AudioManager.GetInstance().OnShotPlay("WoodAxe_Tree");
            }
            else if (TargetObject.GetComponent<ObjectController>().objectName == "Stone")
            {
                AudioManager.GetInstance().OnShotPlay("PickAxe_Stone");
            }
        }
        else if (TargetObject.tag == "Monster")
        {
            _eventManager.OnPlayerAttack(this, TargetObject.GetComponent<AIBase>());

            AudioManager.GetInstance().OnShotPlay("Hit_Impact_Sword");
        }
    }

    private void OnDestroyObject(Player player, ObjectController objectController)
    {
        if (player == this)
        {

        }
    }



    void OnAttackStart()
    {
        //Debug.Log("attack start");
        //_isAttacking = true;
    }

    void OnAttackEnd()
    {
        _isAttacking = false;
    }

    void UseItemCheck()
    {
        if (_useItem)
        {
            if (_equipmentItemSlot != null)
            {
                ObjectInfo ooo = ItemListManager.GetInstance().FindItem<ObjectInfo>(_equipmentItemSlot.itemName);
                if ((!_equipmentItemSlot.isWeapon) && ooo.IsPossibleUse)
                {
                    AudioManager.GetInstance().OnShotPlay("eat");
                    if (_equipmentItemSlot.itemCount == 1)
                    {
                        UseItem(_equipmentItemSlot);
                        _equipmentTempSlot = _equipmentItemSlot;
                        RemoveEquipItem();
                        _equipmentTempSlot.itemUse();
                    }else
                    {
                        UseItem(_equipmentItemSlot);
                        _equipmentItemSlot.itemUse();
                    }
                }
            }
        }
    }
    

    void KeyActionE()
    {
        if (_isActionKey_E)
        {
            if (_isgetItemOneClick)
            {
                _isgetItemOneClick = false;
                
                ray = Camera.main.ViewportPointToRay(_cameraCenter);
                if (Physics.Raycast(ray, out raycasthit, 2f, (1 << LayerMask.NameToLayer("ImpossibleHit"))|(1<<LayerMask.NameToLayer("NPC"))))
                {
                    _eventManager.OnInteraction(this, raycasthit.collider.gameObject);
                    

                    //if(raycasthit.collider.tag == "item")
                    //{
                    //    ObjectInfo oi = raycasthit.collider.GetComponent<ObjectInfo>();

                    //    _eventManager.OnGetItem(this, oi);
                    //    GameObject.Destroy(raycasthit.collider.gameObject);
                    //    QuestItemUpdateEvent(this, oi, false);
                    //}else if(raycasthit.collider.tag == "npc")
                    //{
                    //    OnNpcTalkEvent(raycasthit.collider.gameObject.GetComponent < NPC>().npcName, this);
                    //}
                }
            }
        }
        else
        {
            _isgetItemOneClick = true;
        }
    }

    void StaminaAutoCharge()
    {
        _staminaTime += Time.deltaTime;
        if(_staminaTime * _staminaTimeAlpha > _stCoolTime) //스테미나 차오름 초기 대기
        {
            _staminaTime = _stCoolTime + 1;

            _staminaChargeTime += Time.deltaTime;
            if(_staminaChargeTime * _staminaChargeTimeAlpha > _sctCoolTime)
            {
                _staminaChargeTime = 0;

                stamina += 1;
                OnHMPSEvent(this);

                if (stamina > maxStamina)
                {
                    stamina = maxStamina;
                    
                }
            }
        }
    }


    public void AddEquipItem(Slot slot)
    {
        if (_equipmentItemSlot != null)
        {
            addMinDamage -= ItemListManager.GetInstance().FindItem(_equipmentItemSlot.itemIndex).GetComponent<ObjectInfo>().minDamage;
            addMaxDamage -= ItemListManager.GetInstance().FindItem(_equipmentItemSlot.itemIndex).GetComponent<ObjectInfo>().maxDamage;

            _equipmentItemSlot = null;
        }


        _equipmentItemSlot = slot;
        Debug.Log("add equip item");
        addMinDamage += ItemListManager.GetInstance().FindItem(slot.itemIndex).GetComponent<ObjectInfo>().minDamage;
        addMaxDamage += ItemListManager.GetInstance().FindItem(slot.itemIndex).GetComponent<ObjectInfo>().maxDamage;

        if (weapon.transform.childCount > 0)
        {
            for (int i = 0; i < weapon.transform.childCount; i++)
            {
                GameObject.Destroy(weapon.transform.GetChild(i).gameObject);
            }
        }

        ObjectInfo item = ItemListManager.GetInstance().GetItem<ObjectInfo>(slot.GetItemName(), weapon.transform);
        _isHaveWeapon = item.isWeapon;
        armAnimator.SetBool("HaveWeapon", item.isWeapon);
        armAnimator.SetFloat("haveItem", (armAnimator.GetBool("HaveWeapon") || item.IsPossibleUse) ? 1 : 0);
    }

    public void RemoveEquipItem()
    {
        if(_equipmentItemSlot != null)
        {
            addMinDamage -= ItemListManager.GetInstance().FindItem(_equipmentItemSlot.itemIndex).GetComponent<ObjectInfo>().minDamage;
            addMaxDamage -= ItemListManager.GetInstance().FindItem(_equipmentItemSlot.itemIndex).GetComponent<ObjectInfo>().maxDamage;

            GameObject.Destroy(weapon.transform.GetChild(0).gameObject);
            _equipmentItemSlot = null;
            Debug.Log("take out the equipment");
        }

        armAnimator.SetBool("HaveWeapon", false);
        armAnimator.SetFloat("haveItem", 0);
    }

    public bool BeAttacked(AIBase ai)
    {
        if (hp > 0)
        {
            if (_isShieldOn)
            {
                if (_isParrying)
                {
                    AudioManager.GetInstance().OnShotPlay("Parrying_Sword");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                int debugD = Random.Range(ai.minDamage + ai.addMinDamage, ai.maxDamage + ai.addMaxDamage);
                Debug.Log("debug D: " + debugD);
                hp -= debugD;

                OnHMPSEvent(this);

                if (hp < 0)
                {
                    GameOverEvent();
                    gameObject.layer = LayerMask.NameToLayer("ImpossibleHit");

                    //재료 드랍
                    Debug.Log("object드랍");
                }

                //애니메이션
                //_aiAnimator.SetTrigger("hit");

                return false;
            }
        }
        else
        {
            return false;
        }
        
    }

    void WalkingSound()
    {
        _walkingTime += Time.deltaTime;
        if (_isRun)
        {
            _walkingCT = _walkingCoolTime / 2f;
        }else
        {
            _walkingCT = _walkingCoolTime;
        }
            
        if (_walkingTime > _walkingCT)
        {
            _walkingTime = 0;
            //AudioManager.GetInstance().WalkingPlay(_terrainDectector.GetActiveTerrainTextureIdx(gameObject.transform.position));
            AudioManager.GetInstance().WalkingPlay(0);
        }
    }

    bool attackCheck()
    {
        if (armAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            Debug.Log("return: true;");
            return true;
        }else
        {
            Debug.Log("return: false;");
            return false;
        }
    }


    public void addQuest(string npcName, JsonQuest quest)
    {
        if (quest.questType == 0)
        {
            _CollectionQuestList.Add(new CollectionQuest(npcName, quest));
        }
        else if (quest.questType == 1)
        {
            _CollectionQuestList.Add(new CollectionQuest(npcName, quest));
        }

        QuestItemCheck(this, _CollectionQuestList[_CollectionQuestList.Count - 1]);
    }

    public void RemoveQuest(string npcName, JsonQuest quest)
    {
        for (int i = 0; i < _CollectionQuestList.Count; i++)
        {
            if (_CollectionQuestList[i].npcName == npcName && _CollectionQuestList[i].title == quest.title)
            {
                _CollectionQuestList.RemoveAt(i);
                if(quest.questType ==0)
                    RemoveQuestItem(quest);
                return;
            }
        }
    }


    public void QuestMonsterUpdate(AIBase monster)
    {
       for (int i = 0; i < _CollectionQuestList.Count; i++)
       {
            for (int j = 0; j < _CollectionQuestList[i].targetObject.Count; j++)
            {
                if (_CollectionQuestList[i].targetObject[j] == monster.objectName)
                {
                    _CollectionQuestList[i].nowObject[j] += 1;
                }
            }
       }
    }

    public void addItem(ObjectInfo objectinfo)
    {
        _eventManager.OnGetItem(this, objectinfo);
    }
    public bool AddItem(List<ResultObject> resultObject)
    {
        return GetItemsNameEvent(resultObject);
    }

    void UseItem(Slot item)
    {
        if (item.itemName == "Bread")
        {
            _hp += 15;
            if (_hp > _maxHp)
            {
                _hp = _maxHp;
            }
        }
    }
}
