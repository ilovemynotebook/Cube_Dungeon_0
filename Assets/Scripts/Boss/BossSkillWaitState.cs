using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillWaitState : BossStateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        float animeLength = stateInfo.length;
        _boss.AddWaingTimer(animeLength);

        Vector3 dir = (_boss.Target.transform.position - _boss.gameObject.transform.position).normalized;
        float dirX = dir.x > 0 ? 1 : dir.x < 0 ? -1 : 0;

        if (dirX == 1)
            _boss.transform.rotation = Quaternion.Euler(0, 90, 0);

        else if (dirX == -1)
            _boss.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

}
