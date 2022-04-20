using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] ObjectHP _hpBar;

    public ObjectHP ObjectHP { get { return _hpBar; } }


    [SerializeField] Player _palyer;
    public Player player { get { return _palyer; } }
    [SerializeField] GameObject _menu;
    public GameObject menu { get { return _menu; } }
    [SerializeField] GameObject _inventory;
    public GameObject inventory { get { return _inventory; } }
    [SerializeField] GameObject _crafte;
    public GameObject crafte { get { return _crafte; } }
    //[SerializeField] GameObject _quickSlotWindow;
    //public GameObject quickSlotWindow { get { return _quickSlotWindow; } }
    [SerializeField] GameObject _npcTack;
    NPCTalk npcTalk;
    public GameObject npcTack { get { return _npcTack; } }
    [SerializeField] GameObject _gameOverPanel;
    public GameObject gameOverPanel { get { return _gameOverPanel; } }

    //마우스 제어 체크용
    //윈도우 오픈 시 캐릭터 제어
    bool _isControlCharacter;
    public bool isControlCharacter { get { return _isControlCharacter; } set { _isControlCharacter = value; } }

    //창 열려있으면 True
    bool _isWindowOn;
    public bool isWindowOn { get { return _isWindowOn; } }

    //NPC와 말 하고있으면 True
    bool _isTalkNPC;
    public bool isTalkNPC { get { return _isTalkNPC; } }

    //연속 눌림 방지
    bool _OnClick;


    private void Start()
    {
        _palyer.OnNpcTalkEvent += NpcTalk;
        _palyer.GameOverEvent += GameOver;
        npcTalk = _npcTack.GetComponent<NPCTalk>();
    }



    // Update is called once per frame
    public void Update()
    {
        CheckWindowOnOff();
        CharacterControl();

        if (!Input.anyKey)
        {
            _OnClick = true;
            return;
        }
        if (_OnClick)
        {
            _OnClick = false;
            Ui();
        }
    }

    void CharacterControl()
    {
        
        //마우스 제어
        if(_isWindowOn || _isTalkNPC)
        {
            _isControlCharacter = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            _isControlCharacter = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Ui()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Menu();
        }

        if (!_menu.activeSelf)
        {
            if (Input.GetKey(KeyCode.I))
            {
                _inventory.gameObject.SetActive(!_inventory.gameObject.activeSelf);

            }

            if (Input.GetKey(KeyCode.M))
            {
                if (!_inventory.gameObject.activeSelf)
                {
                    _inventory.gameObject.SetActive(!_inventory.gameObject.activeSelf);
                }
                _crafte.gameObject.SetActive(!_crafte.gameObject.activeSelf);

            }
        }
    }

    void CheckWindowOnOff()
    {
        if (_menu.activeSelf || _inventory.activeSelf || _crafte.activeSelf || _gameOverPanel.activeSelf)
        {
            _isWindowOn = true;
        }else
        {
            _isWindowOn = false;
        }
    }

    void Menu()
    {
        //UI 켜져있으면 OFF,
        if (_isWindowOn||_isTalkNPC)
        {
            WindowAllOff();
        }
        else
        {
            _menu.SetActive(!_menu.activeSelf);
        }
    }

    public void WindowAllOff()
    {
        _menu.SetActive(false);
        _inventory.SetActive(false);
        _crafte.SetActive(false);
        _isWindowOn = false;
        _isTalkNPC = false;
        _npcTack.SetActive(false);
        _gameOverPanel.SetActive(false);
        Debug.Log("talk exit");
    }


    void NpcTalk(string npcName, Player player)
    {
        Debug.Log("talk start");
        _npcTack.SetActive(true);
        _isTalkNPC = true;
        npcTalk.Init(npcName, player);
    }

    public void NPCTalkEnd()
    {
        _npcTack.SetActive(false);
        _isTalkNPC = false;
    }

    public void GameOver(bool onOff)
    {
        _gameOverPanel.SetActive(onOff);
    }

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }
}
