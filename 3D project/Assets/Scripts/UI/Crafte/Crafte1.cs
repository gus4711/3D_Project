using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Crafte1 : MonoBehaviour
{
    public GameObject inventory;

    [SerializeField]
    MakingSlot[] slotList;
    [SerializeField]
    MovingItem movingItem;
    [SerializeField]
    MakingSlot ResultSlot;


    //ΩΩ∑‘ ≈¨∏Ø
    Slot clickDownSlot;
    Slot clickUpSlot;
    bool stayClickItem;
    int calTemp1, calTemp2;

    private void Awake()
    {
        for (int i = 0; i < slotList.Length; i++)
        {
            slotList[i].OnMUp += OnPointerUp;
            slotList[i].OnMDown += OnPointerDown;
            slotList[i].OnMDrag += OnDrag;
            slotList[i].OnMClick += OnPointerClick;
        }

        ResultSlot.OnMUp += OnPointerUp;
        ResultSlot.OnMDown += OnPointerDown;
        ResultSlot.OnMDrag += OnDrag;
        ResultSlot.OnMClick += OnPointerClick;

        gameObject.SetActive(false);
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
        Debug.Log(slot.gameObject.name + "-OnPointerClick");

    }
    public void OnPointerUp(Slot slot, PointerEventData eventData)
    {
        Debug.Log(slot.gameObject.name + "-OnPointerUp");
        movingItem.Hide();

        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            clickUpSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>();

            if (clickUpSlot.GetSlotIndex() == 0)//¿Œ∫•≈‰∏Æ ΩΩ∑‘
            {
                //ΩΩ∑‘ ∫ÒæÓ¿÷¿Ω
                if (clickUpSlot.GetItemIndex() == -1)
                {
                    if (eventData.button == PointerEventData.InputButton.Left)
                    {
                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                        clickDownSlot.SlotInit();//ΩΩ∑‘√ ±‚»≠
                        clickUpSlot.ShowAll();
                    }
                    else if (eventData.button == PointerEventData.InputButton.Right)
                    {
                        calTemp2 = Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
                        calTemp1 = clickDownSlot.GetItemCount() - Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
                        if (calTemp2 < 1)
                        {
                            clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                            clickDownSlot.SlotInit();//ΩΩ∑‘√ ±‚»≠
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
                else// ΩΩ∑‘ æ∆¿Ã≈€ ¡∏¿Á«‘
                {
                    if (eventData.button == PointerEventData.InputButton.Left)
                    {
                        calTemp1 = clickUpSlot.GetItemCount() + clickDownSlot.GetItemCount();
                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), calTemp1, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                        clickDownSlot.SlotInit();//ΩΩ∑‘√ ±‚»≠
                        clickUpSlot.ShowAll();
                    }
                    else if (eventData.button == PointerEventData.InputButton.Right)
                    {
                        calTemp2 = Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
                        calTemp1 = clickDownSlot.GetItemCount() - Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);

                        if (calTemp2 < 1)
                        {
                            clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount() + clickUpSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                            clickDownSlot.SlotInit();//ΩΩ∑‘√ ±‚»≠
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
            else if (clickUpSlot.GetSlotIndex() == 1)//ƒ¸ ΩΩ∑‘
            {
                clickUpSlot.SetViewImage(clickDownSlot.GetViewImage());

            }
            else if (clickUpSlot.GetSlotIndex() == 2)//¡¶¿€ ΩΩ∑‘
            {
                //ΩΩ∑‘ ∫ÒæÓ¿÷¿Ω
                if (clickUpSlot.GetItemIndex() == -1)
                {
                    if (eventData.button == PointerEventData.InputButton.Left)
                    {
                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                        clickDownSlot.SlotInit();//ΩΩ∑‘√ ±‚»≠
                        clickUpSlot.ShowAll();
                    }
                    else if (eventData.button == PointerEventData.InputButton.Right)
                    {
                        calTemp2 = Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
                        calTemp1 = clickDownSlot.GetItemCount() - Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
                        if (calTemp2 < 1)
                        {
                            clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                            clickDownSlot.SlotInit();//ΩΩ∑‘√ ±‚»≠
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
                else// ΩΩ∑‘ æ∆¿Ã≈€ ¡∏¿Á«‘
                {
                    if (eventData.button == PointerEventData.InputButton.Left)
                    {
                        calTemp1 = clickUpSlot.GetItemCount() + clickDownSlot.GetItemCount();
                        clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), calTemp1, clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                        clickDownSlot.SlotInit();//ΩΩ∑‘√ ±‚»≠
                        clickUpSlot.ShowAll();
                    }
                    else if (eventData.button == PointerEventData.InputButton.Right)
                    {
                        calTemp2 = Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);
                        calTemp1 = clickDownSlot.GetItemCount() - Mathf.FloorToInt((float)clickDownSlot.GetItemCount() / 2f);

                        if (calTemp2 < 1)
                        {
                            clickUpSlot.SetSlot(clickDownSlot.GetItemIndex(), clickDownSlot.GetItemCount() + clickUpSlot.GetItemCount(), clickDownSlot.GetViewImage(), clickDownSlot.GetItemName());
                            clickDownSlot.SlotInit();//ΩΩ∑‘√ ±‚»≠
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


        }
    }

    public void OnPointerDown(Slot slot, PointerEventData eventData)
    {
        clickDownSlot = null;
        Debug.Log(slot.gameObject.name + "-OnPointerDown");
        if (slot.GetItemIndex() != -1)
        {
            clickDownSlot = slot;
            movingItem.SetImage(slot.GetViewImage());
            movingItem.Show(eventData.position);
        }
    }


    public void Make()
    {
        List<string> materials = new List<string>();
        foreach(Slot s in slotList)
        {
            if(s.GetItemName() != "")
            {
                materials.Add(s.GetItemName());
            }
        }

        string[] result = CrafteManager.GetInstance().GetCrafte(materials.ToArray());

        if(result != null)
        {
            foreach (Slot s in slotList)
            {
                if (s.GetItemIndex() != -1)
                {
                    if(s.GetItemCount() == 1)
                    {
                        s.SlotInit();
                    }
                    else
                    {
                        s.SetItemCount(s.GetItemCount() - 1);
                        s.SetViewCount(s.GetItemCount().ToString());
                    }
                }
            }


            ObjectInfo resultObject = ItemListManager.GetInstance().FindItem(result[0]).GetComponent<ObjectInfo>();

            ResultSlot.SetSlot(resultObject.itemIndex, 1, resultObject.itemImage, resultObject.objectName);
            ResultSlot.ShowAll();
        }

        
    }



}
