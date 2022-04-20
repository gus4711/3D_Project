using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Crafte : SlotWindowBase
{
    public GameObject inventory;

    [SerializeField]
    MakingSlot ResultSlot;



    private void Awake()
    {
        for (int i = 0; i < slotList.Count; i++)
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

    private void Start()
    {
        
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
            ResultSlot.isWeapon = resultObject.isWeapon;
            ResultSlot.isPossibleUse = resultObject.IsPossibleUse;
            ResultSlot.ShowAll();
        }

        
    }



}
