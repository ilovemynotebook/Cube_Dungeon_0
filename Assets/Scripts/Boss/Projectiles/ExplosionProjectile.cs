using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : Projectile
{
    private Vector3 _myPos;
    Vector3 _targetDir;


    protected override void Start()
    {
        base.Start();
        _myPos = transform.position;
        _targetDir = (_boss.Target.transform.position - _myPos).normalized;
    }


    private void FixedUpdate()
    {

        Vector3 movePos = _targetDir * (_speed * Time.deltaTime);
        _rigidBody.MovePosition(transform.position + movePos);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            Explosion(character);
        }
    }


    private void Explosion(Character character)
    {
        character.GetHit(_power);
        Destroy(gameObject);
    }
}
