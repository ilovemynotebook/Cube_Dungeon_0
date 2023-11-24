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
