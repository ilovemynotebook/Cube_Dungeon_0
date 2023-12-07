using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : Projectile
{
    [SerializeField] private BossParticle _explosionPaticlePrefab;

    [SerializeField] private float _randomRange;

    private Vector3 _targetDir;

    

    protected override void Start()
    {
        base.Start();
        Vector3 myPos = transform.position;

        float targetPosX = _boss.Target.transform.position.x + Random.Range(-_randomRange, _randomRange);
        float targetPosZ = _boss.Target.transform.position.z + Random.Range(-_randomRange, _randomRange);
        Vector3 targetPos = new Vector3(targetPosX, _boss.Target.transform.position.y, targetPosZ);
        _targetDir = (targetPos - myPos).normalized;
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
            character.GetHit(_power);
            Explosion();
        }

        if(other.tag == "Ground")
        {
            Destroy(gameObject);
            Explosion();
        }
    }


    private void Explosion()
    {
        Instantiate(_explosionPaticlePrefab, transform.position, Quaternion.identity);
        _explosionPaticlePrefab.Init(_boss, _power);
        Destroy(gameObject);
    }
}
