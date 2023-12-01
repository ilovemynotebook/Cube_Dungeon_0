using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : Projectile
{
    private Vector3 _myPos;
    protected override void Start()
    {
        base.Start();
        _myPos = transform.position;
    }
    private void FixedUpdate()
    {
        Vector3 targetDir = (_boss.Target.transform.position - _myPos).normalized;
        Vector3 movePos = targetDir * (_speed * Time.deltaTime);
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
