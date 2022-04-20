using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
public class QuickSlotWindow : SlotWindowBase
{
    private void Start()
    {
        var ilm = ItemListManager.GetInstance();
        QuickSlotData quickSlotData = LoadQuickSlotData();

        for (int i = 0; i < slotList.Count; i++)
        {
            slotList[i].OnMUp += OnPointerUp;
            slotList[i].OnMDown += OnPointerDown;
            slotList[i].OnMDrag += OnDrag;
            slotList[i].OnMClick += OnPointerClick;
        }

        ObjectInfo oi = null;
        if (quickSlotData != null && quickSlotData.slotIndex.Count != 0)
        {
            for (int i = 0; i < quickSlotData.slotIndex.Count; i++)
            {
                oi = ilm.FindItem(quickSlotData.itemName[i]).GetComponent<ObjectInfo>();
                slotList[quickSlotData.slotIndex[i]].SetSlot(oi.itemIndex, quickSlotData.itemCount[i], oi.itemImage, quickSlotData.itemName[i]);
                slotList[quickSlotData.slotIndex[i]].isWeapon = oi.isWeapon;
                slotList[quickSlotData.slotIndex[i]].isPossibleUse = oi.IsPossibleUse;
            }
        }
    }


    private void Update()
    {
        InputKey();
    }

    void InputKey()
    {
       //인벤토리가 켜져있으면 실행 X
       if (!UIManager.GetInstance().isWindowOn)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UseQuickSlot(slotList[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                UseQuickSlot(slotList[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                UseQuickSlot(slotList[2]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                UseQuickSlot(slotList[3]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                UseQuickSlot(slotList[4]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                UseQuickSlot(slotList[5]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                UseQuickSlot(slotList[6]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                UseQuickSlot(slotList[7]);
            }
        }
    }


    protected void UseQuickSlot(Slot quickSlot)
    {
        ObjectInfo item = null;
        var itemListManager = ItemListManager.GetInstance();

        if (itemListManager.FindItem(quickSlot.itemName) != null)
        {
            item = itemListManager.FindItem(quickSlot.itemName).GetComponent<ObjectInfo>();
        }
        if (item != null && quickSlot.GetItemIndex() != -1)
        {
            if (item.isWeapon || item.IsPossibleUse)
            {
                user.AddEquipItem(quickSlot);
            }
            else
            {
                user.RemoveEquipItem();
            }
        }
        else
        {
            user.RemoveEquipItem();
        }
    }

    public QuickSlotData LoadQuickSlotData()
    {
        System.IO.DirectoryInfo diCheck = new System.IO.DirectoryInfo("SAVE");
        if (diCheck.Exists)
        {
            System.IO.FileStream fileStream;
            try
            {
                fileStream = new System.IO.FileStream("SAVE/saveData.json", System.IO.FileMode.Open);
            }
            catch (System.IO.FileNotFoundException error)
            {
                return null;
            }

            if (fileStream != null)
            {
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                fileStream.Close();

                string jsonData = System.Text.Encoding.UTF8.GetString(data);
                try{
                    JObject jo = JObject.Parse(jsonData);
                    return JsonConvert.DeserializeObject<QuickSlotData>(jo["quickSlotWindow"].ToString());
                }
                catch (System.Exception e)
                {
                    return null;
                }
                
            }
            else
            {
                return null;
            }
        }
        return null;
    }
}
