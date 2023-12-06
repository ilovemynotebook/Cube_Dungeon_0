using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttackBehaviour : MonoBehaviour
{

    [SerializeField] protected BossController _boss;


    [Tooltip("공격력 배수")]
    [SerializeField] protected float _powerMul;

    public virtual void Init(BossController boss)
    {
        _boss = boss;
    }

    public abstract void SkillStart();

    public abstract void SkillEnd();

}
