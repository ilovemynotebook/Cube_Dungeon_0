using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData 
{
    public float hp;
    public float mhp;
    public float sta;
    public float msta;
    public bool isUpgraded_weapon;
    public bool isUpgraded_shield;
    public bool isUpgraded_Item_0;
    public bool isUpgraded_Item_1;
    public bool isUpgraded_Item_2;
    public bool isUpgraded_Item_3;
    public int hpPotion;
    public int staPotion;
    public int dmgPotion;
    public bool key;

    public int thisStage;
    public int thisPlane;

    public bool[] Plane1boxesOpened;
    public bool[] Plane2boxesOpened;
    public bool[] Plane3boxesOpened;
    public bool[] Plane4boxesOpened;
    public bool[] Plane5boxesOpened;
    public bool[] Plane6boxesOpened;
    public bool[] Plane7boxesOpened;
    public bool[] Plane8boxesOpened;
    public bool[] Plane9boxesOpened;
}
