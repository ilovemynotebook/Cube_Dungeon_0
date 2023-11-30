using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProjectile : Projectile
{


    protected override void Start()
    {
        base.Start();
        transform.position = _boss.Target.transform.position;

    }

    private void FixedUpdate()
    {
        Vector3 movePos = new Vector3(_speed * _dir * Time.deltaTime, 0, 0);
        _rigidBody.MovePosition(transform.position + movePos);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character.GetHit(_power);
            Destroy(gameObject);
        }

        else if(other.tag == "Ground")
        {
            Destroy(gameObject);
        }

    }
}
