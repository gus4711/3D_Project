using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInfo: MonoBehaviour
{
    [SerializeField]
    public string objectName;
    [SerializeField]
    protected int _hp;
    public int hp{ get { return _hp; } set { _hp = value; } }
    [SerializeField]
    protected int _maxHp;
    public int maxHp { get { return _maxHp; } set { _maxHp = value; } }
    [SerializeField]
    protected int _stamina;
    public int stamina { get { return _stamina; } set { _stamina = value; } }
    [SerializeField]
    protected int _maxStamina;
    public int maxStamina { get { return _maxStamina; } set { _maxStamina = value; } }

    [SerializeField]
    protected int _minDamage;
    public int minDamage { get { return _minDamage; } set { _minDamage = value; } }
    [SerializeField]
    protected int _maxDamage;
    public int maxDamage { get { return _maxDamage; } set { _maxDamage = value; } }




    [SerializeField] int _addMinDamage;
    public int addMinDamage { get { return _addMinDamage; } set { _addMinDamage = value; } }
    [SerializeField] int _addMaxDamage;
    public int addMaxDamage { get { return _addMaxDamage; } set { _addMaxDamage = value; } }


}
