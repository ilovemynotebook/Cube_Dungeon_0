using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerData
{
    public float hp;// { get; private set; }
    public float mhp;// { get; private set; }
    public float sta;// { get; private set; }
    public float msta;// { get; private set; }
    public bool isUpgraded_weapon ;//{ get; private set; }
    public bool isUpgraded_shield;// { get; private set; }
    public bool isUpgraded_Item_0;// { get; private set; }
    public bool isUpgraded_Item_1 ;//{ get; private set; }
    public bool isUpgraded_Item_2 ;//{ get; private set; }
    public bool isUpgraded_Item_3 ;//{ get; private set; }
    public int hpPotion;// { get; private set; }
    public int staPotion ;//{ get; private set; }
    public int dmgPotion ;//{ get; private set; }
    public bool key;// { get; private set; }

    //public PlayerData(float hp, float mhp, float sta, float msta,int hpPotion,int staPotion,int dmgPotion)
    //{
    //    this.hp = hp;
    //    this.mhp = mhp;
    //    this.sta = sta;
    //    this.msta = msta;
    //    this.hpPotion = hpPotion;
    //    this.staPotion = staPotion;
    //    this.dmgPotion = dmgPotion;


    //}
    public PlayerData()
    {
        this.hp = 100;
        this.mhp = 100;
        this.sta = 100;
        this.msta = 100;
        this.hpPotion = 0;
        this.staPotion = 0;
        this.dmgPotion = 0;
        this.isUpgraded_Item_0 = false;
        this.isUpgraded_Item_1 = false;
        this.isUpgraded_Item_2 = false;
        this.isUpgraded_Item_3 = false;
        this.isUpgraded_weapon = false;
        this.isUpgraded_shield = false;
        this.key = false;
    }

    public void SetPlayerData(float hp, float mhp, float sta, float msta, int hpPotion, int staPotion, int dmgPotion)
    {
        this.hp = hp;
        this.mhp = mhp;
        this.sta = sta;
        this.msta = msta;
        this.hpPotion = hpPotion;
        this.staPotion = staPotion;
        this.dmgPotion = dmgPotion;

    }
}

