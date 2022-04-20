using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHMPS : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Slider hpBar;
    [SerializeField]
    Slider mpBar;
    [SerializeField]
    Slider staminaBar;



    void Start()
    {
        hpBar.value = (float)player.hp / (float)player.maxHp;
        staminaBar.value = (float)player.stamina / (float)player.maxStamina;

        player.OnHMPSEvent += OnStamina;
    }

    void OnStamina(Player p)
    {
        hpBar.value = (float)player.hp / (float)player.maxHp;
        staminaBar.value = (float)player.stamina / (float)player.maxStamina;
    }
    
}
