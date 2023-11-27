using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingState : BossStateMachineBehaviour
{
    [SerializeField] private float _speed;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        Move();
    }


    private void Move()
    {
        Vector3 dir = (_boss.Target.transform.position - _boss.gameObject.transform.position).normalized;
        float dirZ = dir.z > 0 ? 1 : dir.z < 0 ? -1 : 0;

        Debug.Log(dirZ);
        _boss.transform.position += new Vector3(0, 0, dirZ * _speed * Time.deltaTime);
    }
}
