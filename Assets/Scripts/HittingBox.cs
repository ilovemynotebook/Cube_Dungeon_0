using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HittingBox : MonoBehaviour
{
    public int dmg;
    Player player;

    public bool isOnce = false;
    public GameObject Root;

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag == "PlayerHitBox")
        {
            
            player = collision.GetComponentInParent<Player>();  
            Vector3 direction = (player.transform.position - transform.position).normalized;
            
            if (player.isHoldingShield && ((player.isCharacterLookRight && direction.x > 0) || (!player.isCharacterLookRight && direction.x <= 0)))
            {
                float playerShieldCost = player.shieldStaminaCost;
                if (player.isUpgraded_shield) playerShieldCost /= player.shieldProtect;
                if (player.sta >= playerShieldCost) // ¸·¾ÒÀ»¶§
                {
                    player.ShieldSucceed(playerShieldCost);

                    Character Ene = null;
                    transform.parent?.parent?.TryGetComponent<Character>(out Ene);
                    if(Ene == null) Ene = transform.parent?.parent?.parent?.GetComponent<Character>();

                    direction.x = direction.x * -1;
                    if (Ene != null) Ene.KnockBack(dmg * direction * 4);
                }
                else //½¯µå ºÎ¼ÅÁü
                {
                    player.ShieldBroke();
                    player.KnockBack(dmg * direction * 4);
                }
            }
            else // ¸ÂÀ»¶§
            {
                player.GetHit(dmg);
                player.KnockBack(dmg * direction * 4);
            }

            if (isOnce == true)
            {
                if(Root == null)
                Root = transform.parent.gameObject;
                Debug.Log(Root);
                Destroy(Root);
            }
        }
    }
}
