using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectHP : MonoBehaviour
{
    public Slider hpbar;
    public TextMeshProUGUI objectName;

    public Image panelBackground;
    public Image background;
    public Image frontBackground;
    
    public float viewCoolTIme;
    //투명화 진행 시간
    public float transparencyTime;


    //투명Temp color
    Color transparencyColor = new Color();

    float viewTIme = 99f;
    


    public void SetName(string name)
    {
        objectName.text = name;
    }

    public void SetHpBar(float hp)
    {
        hpbar.value = hp;
    }

    public void ViewHP()
    {
        viewTIme = 0f;
    }

    private void Update()
    {
        viewTIme += Time.deltaTime;
        if (viewTIme < viewCoolTIme)
        {
            SetTransparency(1f);
        }
        else
        {

            //Debug.Log("viewTIme: " + viewTIme);
            //Debug.Log("viewCoolTIme: " + viewCoolTIme);
            
            SetTransparency(1 - Mathf.Clamp((viewTIme - viewCoolTIme) / transparencyTime, 0, 1));
        }
    }


    private void SetTransparency(float alpha)
    {
        transparencyColor = panelBackground.color;
        transparencyColor.a = alpha * 0.5f;
        panelBackground.color = transparencyColor;

        transparencyColor = background.color;
        transparencyColor.a = alpha;
        background.color = transparencyColor;

        transparencyColor = frontBackground.color;
        transparencyColor.a = alpha;
        frontBackground.color = transparencyColor;

        transparencyColor = objectName.color;
        transparencyColor.a = alpha;
        objectName.color = transparencyColor;
    }
}
