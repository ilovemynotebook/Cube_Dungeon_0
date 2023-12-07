using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isUpgraded_weapon = false;
    public bool isUpgraded_shield = false;
    public bool isUpgraded_Item_0 = false;
    public bool isUpgraded_Item_1 = false;
    public bool isUpgraded_Item_2 = false;
    public bool isUpgraded_Item_3 = false;
    public int hpPotion = 0;
    public int staPotion = 0;
    public int dmgPotion = 0;
    public bool key = false;
    //리팩을 하긴 해야할 듯
    //귀찮아서 안할래
    public GameObject ItemPrefab;
    public bool isEButtonNeeded = false;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "PlayerHitBox")
        {
            //ItemGet(collision.gameObject.GetComponentInParent<Player>());    

        }
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        MultipleItemSpawn(isEButtonNeeded);
    }

    void MultipleItemSpawn(bool isButtonNeeded)
    {
        if (isUpgraded_weapon)
            ItemSpawn(0, isButtonNeeded);
        if (isUpgraded_shield)
            ItemSpawn(1, isButtonNeeded);
        if (isUpgraded_Item_0)
            ItemSpawn(2, isButtonNeeded);
        if (isUpgraded_Item_1)
            ItemSpawn(3, isButtonNeeded);
        if (isUpgraded_Item_2)
            ItemSpawn(4, isButtonNeeded);
        if (isUpgraded_Item_3)
            ItemSpawn(5, isButtonNeeded);
        if (key)
            ItemSpawn(6, isButtonNeeded);
        if (hpPotion > 0)
            ItemSpawn(7, isButtonNeeded);
        if (staPotion > 0)
            ItemSpawn(8, isButtonNeeded);
        if (dmgPotion > 0)
            ItemSpawn(9, isButtonNeeded);
        //안겹치게 왼,오 순으로 하나씩 기차처럼 나오게 할까?

        Destroy(gameObject);
    }

    void ItemSpawn(int _whatItem, bool isButtonNeeded = false, int _howMany = 1)
    {
        DroppedItem _item = Instantiate(ItemPrefab, transform.position, transform.rotation).GetComponent<DroppedItem>();
        _item.whatItem = (DroppedItem.WhatItem)_whatItem;
        _item.isButtonNeeded = isButtonNeeded;
        if (5 < _whatItem) _item.howMany = _howMany;
        
    }

    /*
    void ItemGet(Player player)
    {
        player.isUpgraded_weapon = isUpgraded_weapon ? true : player.isUpgraded_weapon;
        player.isUpgraded_shield = isUpgraded_shield ? true : player.isUpgraded_shield;
        player.isUpgraded_Item_0 = isUpgraded_Item_0 ? true : player.isUpgraded_Item_0;
        player.isUpgraded_Item_1 = isUpgraded_Item_1 ? true : player.isUpgraded_Item_1;
        player.isUpgraded_Item_2 = isUpgraded_Item_2 ? true : player.isUpgraded_Item_2;
        player.isUpgraded_Item_3 = isUpgraded_Item_3 ? true : player.isUpgraded_Item_3;
        player.key = key ? true : player.key;
        player.hpPotion += hpPotion;
        player.staPotion += staPotion;
        player.dmgPotion += dmgPotion;

        CanvasManager.Instance.isUpgraded_weapon = player.isUpgraded_weapon;
        CanvasManager.Instance.isUpgraded_shield = player.isUpgraded_shield;
        CanvasManager.Instance.isUpgraded_Item_0 = player.isUpgraded_Item_0 ;
        CanvasManager.Instance.isUpgraded_Item_1 = player.isUpgraded_Item_1;
        CanvasManager.Instance.isUpgraded_Item_2 = player.isUpgraded_Item_2;
        CanvasManager.Instance.isUpgraded_Item_3 = player.isUpgraded_Item_3;
        CanvasManager.Instance.key = player.key;
        CanvasManager.Instance.hpPotion = player.hpPotion;
        CanvasManager.Instance.staPotion = player.staPotion;
        CanvasManager.Instance.dmgPotion = player.dmgPotion;


        player.ItemStats();
        CanvasManager.Instance.UpdateHud();

        Destroy(gameObject);
    }
    */

    
    public void setItem(int hpP, int staP, int dmgP, bool weapon = false,
        bool shield = false, bool item0 = false, bool item1 = false,
        bool item2 = false, bool item3 = false)
    {
        //hpPotion = Random.Range(0, hpP);
        //staPotion = Random.Range(0, staP);
        //dmgPotion = Random.Range(0, dmgP);
        hpPotion = hpP;
        staPotion = staP;
        dmgPotion = dmgP;
        isUpgraded_weapon = weapon;
        isUpgraded_shield = shield;
        isUpgraded_Item_0 = item0;
        isUpgraded_Item_1 = item1;
        isUpgraded_Item_2 = item2;
        isUpgraded_Item_3 = item3;
    }
}
