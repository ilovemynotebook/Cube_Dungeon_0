using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Equipment DataBase",
    menuName = "Scriptable Object/Equipment DataBase",
    order = int.MaxValue)]
public class EqupimentDataBase : ScriptableObject
{
    public WeaponData[] weapons;
    //public ShieldData[] shields;


}
