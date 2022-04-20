using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafteManager:Singleton<CrafteManager>
{

    //material.keys == crafteResult.keys
    List<string[]> _materialsList = new List<string[]>();
    List<string[]> _crafteResultList = new List<string[]>();


    // key = 첫 재료, value = 해당 재료 리스트
    Dictionary<string, List<int>> _crafteBook = new Dictionary<string, List<int>>();

    //인덱스 검색
    

    public void Awake()
    {
        addCrateList(new string[] { "Firewood", "Firewood", "Firewood" }, new string[] { "WoodAxe" });
        addCrateList(new string[] { "Firewood", "Firewood", "Firewood", "Firewood" }, new string[] { "PickAxe" });
        addCrateList(new string[] { "Wheat", "Wheat", "Wheat" }, new string[] { "Bread" });
        addCrateList(new string[] { "Firewood", "Firewood", "Firewood", "Firewood", "Firewood" }, new string[] { "WoodSword" });
        addCrateList(new string[] { "Firewood", "Firewood", "Stone", "Stone", "Stone" }, new string[] { "StoneSword" });
    }

    //재료 인덱스로 추가
    void addCrateList(string[] materials, string[] crafteResult)
    {
        string lowIndex = getLowIndex(materials);

        _materialsList.Add(materials);
        _crafteResultList.Add(crafteResult);

        if (_crafteBook.ContainsKey(lowIndex))
        {
            List<int> list = _crafteBook[lowIndex];
            list.Add(_materialsList.Count - 1);
        }
        else
        {
            _crafteBook.Add(lowIndex, new List<int>());
            List<int> list = _crafteBook[lowIndex];
            list.Add(_materialsList.Count - 1);
        }
    }

    public string[] GetCrafte(string[] materials)
    {
        string lowIndex = getLowIndex(materials);
        List<int> craftelist;
        string[] arr = null;

        int returnResultIndex = -1;

        if (_crafteBook.ContainsKey(lowIndex))
        {
            craftelist = _crafteBook[lowIndex];

            foreach (int i in craftelist)
            {
                arr = _materialsList[i];
                returnResultIndex = i;

                if (materials.Length == arr.Length)//배열크기 체크
                {
                    for (int j = 0; j < materials.Length; j++)
                    {
                        if (!(materials[j] == arr[j]))
                        {
                            returnResultIndex = -1;
                            break;
                        }
                    }
                }
                else
                {
                    returnResultIndex = -1;
                    continue;
                }

                if (returnResultIndex != -1)
                {
                    break;
                }
            }

            if (returnResultIndex == -1)
            {
                return null;
            }
            else
            {
                return _crafteResultList[returnResultIndex];
            }

        }
        else
        {
            return null;
        }
    }

    ////재료 한글로 추가- 보류
    //void addCrateList(string[] materials, string[] crafteResult)
    //{

    //}

    string getLowIndex(string[] arr)
    {

        System.Array.Sort(arr);

        return arr[0];
    }
    void SetList(string sJson)
    {
        char[] sp = ", \"crafteResultList".ToCharArray();
        string[] temp = sJson.Split(sp);
    }
}
