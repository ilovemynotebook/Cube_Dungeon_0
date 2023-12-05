using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Projectile : MonoBehaviour
{

    [SerializeField] protected float _speed;

    [SerializeField] protected float _disabledTime;

    protected BossController _boss;

    protected Rigidbody _rigidBody;

    protected float _power;

    protected int _dir;






    public void SetPower(BossController boss, float power, int dir)
    {
        _boss = boss;
        _power = power;
        _dir = dir;
    }

    protected virtual void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, _disabledTime);
    }

}
