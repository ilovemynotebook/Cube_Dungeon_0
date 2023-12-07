using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParticle : MonoBehaviour
{

    [SerializeField] protected float _disabledTime;

    protected float _power;

    protected BossController _boss;

    public void Init(BossController boss, float power)
    {
        _boss = boss;
       _power = power;
    }

    public virtual void Start()
    {
        Destroy(gameObject, _disabledTime);
    }
}
