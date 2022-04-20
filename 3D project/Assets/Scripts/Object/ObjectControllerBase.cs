using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControllerBase : BaseInfo
{
    public delegate void DieMotionHandler(GameObject go, Vector3 direction);
    public event DieMotionHandler OnDieMotionEvent;

    


    //public function

    public void BeAttacked(Player player)
    {
        int debugD = Random.Range(player.minDamage + player.addMinDamage, player.maxDamage + player.addMaxDamage);
        
        hp -= debugD;

        //������ â
        UIManager.GetInstance().ObjectHP.SetName(objectName);
        UIManager.GetInstance().ObjectHP.SetHpBar((float)hp/ (float)maxHp);
        UIManager.GetInstance().ObjectHP.ViewHP();
        
        if (hp < 0)
        {
            gameObject.layer = LayerMask.NameToLayer("ImpossibleHit");
            if (OnDieMotionEvent!= null)
            {
                //�״� ���
                OnDieMotionEvent(gameObject, player.transform.forward);
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
            
            //��� ���
            Debug.Log("object���");
        }
    }

}
