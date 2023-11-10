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



    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "PlayerHitBox")
        {
            ItemGet(collision.gameObject.GetComponentInParent<Player>());    
        }
    }

    void ItemGet(Player player)
    {
        player.isUpgraded_weapon = isUpgraded_weapon ? true : false;
        player.isUpgraded_shield = isUpgraded_shield ? true : false;
        player.isUpgraded_Item_0 = isUpgraded_Item_0 ? true : false;
        player.isUpgraded_Item_1 = isUpgraded_Item_1 ? true : false;
        player.isUpgraded_Item_2 = isUpgraded_Item_2 ? true : false;
        player.isUpgraded_Item_3 = isUpgraded_Item_3 ? true : false;
        player.key = key ? true : false;
        player.hpPotion += hpPotion;
        player.staPotion += staPotion;
        player.dmgPotion += dmgPotion;

        Destroy(gameObject);
    }


    public void setItem(int hpPCramp, int staPCramp, int dmgPCramp, bool weapon = false,
        bool shield = false, bool item0 = false, bool item1 = false,
        bool item2 = false, bool item3 = false)
    {
        hpPotion = Random.Range(0, hpPCramp);
        staPotion = Random.Range(0, staPCramp);
        dmgPotion = Random.Range(0, dmgPCramp);
        isUpgraded_weapon = weapon;
        isUpgraded_shield = shield;
        isUpgraded_Item_0 = item0;
        isUpgraded_Item_1 = item1;
        isUpgraded_Item_2 = item2;
        isUpgraded_Item_3 = item3;
    }
}
