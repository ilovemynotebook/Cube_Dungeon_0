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


    public void AttackHitBoxCast(float dmg)
    {
        if (dmg == 0) dmg = player.currentWeapon.Dmg + player.buffedDmg;
        //dmg가 0이면 플레이어의 공격력을 가한다.
        //특정 값을 입력하면 그 값만큼을 가한다.
        //만약 0을 원한다해도, 1만큼은 있어야 한다.

        if (dmg == -1) dmg = (player.currentWeapon.Dmg + player.buffedDmg) * 2;
        // + 만약 -1을 입력하면 캐릭터의 두배 데미지. 차징 공격 의미


        Debug.Log(dmg);
        GameObject box = Instantiate(attackHitBox, transform.parent.transform.position, transform.parent.rotation);
        //box.GetComponentInChildren<PlayerAttackHitBox>().SetUp(player.currentWeapon.Dmg + player.buffedDmg, 0.3f, true, player.gameObject);
        box.GetComponentInChildren<PlayerAttackHitBox>().SetUp(dmg, 0.3f, true, player.gameObject);
    }
}
