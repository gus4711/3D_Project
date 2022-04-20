using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotWindowBase : MonoBehaviour
{
    [SerializeField] protected Inventory _inventory;
    [SerializeField] protected Crafte _crafte;
    [SerializeField] protected QuickSlotWindow _quickWindow;


    [SerializeField] protected Player user;
    [SerializeField] protected GameObject slotObject;
    [SerializeField] protected Transform slotParent;
    [SerializeField] protected MovingItem movingItem;




    protected GameObject slotObjectTemp;
    protected Slot slotScriptTemp;



    //슬롯 추가
    [SerializeField]
    protected List<Slot> slotList;


    //슬롯 검색
    protected Slot LastSlot;
    protected GameObject[] _quickSlots;
    protected int emtySlotCount;
    protected bool addPossibleSlot;
    protected ObjectInfo checkObjectInfo;


    //슬롯 클릭
    protected Slot clickDownSlot;
    protected Slot clickUpSlot;
    protected bool stayClickItem;
    protected int calTemp1, calTemp2;

    protected Slot tempSlot;

    //더블클릭
    protected float doubleClickTime;
    [SerializeField]
    protected float dcCoolTime;


    // Update is called once per frame
    void Update()
    {
        doubleClickTime += Time.deltaTime;
        if (doubleClickTime > 1f)
        {
            doubleClickTime = 1f;
        }
    }

    public void OnDrag(Slot slot, PointerEventData eventData)
    {
        if (slot.GetItemIndex() != -1)
        {
            movingItem.Moving(eventData.position);
        }
    }

    public void OnPointerClick(Slot slot, PointerEventData eventData)
    {
        //Debug.Log(slot.gameObject.name + "-OnPointerClick");

        if (doubleClickTime < dcCoolTime)
        {
            Debug.Log("doubleclickkkkk");
            clickUpSlot.itemUse();
        }
        else
        {
            doubleClickTime = 0;
        }


    }


    public void OnPointerUp(Slot slot, PointerEventData eventData)
    {
        stayClickItem = false;
        //Debug.Log(slot.gameObject.name + "-OnPointerUp");
        //Debug.Log("gO: " + eventData.pointerCurrentRaycast.gameObject.name);
        GameObject tempO = eventData.pointerCurrentRaycast.gameObject;
        if (tempO != null)
        {
            clickUpSlot = tempO.GetComponent<Slot>();
        }


        if (eventData.pointerCurrentRaycast.gameObject != null && clickDownSlot != null && clickUpSlot != null && clickDownSlot != clickUpSlot)
        {
            //슬롯 비어있음
            if (clickUpSlot.GetItemIndex() == -1)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                    clickDownSlot.SlotInit();//슬롯초기화
                    clickUpSlot.ShowAll();
                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    calTemp2 = Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
                    calTemp1 = clickDownSlot.GetItemCount() - Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
                    if (calTemp2 < 1)
                    {
                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                        clickDownSlot.SlotInit();//슬롯초기화
                    }
                    else
                    {
                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), calTemp2, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                        clickDownSlot.SetItemCount(calTemp1);
                        clickDownSlot.SetViewCount(calTemp1.ToString());
                    }
                    clickUpSlot.ShowAll();
                }
            }
            else// 슬롯 아이템 존재함
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    if (clickDownSlot != clickUpSlot)
                    {
                        if (clickDownSlot.GetItemIndex() == clickUpSlot.GetItemIndex())
                        {
                            calTemp1 = clickUpSlot.GetItemCount() + clickDownSlot.GetItemCount();
                            clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), calTemp1, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                            clickDownSlot.SlotInit();
                            clickUpSlot.ShowAll();
                        }
                        else if (clickDownSlot.GetItemIndex() != clickUpSlot.GetItemIndex())
                        {
                            tempSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                            clickDownSlot.SetSlot(clickUpSlot.GetItemIndex(), clickUpSlot.GetItemCount(), clickUpSlot.GetViewImage(), clickDownSlot.GetItemName());
                            clickUpSlot.SetSlot(tempSlot.GetItemIndex(), tempSlot.GetItemCount(), tempSlot.GetViewImage(), clickDownSlot.GetItemName());
                            tempSlot.SlotInit();
                        }
                    }

                }
                else if (eventData.button == PointerEventData.InputButton.Right)
                {
                    calTemp2 = Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
                    calTemp1 = clickDownSlot.GetItemCount() - Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);

                    if (calTemp2 < 1)
                    {
                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount() + clickUpSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                        clickDownSlot.SlotInit();//슬롯초기화
                    }
                    else
                    {
                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickUpSlot.GetItemCount() + calTemp2, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                        clickDownSlot.SetItemCount(calTemp1);
                        clickDownSlot.SetViewCount(calTemp1.ToString());
                    }

                    clickUpSlot.ShowAll();
                }
            }
            

        }
        else//UI밖 버림
        {

        }

        if (!stayClickItem)
        {
            movingItem.Hide();
        }


    }

    public void OnPointerDown(Slot slot, PointerEventData eventData)
    {
        clickDownSlot = null;
        //movingItem.isActive: true- 들고있는게 있음
        if (!movingItem.isActive)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (slot.GetItemIndex() != -1)
                {
                    List<Slot> ls = null;
                    if (slot.windowIndex != 1)
                    {
                        if (_crafte.gameObject.activeSelf)
                        {
                            ls = _crafte.GetSlotList();
                        }
                    }
                    else if (slot.windowIndex == 1)
                    {
                        ls = _inventory.GetSlotList();
                    }
                    if (ls != null)
                    {
                        for (int i = 0; i < ls.Count; i++)
                        {
                            if (ls[i].GetItemIndex() == -1)
                            {
                                ls[i].SetSlot(slot.GetItemIndex(), slot.GetItemCount(), slot.GetViewImage(), slot.GetItemName());
                                ls[i].isWeapon = slot.isWeapon;
                                ls[i].isPossibleUse = slot.isPossibleUse;
                                slot.SlotInit();
                                break;
                            }
                        }
                    }
                }
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                if (slot.GetItemIndex() != -1)
                {
                    List<Slot> ls = null;
                    if (slot.windowIndex != 2)
                    {
                        ls = _quickWindow.GetSlotList();
                    }
                    else if (slot.windowIndex == 2)
                    {
                        if (_inventory.gameObject.activeSelf)
                        {
                            ls = _inventory.GetSlotList();
                        }
                    }
                    
                    if (ls != null)
                    {
                        for (int i = 0; i < ls.Count; i++)
                        {
                            if (ls[i].GetItemIndex() == -1)
                            {
                                ls[i].SetSlot(slot.GetItemIndex(), slot.GetItemCount(), slot.GetViewImage(), slot.GetItemName());
                                ls[i].isWeapon = slot.isWeapon;
                                ls[i].isPossibleUse = slot.isPossibleUse;
                                slot.SlotInit();
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                //Debug.Log(slot.gameObject.name + "-OnPointerDown");
                if (slot.GetItemIndex() != -1)
                {
                    clickDownSlot = slot;
                    movingItem.SetImage(slot.GetViewImage());
                    movingItem.Show(eventData.position);
                }
            }
        }
    }

    protected void getItem(ObjectInfo oi)
    {
        LastSlot = null;
        foreach (Slot s in slotList)
        {
            if (s.GetItemIndex() == oi.itemIndex && (!oi.isWeapon))
            {
                LastSlot = s;
                break;
            }
            else if (s.GetItemIndex() == -1 && LastSlot == null)
            {
                LastSlot = s;
                continue;
            }
        }

        LastSlot.SetItemCount(LastSlot.GetItemCount() + oi.count);
        LastSlot.SetViewCount(LastSlot.GetItemCount().ToString());
        if (LastSlot.GetItemIndex() == -1)
        {
            LastSlot.SetItemIndex(oi.itemIndex);
            LastSlot.SetItemName(oi.objectName);
            LastSlot.SetViewImage(oi.itemImage);
        }
        LastSlot.isWeapon = oi.isWeapon;
        LastSlot.isPossibleUse = oi.IsPossibleUse;
    }

    public bool GetItemsName(List<ResultObject> objectNames)
    {
        addPossibleSlot = false;
        List<ObjectInfo> oiList = new List<ObjectInfo>();

        for (int i = 0; i < objectNames.Count; i++)
        {
            oiList.Add(ItemListManager.GetInstance().FindItem(objectNames[i].objectName).GetComponent<ObjectInfo>());
        }

        int minNeedNewSlotCount = 0;
        int needNewSlotCount = 0; // minNeedNewSlotCount < x <= objectNames.Count
        int addPossibleCount = 0; // <= objectNames.Count;

        for (int i = 0; i < oiList.Count; i++)
        {
            if (oiList[i].isWeapon)
            {
                for (int j = 0; j < slotList.Count; j++)
                {
                    if (slotList[j].itemIndex == -1)
                    {
                        minNeedNewSlotCount++;
                        needNewSlotCount++;
                        addPossibleCount++;

                        break;
                    }
                }
            }else
            {
                for (int j = 0; j < slotList.Count; j++)
                {
                    if (slotList[i].isWeapon) continue;
                    else
                    {
                        if (slotList[j].itemIndex == oiList[i].itemIndex)
                        {
                            addPossibleCount++;
                            break;
                        }
                        else if (slotList[j].itemIndex == -1)
                        {
                            needNewSlotCount++;
                            if (minNeedNewSlotCount < needNewSlotCount)
                            {
                                addPossibleCount++;
                                minNeedNewSlotCount++;

                                if (addPossibleCount == objectNames.Count) break;
                            }
                        }
                    }
                }
            }

            if (addPossibleCount == objectNames.Count) break;
        }
        

        if (addPossibleCount == objectNames.Count)
        {
            for (int i = 0; i < oiList.Count; i++)
            {
                for (int j = 0; j < objectNames[i].count; j++)
                {
                    getItem(oiList[i]);
                }
            }

            return true;
        }else
        {
            return false;
        }
    }
    public List<Slot> GetSlotList()
    {
        return slotList;
    }
}
