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

        //데미지 창
        UIManager.GetInstance().ObjectHP.SetName(objectName);
        UIManager.GetInstance().ObjectHP.SetHpBar((float)hp/ (float)maxHp);
        UIManager.GetInstance().ObjectHP.ViewHP();
        
        if (hp < 0)
        {
            gameObject.layer = LayerMask.NameToLayer("ImpossibleHit");
            if (OnDieMotionEvent!= null)
            {
                //죽는 모션
                OnDieMotionEvent(gameObject, player.transform.forward);
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
            
            //재료 드랍
            Debug.Log("object드랍");
        }
    }

}
