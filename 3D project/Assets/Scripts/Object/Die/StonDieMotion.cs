using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonDieMotion : MonoBehaviour
{
    public ObjectControllerBase controller;

    public Transform[] ItemPosition;

    Rigidbody treeRigidBody;
    CapsuleCollider treeCollider;

    //드랍 아이템
    GameObject dropItem;

    private void Awake()
    {
        treeCollider = GetComponent<CapsuleCollider>();
        controller.OnDieMotionEvent += DieStart;
    }

    // Update is called once per frame
    void Update()
    {
    }



    void DieStart(GameObject go, Vector3 direction)
    {
        //아이템 드랍
        for (int i = 0; i < ItemPosition.Length; i++)
        {
            dropItem = ItemListManager.GetInstance().GetItem("Stone");
            dropItem.transform.position = ItemPosition[i].transform.position;
            dropItem.transform.eulerAngles = ItemPosition[i].transform.eulerAngles;
        }

        GameObject.Destroy(gameObject);
        AudioManager.GetInstance().OnShotPlay("Destroy_Stone", 0.2f);
    }
}
