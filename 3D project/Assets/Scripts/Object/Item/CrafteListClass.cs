using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CrafteListClass
{
    [SerializeField]
    public List<SubClass> materialsList;
    [SerializeField]
    public List<SubClass> crafteResultList;
}

[System.Serializable]
public class SubClass
{
    [SerializeField]
    public string[] list;
}