using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeDieMotion : MonoBehaviour
{
    public ObjectControllerBase controller;

    public Transform[] ItemPosition;

    Rigidbody treeRigidBody;

    //��ġ�� �� - �Ѿ��� ȿ��
    public float forceValue;

    //���� �ı�
    bool dieStart;
    float dieTime;
    public float dieCoolTime = 5f;
    public float angularDrag = 10f;

    //��� ������
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

                                //������ ���
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
        //���� ���� ����
        treeRigidBody = gameObject.AddComponent<Rigidbody>();
        treeRigidBody.angularDrag = angularDrag;

        //Ư������ �� �ۿ�
        treeRigidBody.AddForce(direction * forceValue);

        AudioManager.GetInstance().OnShotPlay("Destroy_Tree", 0.3f);
    }
}
