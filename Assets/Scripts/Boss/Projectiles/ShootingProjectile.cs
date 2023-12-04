using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingProjectile : Projectile
{
    [SerializeField] private ShootingObject ShootingPrefab;

    [SerializeField] private float _shootingIntervalTime;

    private float _shootingIntervalTimer;

    private Vector3 _secondForRotate; 

    protected override void Start()
    {
        base.Start();

        _secondForRotate = new Vector3(0, 0, Random.Range(180f, 360f));

    }


    private void FixedUpdate()
    {
        gameObject.transform.Rotate(_secondForRotate * Time.deltaTime);

        _shootingIntervalTimer -= Time.deltaTime;

        if(_shootingIntervalTimer <= 0)
        {
            Instantiate(ShootingPrefab, gameObject.transform.position + transform.up * 2, transform.rotation).Init(_boss, _speed, _power);
            _shootingIntervalTimer = _shootingIntervalTime;
        }

    }

}
