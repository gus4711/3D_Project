using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Text;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class SaveData
{
    public InventoryData inventory = new InventoryData();
    public QuickSlotData quickSlotWindow = new QuickSlotData();
}
[System.Serializable]
public class InventoryData
{
    public List<int> slotIndex = new List<int>();
    public List<string> itemName = new List<string>();
    public List<int> itemCount = new List<int>();
}
public class QuickSlotData
{
    public List<int> slotIndex = new List<int>();
    public List<string> itemName = new List<string>();
    public List<int> itemCount = new List<int>();
}
public class PlayerData
{
    public List<int> position = new List<int>();
    public List<string> direction = new List<string>();
}

public class ESCMenu : MonoBehaviour
{

    [SerializeField]
    GameObject _Loading;
    [SerializeField]
    Inventory _inventory;
    [SerializeField]
    QuickSlotWindow _quickSlowWindow;
    [SerializeField]
    Player _player;

    private void Awake()
    {
        ItemListManager.CreateInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SaveData()
    {
        _Loading.SetActive(true);
        string path = "SAVE/saveData.json";
        string tempJson;



        SaveData sd = new SaveData();

        List<Slot> ls = _inventory.GetSlotList();

        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].GetItemIndex() != -1)
            {
                sd.inventory.slotIndex.Add(ls[i].GetSlotIndex());
                sd.inventory.itemName.Add(ls[i].GetItemName());
                sd.inventory.itemCount.Add(ls[i].GetItemCount());
            }
        }

        ls = _quickSlowWindow.GetSlotList();

        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].GetItemIndex() != -1)
            {
                sd.quickSlotWindow.slotIndex.Add(ls[i].GetSlotIndex());
                sd.quickSlotWindow.itemName.Add(ls[i].GetItemName());
                sd.quickSlotWindow.itemCount.Add(ls[i].GetItemCount());
            }
        }


        Directory.CreateDirectory(Path.GetDirectoryName(path));

        FileStream fileStream = new FileStream(path, FileMode.Create);
        tempJson = JsonConvert.SerializeObject(sd);
        byte[] data = Encoding.UTF8.GetBytes(tempJson);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();

        _Loading.SetActive(false);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit(); // 어플리케이션 종료
        #endif
    }
}
