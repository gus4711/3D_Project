using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TMPro;

public class NPCTalk : MonoBehaviour
{
    [SerializeField] Transform _SelectMenu;
    [SerializeField] GameObject _SelectMenuBtn;
    [SerializeField] GameObject _Answer;

    [SerializeField] TextMeshProUGUI _talkPanelText;

    //NPC 데이터 자료
    NPCJson _npcJson;
    
    Dictionary<string, List<string>> _baseTalk = new Dictionary<string, List<string>>();
    Dictionary<string, List<JsonNomalTalk>> _nomalTalk = new Dictionary<string, List<JsonNomalTalk>>();
    Dictionary<string, List<JsonQuest>> _quest = new Dictionary<string, List<JsonQuest>>();

    //현재 선택 퀘스트
    JsonQuest _selectQuest;
    string _npcName;
    Player _player;
    

    //대화 범퍼
    List<string> _talkArray = new List<string>();
    int _talkArrayIndex;

    //대화 종료
    bool _talkEnd = false;

    private void Start()
    {
        var dataText = Resources.Load<TextAsset>("DATA/NPC_new");

        byte[] btyes = System.Text.Encoding.Default.GetBytes(dataText.ToString());
        string data = System.Text.Encoding.UTF8.GetString(btyes);

        JObject jo = JObject.Parse(data);

        foreach (JProperty jp in jo.Properties())
        {
            _npcName = jp.Name;
            string value = jp.Value.ToString();

            _baseTalk.Add(_npcName, JsonConvert.DeserializeObject<List<string>>(jo[_npcName]["baseTalk"].ToString()));

            _nomalTalk.Add(_npcName, JsonConvert.DeserializeObject<List<JsonNomalTalk>>(jo[_npcName]["talks"].ToString()));

            string ssss = jo[_npcName]["quest"].ToString();
            _quest.Add(_npcName, JsonConvert.DeserializeObject<List<JsonQuest>>(ssss));
        }

        gameObject.SetActive(false);
    }


    int childCount;
    int wantedObjectCount;

    List<GameObject> _selectMenuList = new List<GameObject>();
    GameObject addObjectTemp;
    List<ButtonPanel> _selectBtnList = new List<ButtonPanel>();
    public void Init(string npcName, Player player)
    {
        _talkEnd = false;

        //기본대화 설정
        _talkPanelText.text = _baseTalk[npcName][Random.Range(0, (int)_baseTalk[npcName].Count)];

        childCount = _SelectMenu.childCount;
        wantedObjectCount = _nomalTalk[npcName].Count + _quest[npcName].Count;

        

        //선택버튼 리스트 초기화
        if (childCount > wantedObjectCount)
        {
            _selectMenuList.RemoveRange(0, childCount - wantedObjectCount);
            _selectBtnList.RemoveRange(0, childCount - wantedObjectCount);

            for (int i = 0; i < (childCount - wantedObjectCount); i++)
            {
                GameObject.Destroy(_SelectMenu.GetChild(i).gameObject);
            }
        }
        else
        {
            for (int i = 0; i < (wantedObjectCount - childCount); i++)
            {
                addObjectTemp = GameObject.Instantiate<GameObject>(_SelectMenuBtn, _SelectMenu);
                _selectMenuList.Add(addObjectTemp);
                _selectBtnList.Add(addObjectTemp.GetComponent<ButtonPanel>());
            }
        }

        for (int i = 0; i < wantedObjectCount; i++)
        {
            if (i < _nomalTalk[npcName].Count)
            {
                _selectBtnList[i].nomalTalkInfo = _nomalTalk[npcName][i];
                _selectBtnList[i].clickNomalTalkEvent += NomalTalkBtnClick;
            }
            else
            {
                _selectBtnList[i].questInfo = _quest[npcName][i- _nomalTalk[npcName].Count];
                _selectBtnList[i].clickQuestEvent += QuestBtnClick;
            }
        }

        _player = player;
    }

    public void OkButton()
    {
        //플레이어한테 퀘스트 넘김
        _player.addQuest(_npcName, _selectQuest);
        _talkArray = _selectQuest.okTalk;
        _talkPanelText.text = _talkArray[0];
        _talkArrayIndex = 1;

        _Answer.SetActive(false);
        _talkEnd = true;
    }

    public void NoButton()
    {
        _talkArray = _selectQuest.noTalk;
        _talkPanelText.text = _talkArray[0];
        _talkArrayIndex = 1;

        _Answer.SetActive(false);
        _talkEnd = true;
    }

    public void QuestBtnClick(JsonQuest quest)
    {
        bool isClear = false;
        bool containQuest = false;
        for (int i = 0; i < _player.collectionQuestList.Count; i++)
        {
            if (_player.collectionQuestList[i].npcName == _npcName && _player.collectionQuestList[i].title == quest.title)
            {
                containQuest = true;
                for (int j = 0; j < _player.collectionQuestList[i].nowObject.Count; j++)
                {
                    if (_player.collectionQuestList[i].nowObject[j] >= quest.targetCount[j])
                    {
                        isClear = true;
                    }
                    else
                    {
                        isClear = false;
                        break;
                    }
                }
                break;
            }
        }

        _talkArrayIndex = 1;
        _selectQuest = quest;

        if (isClear)
        {
            //아이템 지급 로직
            if (_player.AddItem(quest.resultObject))
            {
                _talkArray = quest.successTalk;
                _talkPanelText.text = _talkArray[0];

                _player.RemoveQuest(_npcName, _selectQuest);
            }else
            {
                _talkArray = quest.resultFail;
                _talkPanelText.text = _talkArray[0];
            }
            //수락, 거절 버튼 활성화
            _Answer.SetActive(false);
            _talkEnd = true;
        }
        else
        {
            if (containQuest)
            {
                _talkArray = quest.notYet;
                _talkPanelText.text = _talkArray[0];

                //수락, 거절 버튼 활성화
                _Answer.SetActive(false);
            }
            else
            {
                _talkArray = quest.talk;
                _talkPanelText.text = _talkArray[0];

                //수락, 거절 버튼 활성화
                _Answer.SetActive(true);
            }
        }
    }
    public void NomalTalkBtnClick(JsonNomalTalk nomalTalk)
    {
        _talkArray = nomalTalk.talk;
        _talkPanelText.text = _talkArray[0];
        _talkArrayIndex = 1;

        //수락, 거절 버튼 비활성화
        _Answer.SetActive(false);
    }

    private void Update()
    {
        if (UIManager.GetInstance().isTalkNPC)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_talkArrayIndex < _talkArray.Count)
                {
                    _talkPanelText.text = _talkArray[_talkArrayIndex++];
                }else
                {
                    if (_talkEnd)
                    {
                        UIManager.GetInstance().NPCTalkEnd();
                    }
                }
            }
        }
    }
}
