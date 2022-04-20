using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI _text;
    JsonQuest _questInfo;
    JsonNomalTalk _nomalTalkInfo;

    public JsonQuest questInfo { get { return _questInfo;  } set { _questInfo = value; _text.text = _questInfo.title; } }
    public JsonNomalTalk nomalTalkInfo { get { return _nomalTalkInfo; } set { _nomalTalkInfo = value; _text.text = _nomalTalkInfo.title; } }

    public string text { get { return _text.text; } set { _text.text = value; } }

    public delegate void ClickQuestHandler(JsonQuest quest);
    private ClickQuestHandler _clickQuestEvent;
    public event ClickQuestHandler clickQuestEvent
    {
        add
        {
            _clickQuestEvent = value;
        }remove
        {
            _clickQuestEvent += value;
        }
    }

    public delegate void ClickNomalTalkHandler(JsonNomalTalk nomalTalk);
    private ClickNomalTalkHandler _clickNomalTalkEvent;
    public event ClickNomalTalkHandler clickNomalTalkEvent
    {
        add
        {
            _clickNomalTalkEvent = value;
        }
        remove
        {
            _clickNomalTalkEvent += value;
        }
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (_clickNomalTalkEvent != null)
        {
            _clickNomalTalkEvent(_nomalTalkInfo);
        }
        if (_clickQuestEvent != null)
        {
            _clickQuestEvent(_questInfo);
        }
    }

    public void ButtonInit()
    {
        _questInfo = null;
        _nomalTalkInfo = null;
    }
    


    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
