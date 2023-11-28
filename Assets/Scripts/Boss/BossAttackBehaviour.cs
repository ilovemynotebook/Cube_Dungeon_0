using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttackBehaviour : MonoBehaviour
{

    [SerializeField] protected Boss _boss;


    [Tooltip("���ݷ� ���")]
    [SerializeField] protected float _powerMul;

    public abstract void SkillStart();

    public abstract void SkillEnd();

}
