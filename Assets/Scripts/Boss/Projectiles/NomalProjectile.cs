using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalProjectile : Projectile
{

    public override void SetPower(BossController boss, float power, int dir)
    {
        base.SetPower(boss, power, dir);

        if (dir == -1)
            transform.rotation = Quaternion.Euler(0, -180, 0);
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
