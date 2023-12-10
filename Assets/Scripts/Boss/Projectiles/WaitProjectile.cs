using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitProjectile : Projectile
{
    [SerializeField] private float _waitTime;

    [SerializeField] private GameObject _shootParticle;

    [SerializeField] private GameObject _explosionParticlePrefab;

    private float _waitTimer;

    private Vector3 _myPos;
    private Vector3 _targetDir;

    protected override void Start()
    {
        base.Start();
        if (_shootParticle != null)
            _shootParticle.SetActive(false);
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
                transform.LookAt(_boss.Target.transform);
            }
            else
            {
                transform.parent = null;

                Vector3 movePos = _targetDir * (_speed * Time.deltaTime);
                _rigidBody.MovePosition(transform.position + movePos);

                if(_shootParticle != null)
                    _shootParticle.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character.GetHit(_power);

            if(_explosionParticlePrefab != null)
                Instantiate(_explosionParticlePrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        if(other.tag == "Ground")
        {
            if (_explosionParticlePrefab != null)
                Instantiate(_explosionParticlePrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }

}
