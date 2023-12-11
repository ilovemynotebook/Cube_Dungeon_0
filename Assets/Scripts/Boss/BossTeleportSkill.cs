using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleportSkill : BossAttackBehaviour
{
    [SerializeField] private Vector3[] _teleportPos;


    public override void SkillStart()
    {
        Vector3 bossPos = _boss.transform.position;
        Vector3 telPos;
        do
        {
            int randInt = Random.Range(0, _teleportPos.Length);
            telPos = _teleportPos[randInt];

        } while (Vector3.Distance(bossPos, telPos) < 2);
        _boss.transform.position = telPos;
    }

    public override void SkillEnd()
    {


    }


}
