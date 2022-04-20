using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory1 : MonoBehaviour
{
    //public Player user;
    //public GameObject slotObject;
    //public Transform slotParent;
    //public MovingItem movingItem;


    

    //GameObject slotObjectTemp;
    //Slot slotScriptTemp;



    ////슬롯 추가
    //List<Slot> slotList;

    

    ////슬롯 검색
    //Slot LastSlot;
    //GameObject[] _quickSlots;


    ////슬롯 클릭
    //Slot clickDownSlot;
    //Slot clickUpSlot;
    //bool stayClickItem;
    //int calTemp1, calTemp2;

    //Slot tempSlot;

    ////더블클릭
    //float doubleClickTime;
    //[SerializeField]
    //float dcCoolTime;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    _quickSlots = GameObject.FindGameObjectsWithTag("QuickSlot");




    //    slotList = new List<Slot>();
    //    tempSlot = new Slot();

    //    for (int i = 0; i < 10; i++)
    //    {
    //        slotObjectTemp = GameObject.Instantiate<GameObject>(slotObject, slotParent);
    //        slotObjectTemp.name = "slot" + i;
    //        slotScriptTemp = slotObjectTemp.GetComponent<Slot>();
    //        slotScriptTemp.SetSlotIndex(i);
    //        slotList.Add(slotScriptTemp);

    //        slotScriptTemp.OnMUp += OnPointerUp;
    //        slotScriptTemp.OnMDown += OnPointerDown;
    //        slotScriptTemp.OnMDrag += OnDrag;
    //        slotScriptTemp.OnMClick += OnPointerClick;
    //    }
    //    user.GetItemEvent += getItem;

    //    for (int i = 0; i < _quickSlots.Length; i++)
    //    {
    //        _quickSlots[i].GetComponent<QuickSlot>().OnUseItemEvent += UseQuickSlot;
    //    }

    //    gameObject.SetActive(false);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    doubleClickTime += Time.deltaTime;
    //    if (doubleClickTime > 1f)
    //    {
    //        doubleClickTime = 1f;
    //    }
    //}

    //public void OnDrag(Slot slot, PointerEventData eventData)
    //{
    //    if (slot.GetItemIndex() != -1)
    //    {
    //        movingItem.Moving(eventData.position);
    //    }
    //}

    //public void OnPointerClick(Slot slot, PointerEventData eventData)
    //{
    //    //Debug.Log(slot.gameObject.name + "-OnPointerClick");

    //    if(doubleClickTime < dcCoolTime)
    //    {
    //        Debug.Log("doubleclickkkkk");
    //        clickUpSlot.itemUse();
    //    }
    //    else
    //    {
    //        doubleClickTime = 0;
    //    }
        
        
    //}


    //public void OnPointerUp(Slot slot, PointerEventData eventData)
    //{
    //    stayClickItem = false;
    //    //Debug.Log(slot.gameObject.name + "-OnPointerUp");
    //    //Debug.Log("gO: " + eventData.pointerCurrentRaycast.gameObject.name);
    //    GameObject tempO = eventData.pointerCurrentRaycast.gameObject;
    //    if (tempO != null)
    //    {
    //        clickUpSlot = tempO.GetComponent<Slot>();
    //    }
        

    //    if (eventData.pointerCurrentRaycast.gameObject != null && clickDownSlot != null && clickUpSlot != null)
    //    {
            
    //        if (clickUpSlot.GetSlotIndex() == 0)//인벤토리 슬롯
    //        {
    //            //슬롯 비어있음
    //            if (clickUpSlot.GetItemIndex() == -1)
    //            {
    //                if(eventData.button == PointerEventData.InputButton.Left)
    //                {
    //                    clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                    clickDownSlot.SlotInit();//슬롯초기화
    //                    clickUpSlot.ShowAll();
    //                }
    //                else if(eventData.button == PointerEventData.InputButton.Right)
    //                {
    //                    calTemp2 = Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
    //                    calTemp1 = clickDownSlot.GetItemCount() - Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
    //                    if (calTemp2 < 1)
    //                    {
    //                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                        clickDownSlot.SlotInit();//슬롯초기화
    //                    }
    //                    else
    //                    {
    //                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), calTemp2, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                        clickDownSlot.SetItemCount(calTemp1);
    //                        clickDownSlot.SetViewCount(calTemp1.ToString());
    //                    }
    //                    clickUpSlot.ShowAll();
    //                }
    //            }
    //            else// 슬롯 아이템 존재함
    //            {
    //                if (eventData.button == PointerEventData.InputButton.Left)
    //                {
    //                    if(clickDownSlot != clickUpSlot)
    //                    {
    //                        if( clickDownSlot.GetItemIndex() == clickUpSlot.GetItemIndex())
    //                        {
    //                            calTemp1 = clickUpSlot.GetItemCount() + clickDownSlot.GetItemCount();
    //                            clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), calTemp1, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                            clickDownSlot.SlotInit();
    //                            clickUpSlot.ShowAll();
    //                        }else if(clickDownSlot.GetItemIndex() != clickUpSlot.GetItemIndex())
    //                        {
    //                            tempSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                            clickDownSlot.SetSlot(clickUpSlot.GetItemIndex(), clickUpSlot.GetItemCount(), clickUpSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                            clickUpSlot.SetSlot(tempSlot.GetItemIndex(), tempSlot.GetItemCount(), tempSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                            tempSlot.SlotInit();
    //                        }
    //                    }
                        
    //                }
    //                else if (eventData.button == PointerEventData.InputButton.Right)
    //                {
    //                    calTemp2 = Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
    //                    calTemp1 = clickDownSlot.GetItemCount() - Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);

    //                    if (calTemp2 < 1)
    //                    {
    //                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount()+ clickUpSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                        clickDownSlot.SlotInit();//슬롯초기화
    //                    }
    //                    else
    //                    {
    //                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickUpSlot.GetItemCount() + calTemp2, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                        clickDownSlot.SetItemCount(calTemp1);
    //                        clickDownSlot.SetViewCount(calTemp1.ToString());
    //                    }

    //                    clickUpSlot.ShowAll();
    //                }
    //            }
    //        }else if(clickUpSlot.GetSlotIndex() == 1)//퀵 슬롯
    //        {
    //            clickUpSlot.SetViewImage(clickDownSlot.GetViewImage());

    //            clickUpSlot.gameObject.GetComponent<QuickSlot>().targetSlot = slot;

    //        }
    //        else if (clickUpSlot.GetSlotIndex() == 2)//제작 슬롯
    //        {
    //            //슬롯 비어있음
    //            if (clickUpSlot.GetItemIndex() == -1)
    //            {
    //                if (eventData.button == PointerEventData.InputButton.Left)
    //                {
    //                    clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                    clickDownSlot.SlotInit();//슬롯초기화
    //                    clickUpSlot.ShowAll();
    //                }
    //                else if (eventData.button == PointerEventData.InputButton.Right)
    //                {
    //                    calTemp2 = Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
    //                    calTemp1 = clickDownSlot.GetItemCount() - Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
    //                    if (calTemp2 < 1)
    //                    {
    //                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                        clickDownSlot.SlotInit();//슬롯초기화
    //                    }
    //                    else
    //                    {
    //                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), calTemp2, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                        clickDownSlot.SetItemCount(calTemp1);
    //                        clickDownSlot.SetViewCount(calTemp1.ToString());
    //                    }
    //                    clickUpSlot.ShowAll();
    //                }
    //            }
    //            else// 슬롯 아이템 존재함
    //            {
    //                if (eventData.button == PointerEventData.InputButton.Left)
    //                {
    //                    calTemp1 = clickUpSlot.GetItemCount() + clickDownSlot.GetItemCount();
    //                    clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), calTemp1, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                    clickDownSlot.SlotInit();//슬롯초기화
    //                    clickUpSlot.ShowAll();
    //                }
    //                else if (eventData.button == PointerEventData.InputButton.Right)
    //                {
    //                    calTemp2 = Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
    //                    calTemp1 = clickDownSlot.GetItemCount() - Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);

    //                    if (calTemp2 < 1)
    //                    {
    //                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount() + clickUpSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                        clickDownSlot.SlotInit();//슬롯초기화
    //                    }
    //                    else
    //                    {
    //                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickUpSlot.GetItemCount() + calTemp2, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
    //                        clickDownSlot.SetItemCount(calTemp1);
    //                        clickDownSlot.SetViewCount(calTemp1.ToString());
    //                    }

    //                    clickUpSlot.ShowAll();
    //                }
    //            }
    //        }

    //    }
    //    else//UI밖 버림
    //    {
            
    //    }

    //    if (!stayClickItem)
    //    {
    //        movingItem.Hide();
    //    }
        

    //}

    //public void OnPointerDown(Slot slot, PointerEventData eventData)
    //{
    //    //들고있는게 있음
    //    if(!movingItem.isActive)
    //    {
    //        clickDownSlot = null;
    //        //Debug.Log(slot.gameObject.name + "-OnPointerDown");
    //        if (slot.GetItemIndex() != -1)
    //        {
    //            clickDownSlot = slot;
    //            movingItem.SetImage(slot.GetViewImage());
    //            movingItem.Show(eventData.position);
    //        }
    //    }
    //}

    //void getItem(ObjectInfo oi)
    //{
    //    LastSlot = null;
    //    foreach(Slot s in slotList)
    //    {
    //        if(s.GetItemIndex() == oi.itemIndex)
    //        {
    //            LastSlot = s;
    //            break;
    //        }
    //        else if(s.GetItemIndex() == -1 && LastSlot == null)
    //        {
    //            LastSlot = s;
    //            continue;
    //        }
    //    }

    //    LastSlot.SetItemCount(LastSlot.GetItemCount() + oi.count);
    //    LastSlot.SetViewCount(LastSlot.GetItemCount().ToString());
    //    if (LastSlot.GetItemIndex() == -1)
    //    {
    //        LastSlot.SetItemIndex(oi.itemIndex);
    //        LastSlot.SetItemName(oi.objectName);
    //        LastSlot.SetViewImage(oi.itemImage);

    //    }
    //}

    //void UseQuickSlot(QuickSlot quickSlot)
    //{
    //    if(quickSlot.targetSlot != null)
    //    {
    //        if (ItemListManager.GetInstance().FindItem(quickSlot.targetSlot.itemIndex).GetComponent<ObjectInfo>().isWeapon)
    //        {
    //            user.addEquipItem(quickSlot.targetSlot);
    //        }
    //        else
    //        {
    //            user.removeEquipItem();
    //        }
    //    }
    //    else
    //    {
    //        user.removeEquipItem();
    //    }

    //}

    //public List<Slot> GetInventorySlot()
    //{
    //    return slotList;
    //}
}
