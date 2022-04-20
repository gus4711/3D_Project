using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDieMotion : MonoBehaviour
{
    public ObjectControllerBase controller;

    public Transform[] ItemPosition;

    Rigidbody treeRigidBody;

    //밀치는 힘 - 넘어짐 효과
    public float forceValue;

    //나무 파괴
    bool dieStart;
    float dieTime;
    public float dieCoolTime = 5f;
    public float angularDrag = 10f;

    //드랍 아이템
    GameObject dropItem;

    private void Awake()
    {
        controller.OnDieMotionEvent += DieStart;
        dieTime = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (dieStart)
        {
            dieTime += Time.deltaTime;
            if (dieTime > dieCoolTime) {

                                //아이템 드랍
                for (int i = 0; i < ItemPosition.Length; i++)
                {
                    dropItem = ItemListManager.GetInstance().GetItem("Firewood");
                    dropItem.transform.position = ItemPosition[i].transform.position;
                    dropItem.transform.eulerAngles = ItemPosition[i].transform.eulerAngles;
                }


                GameObject.Destroy(gameObject);
                
            }
        }
    }



    void DieStart(GameObject go, Vector3 direction)
    {
        dieStart = true;
        //나무 자유 낙하
        treeRigidBody = gameObject.AddComponent<Rigidbody>();
        treeRigidBody.angularDrag = angularDrag;

        //특정방향 힘 작용
        treeRigidBody.AddForce(direction * forceValue);

        AudioManager.GetInstance().OnShotPlay("Destroy_Tree", 0.3f);
    }
}
