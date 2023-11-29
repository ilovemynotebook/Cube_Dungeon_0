using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingState : BossStateMachineBehaviour
{
    private float _speed => _boss.Speed;

    private float _timer;

    private int _dirX;



    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        Vector3 dir = (_boss.Target.transform.position - _boss.gameObject.transform.position).normalized;
        _dirX = dir.x > 0 ? 1 : dir.x < 0 ? -1 : 0;
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        SetDir();
        Move();
    }


    private void Move()
    {
        if(_dirX == 1)
            _boss.transform.rotation = Quaternion.Euler(0, 90, 0);

        else if (_dirX == -1)
            _boss.transform.rotation = Quaternion.Euler(0, -90, 0);

        _boss.transform.position += new Vector3(_dirX * _speed * Time.deltaTime, 0, 0);
        //_boss.Rigidbody.MovePosition(_boss.transform.position  + new Vector3(_dirX * _speed * Time.deltaTime, 0, 0));
    }

    private void SetDir()
    {
        _timer += Time.deltaTime;
        if (0.5f < _timer)
        {
            Vector3 dir = (_boss.Target.transform.position - _boss.gameObject.transform.position).normalized;
            _dirX = dir.x > 0 ? 1 : dir.x < 0 ? -1 : 0;
            _timer = 0;
        }

    }
}
