using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class BoxData
{
    public Box box;
    public Vector3 spawnPos;
    public bool isOpen;
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

    public BoxData Cloneing()
    {
        BoxData boxData = new BoxData();
        boxData.box = box;
        boxData.spawnPos = spawnPos;
        boxData.isOpen = isOpen;
        boxData.hpPotion = hpPotion;
        boxData.staPotion = staPotion;
        boxData.dmgPotion = dmgPotion;
        boxData.key = key;
        boxData.isUpgraded_Item_0= isUpgraded_Item_0;
        boxData.isUpgraded_Item_1= isUpgraded_Item_1;
        boxData.isUpgraded_Item_2= isUpgraded_Item_2;
        boxData.isUpgraded_Item_3= isUpgraded_Item_3;
        return boxData;

    }
}


public class Box : MonoBehaviour
{
    public bool isOpen;
    MeshRenderer box;
    public Item item;

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
    private void Awake()
    {
        box = GetComponent<MeshRenderer>();
        
    }
    private void Start()
    {
        if (isOpen)
        {
            box.material.color = Color.red;
        }
        item.gameObject.SetActive(false);
        item.setItem(hpPotion, staPotion, dmgPotion, isUpgraded_weapon, isUpgraded_shield, isUpgraded_Item_0, isUpgraded_Item_1, isUpgraded_Item_2, isUpgraded_Item_3,key);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E)&&isOpen==false)
            {
                isOpen = true;
                PlaneSceneManager.Instance.StageSave(PlaneSceneManager.Instance.thisPlane);
                item.gameObject.SetActive(true);
                box.material.color = Color.red;
            }
        }
    }

    public void setitem(int hppotion,int stapotion,int dmgpotion)
    {
        hpPotion = hppotion;
        staPotion = stapotion;
        dmgPotion = dmgpotion;
    }

}
