using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitProjectile : Projectile
{
    [SerializeField] private float _waitTime;

    private float _waitTimer;

    private Vector3 _myPos;
    private Vector3 _targetDir;

    protected override void Start()
    {
        base.Start();

    }


    private void FixedUpdate()
    {
        _waitTimer += Time.deltaTime;


        if (_waitTime <= _waitTimer)
        {
            if(_myPos == null || _myPos == Vector3.zero)
            {
                _myPos = transform.position;
                _targetDir = (_boss.Target.transform.position - _myPos).normalized;
            }
            else
            {
                transform.parent = null;

                Vector3 movePos = _targetDir * (_speed * Time.deltaTime);
                _rigidBody.MovePosition(transform.position + movePos);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character.GetHit(_power);
            Destroy(gameObject);
        }
    }

}
