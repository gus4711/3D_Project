using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance = null;

    public static T Instance
    {
        get { 
            if (_instance == null)
            {
                CreateInstance();
            }
            return _instance; }
    }
    public static T GetInstance()
    {
        return Instance;
    }

    public static void Release()
    {
        _instance = null;
    }

    public static void CreateInstance()
    {
        GameObject obj;

        obj = GameObject.Find("@Manager");

        if (obj.GetComponent<T>() == null)
        {
            _instance = obj.AddComponent<T>();
        }else
        {
            _instance = obj.GetComponent<T>();
        }
         
    }
}
