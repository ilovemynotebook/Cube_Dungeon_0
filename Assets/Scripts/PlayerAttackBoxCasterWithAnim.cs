using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBoxCasterWithAnim : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject attackHitBox;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }


    public void AttackHitBoxCast()
    {
        //if (dmg == 0) dmg = player.currentWeapon.Dmg + player.buffedDmg;
        GameObject box = Instantiate(attackHitBox, transform.parent.transform.position, Quaternion.identity);
        Debug.Log(box);
        box.GetComponentInChildren<PlayerAttackHitBox>().SetUp(player.currentWeapon.Dmg + player.buffedDmg, 0.3f, true, player.gameObject);
        //box.GetComponent<PlayerAttackHitBox>().SetUp(dmg, 0.3f, true, player.gameObject);
    }
}
