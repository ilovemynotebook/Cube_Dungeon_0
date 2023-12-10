using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProjectile : Projectile
{
    [SerializeField] private BossParticle _particle;

    private int dirX;

    protected override void Start()
    {
        base.Start();
        Vector3 dir = (transform.position - _boss.gameObject.transform.position).normalized;
        dirX = dir.x > 0 ? 1 : dir.x < 0 ? -1 : 0;

        transform.position = _boss.Target.transform.position;
        transform.position += new Vector3(10 * dirX, 20, 0);

        transform.LookAt(_boss.Target.transform);
        transform.Rotate(new Vector3(90, 0, 0));
    }

    private void FixedUpdate()
    {
        Vector3 movePos = transform.up * Time.deltaTime * _speed;
        _rigidBody.MovePosition(transform.position + movePos);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character.GetHit(_power);
        }

        else if(other.tag == "Ground")
        {
            Instantiate(_particle, transform.position + transform.up * 10, Quaternion.identity).Init(_boss, _power * 0.5f);
            Destroy(gameObject);

        }

    }
}
