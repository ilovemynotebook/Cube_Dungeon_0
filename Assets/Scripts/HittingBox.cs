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
                if (player.sta >= dmg) // ��������
                {
                    player.ShieldSucceed();
                    Character Ene = GetComponentInParent<Character>().GetComponent<Character>();
                    direction.x = direction.x * -1;
                    Ene.KnockBack(dmg * direction * 4);
                }
                else //���� �μ���
                {
                    player.ShieldBroke();
                    player.KnockBack(dmg * direction * 4);
                }
            }
            else // ������
            {
                player.GetHit(dmg);
                player.KnockBack(dmg * direction * 8);
            }

            if (isOnce == true)
            {
                if(Root == null)
                Root = transform.parent.gameObject;
                Destroy(Root);
            }
        }
    }
}
