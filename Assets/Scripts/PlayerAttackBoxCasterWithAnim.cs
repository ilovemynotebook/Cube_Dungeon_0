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
        //dmg�� 0�̸� �÷��̾��� ���ݷ��� ���Ѵ�.
        //Ư�� ���� �Է��ϸ� �� ����ŭ�� ���Ѵ�.
        //���� 0�� ���Ѵ��ص�, 1��ŭ�� �־�� �Ѵ�.

        if (dmg == -1) dmg = (player.currentWeapon.Dmg + player.buffedDmg) * 2;
        // + ���� -1�� �Է��ϸ� ĳ������ �ι� ������. ��¡ ���� �ǹ�


        Debug.Log(dmg);
        GameObject box = Instantiate(attackHitBox, transform.parent.transform.position, transform.parent.rotation);
        //box.GetComponentInChildren<PlayerAttackHitBox>().SetUp(player.currentWeapon.Dmg + player.buffedDmg, 0.3f, true, player.gameObject);
        box.GetComponentInChildren<PlayerAttackHitBox>().SetUp(dmg, 0.3f, true, player.gameObject);
    }
}
