using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss6_AttackWail : BossSkillStateMachine
{
    [SerializeField] private Vector3 _bossRot;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        _boss.transform.rotation = Quaternion.Euler(_bossRot);
    }
}
