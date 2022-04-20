using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    //slot info
    [SerializeField] int _slotIndex;
    [SerializeField] int _windowIndex;
    public int windowIndex { get { return _windowIndex; } set { _windowIndex = value; } }
    [SerializeField] int _itemIndex;
    public int itemIndex { get { return _itemIndex; } }
    [SerializeField] string _itemName;
    public string itemName { get { return _itemName; } set { _itemName = value; } }
    [SerializeField] int _itemCount;
    public int itemCount { get { return _itemCount; } set { _itemCount = value; _itemCountText.text = _itemCount.ToString(); } }
    [SerializeField] bool _IsPossibleUse;
    public bool isPossibleUse { get { return _IsPossibleUse; } set { _IsPossibleUse = value; } }
    [SerializeField] bool _isWeapon = false;
    public bool isWeapon { get { return _isWeapon; } set { _isWeapon = value; } }
    /////////////////////////
    [SerializeField]
    Image _itemImage;
    [SerializeField]
    TextMeshProUGUI _itemCountText;


    EventTrigger eventTrigger;
    EventTrigger.Entry et_entry;


    QuickSlot _quickSlot;


    public delegate void MDownHandler(Slot s, PointerEventData eventData);
    public event MDownHandler OnMDown;

    public delegate void MUpHandler(Slot s, PointerEventData eventData);
    public event MUpHandler OnMUp;

    public delegate void MClickHandler(Slot s, PointerEventData eventData);
    public event MClickHandler OnMClick;

    public delegate void MDragHandler(Slot s, PointerEventData eventData);
    public event MDragHandler OnMDrag;



    // Start is called before the first frame update


    // Update is called once per frame
    private void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();

        et_entry = new EventTrigger.Entry();
        et_entry.eventID = EventTriggerType.PointerUp;
        et_entry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        eventTrigger.triggers.Add(et_entry);

        et_entry = new EventTrigger.Entry();
        et_entry.eventID = EventTriggerType.PointerDown;
        et_entry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(et_entry);

        et_entry = new EventTrigger.Entry();
        et_entry.eventID = EventTriggerType.Drag;
        et_entry.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        eventTrigger.triggers.Add(et_entry);

        et_entry = new EventTrigger.Entry();
        et_entry.eventID = EventTriggerType.PointerClick;
        et_entry.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        eventTrigger.triggers.Add(et_entry);
    }

    public void SlotInit()
    {
        _itemIndex = -1;
        _itemName = "";
        _itemCount = 0;

        HideAll();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name+"-OnPointerClick");
        if(OnMClick != null)
            OnMClick(this, eventData);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name + "-OnPointerUp");
        if (OnMUp != null)
            OnMUp(this, eventData);
    }

    

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name + "-OnPointerDown");
        if (OnMDown != null)
            OnMDown(this, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(gameObject.name + "-OnDrag");
        if (OnMDrag != null)
            OnMDrag(this, eventData);
    }


    public virtual void ShowAll()
    {
        _itemImage.enabled = true;
        _itemCountText.enabled = true;
    }

    public void HideAll()
    {
        _itemImage.enabled = false;
        _itemCountText.enabled = false;
    }

    public void HideViewImage()
    {
        _itemImage.enabled = false;
    }
    public void HideViewCount()
    {
        _itemCountText.enabled = false;
    }

    public void ShowViewImage()
    {
        _itemImage.enabled = true;
    }
    public void ShowViewCount()
    {
        _itemCountText.enabled = true;
    }

    public void SetViewImage(Sprite image)
    {
        _itemImage.sprite = image;
        _itemImage.enabled = true;
    }

    public Sprite GetViewImage()
    {
        return _itemImage.sprite;
    }

    public void SetItemIndex(int index)
    {
        _itemIndex = index;
    }

    public int GetItemIndex()
    {
        return _itemIndex;
    }

    public void SetSlotIndex(int index)
    {
        _slotIndex = index;
    }

    public int GetSlotIndex()
    {
        return _slotIndex;
    }


    public void SetViewCount(string count)
    {
        if(count == "0")
        {
            _itemCountText.enabled = false;
        }
        else
        {
            _itemCountText.enabled = true;
        }
        _itemCountText.text = count;
    }

    public string GetViewCount()
    {
        return _itemCountText.text;
    }

    public int GetItemCount()
    {
        return _itemCount;
    }
    public void SetItemCount(int count)
    {
        _itemCount = count;
    }

    public string GetItemName()
    {
        return _itemName;
    }

    public void SetItemName(string name)
    {
        _itemName = name;
    }


    public void SetSlot(int index, int count, Sprite image, string name)
    {
        _itemIndex = index;
        _itemCount = count;
        SetViewCount(count.ToString());
        SetViewImage(image);
        _itemName = name;
    }

    void updateUI()
    {
        SetViewImage(_itemImage.sprite);
        SetViewCount(_itemCount.ToString());
    }


    public bool itemUse()
    {
        if (_IsPossibleUse)
        {
            _itemCount -= 1;
            if (_itemCount < 1)
            {
                SlotInit();
                return false;
            }
            else
            {
                updateUI();
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
