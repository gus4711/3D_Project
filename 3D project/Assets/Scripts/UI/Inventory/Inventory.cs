using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class Inventory : SlotWindowBase
{

    //managers
    EventManager _eventManager;

    void Start()
    {
        //managers
        _eventManager = EventManager.Instance;

        _quickSlots = GameObject.FindGameObjectsWithTag("QuickSlot");


        slotList = new List<Slot>();
        tempSlot = new Slot();
        InventoryData inventoryData = LoadInventoryData();
        for (int i = 0; i < 10; i++)
        {
            slotObjectTemp = GameObject.Instantiate<GameObject>(slotObject, slotParent);
            slotObjectTemp.name = "slot" + i;

            slotScriptTemp = slotObjectTemp.GetComponent<Slot>();
            slotScriptTemp.windowIndex = 0;
            slotScriptTemp.SetSlotIndex(i);
            slotList.Add(slotScriptTemp);

            slotScriptTemp.OnMUp += OnPointerUp;
            slotScriptTemp.OnMDown += OnPointerDown;
            slotScriptTemp.OnMDrag += OnDrag;
            slotScriptTemp.OnMClick += OnPointerClick;
        }

        ObjectInfo oi = null;

        if (inventoryData != null && inventoryData.slotIndex.Count != 0)
        {
            for (int i = 0; i < inventoryData.slotIndex.Count; i++)
            {
                oi = ItemListManager.GetInstance().FindItem(inventoryData.itemName[i]).GetComponent<ObjectInfo>();
                slotList[inventoryData.slotIndex[i]].SetSlot(oi.itemIndex, inventoryData.itemCount[i], oi.itemImage, inventoryData.itemName[i]);
                slotList[inventoryData.slotIndex[i]].isWeapon = oi.isWeapon;
            }
        }

        //_eventManager.OnGetItemEvent += getItem;
        _eventManager.OnInteractionEvent += getItem;
        user.GetItemsNameEvent += GetItemsName;
        user.QuestItemUpdateEvent += QuestItemUpdate;
        user.QuestItemCheck += QuestItemCheck;

        user.RemoveQuestItem += RemoveQuestItem;

        gameObject.SetActive(false);
    }

    void QuestItemUpdate(Player player, ObjectInfo oi, bool allCheck)
    {
        int itemCount = 0;

        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].GetItemName() == oi.objectName)
            {
                itemCount += slotList[i].GetItemCount();
            }
        }

        int objectIndex;
        if (allCheck)
        {

        }else
        {
            for (int i = 0; i < player.collectionQuestList.Count; i++)
            {
                objectIndex = player.collectionQuestList[i].targetObject.IndexOf(oi.objectName);
                if (objectIndex > -1)
                {
                    player.collectionQuestList[i].nowObject[objectIndex] = itemCount;
                }
            }
        }
    }

    void QuestItemCheck(Player player, CollectionQuest quest)
    {
        for (int i = 0; i < quest.targetObject.Count; i++)
        {
            for (int j = 0; j < slotList.Count; j++)
            {
                if (slotList[j].itemName == quest.targetObject[i])
                {
                    quest.nowObject[i] += slotList[j].itemCount;
                }
            }
        }
    }

    void RemoveQuestItem(JsonQuest quest)
    {
        List<int> tempCount = new List<int>();
        for (int i = 0; i < quest.targetCount.Count; i++)
        {
            tempCount.Add(quest.targetCount[i]);
        }
        int resultValue;
        for (int i = 0; i < slotList.Count; i++)
        {
            for (int j = 0; j < quest.targetObject.Count; j++)
            {
                if (slotList[i].itemName == quest.targetObject[j])
                {
                    resultValue = tempCount[j] - slotList[i].itemCount;
                    if (resultValue >= 0)
                    {
                        tempCount[j] = resultValue;
                        slotList[i].SlotInit();
                    }
                    else
                    {
                        slotList[i].itemCount -= tempCount[j];
                        tempCount.RemoveAt(j);
                    }
                }
            }
        }
    }

    public InventoryData LoadInventoryData()
    {
        System.IO.DirectoryInfo diCheck = new System.IO.DirectoryInfo("SAVE");
        if (diCheck.Exists)
        {
            System.IO.FileStream fileStream;
            try
            {
                fileStream = new System.IO.FileStream("SAVE/saveData.json", System.IO.FileMode.Open);
            }catch(System.IO.FileNotFoundException error)
            {
                return null;
            }

            if (fileStream != null)
            {
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                fileStream.Close();

                string jsonData = System.Text.Encoding.UTF8.GetString(data);
                try
                {
                    JObject jo = JObject.Parse(jsonData);
                    return JsonConvert.DeserializeObject<InventoryData>(jo["inventory"].ToString());
                }
                catch(System.Exception e)
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
