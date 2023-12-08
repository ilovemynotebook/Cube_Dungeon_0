using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public bool isButtonNeeded = false;
    Player player;


    public enum WhatItem
    {
        item0,
        item1, 
        item2, 
        item3, 
        item4, 
        item5,
        key,
        hpP,
        staP,
        dmgP,
    };

    public int howMany = 1;
    public WhatItem whatItem = new WhatItem();

    private void OnTriggerStay(Collider collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.GetComponent<Player>();
            if(!isButtonNeeded)
            {
                Acquisition();
            }
            else
            {
                if (Input.GetKey(KeyCode.E))
                {
                    Acquisition();
                }
            }
        }
    }

    void Acquisition()
    {
        switch(whatItem)
        {
            case WhatItem.item0:
                CanvasManager.Instance.isUpgraded_weapon = true;
                break;
            case WhatItem.item1:
                CanvasManager.Instance.isUpgraded_shield = true;
                break;
            case WhatItem.item2:
                CanvasManager.Instance.isUpgraded_Item_0 = true;
                break;
            case WhatItem.item3:
                CanvasManager.Instance.isUpgraded_Item_1 = true;
                break;
            case WhatItem.item4:
                CanvasManager.Instance.isUpgraded_Item_2 = true;
                break;
            case WhatItem.item5:
                CanvasManager.Instance.isUpgraded_Item_3 = true;
                break;
            case WhatItem.key:
                CanvasManager.Instance.key = true;
                break;
            case WhatItem.hpP:
                CanvasManager.Instance.hpPotion += howMany;
                break;
            case WhatItem.staP:
                CanvasManager.Instance.staPotion += howMany;
                break;
            case WhatItem.dmgP:
                CanvasManager.Instance.dmgPotion += howMany;
                break;
            default:
                break;
        }

        player.sounds.GetItem_AS.Play();
        player.itemsSetup();
        player.ItemStats();
        CanvasManager.Instance.UpdateHud();

        Destroy(gameObject);
    }


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
    CanvasManager.Instance.isUpgraded_Item_0 = player.isUpgraded_Item_0;
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