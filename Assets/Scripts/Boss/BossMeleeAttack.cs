using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeAttack : BossAttackBehaviour
{

    [SerializeField] protected BoxCollider _skillBox;

    public Vector3 Size => _skillBox.size;

    public Vector3 Pos => _skillBox.transform.position + _skillBox.center;

    public Quaternion Rot => _skillBox.transform.rotation;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public override void SkillStart()
    {
        gameObject.SetActive(true);
    }

    public override void SkillEnd()
    {
        gameObject.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character.GetHit(_boss.Power * _powerMul);
        }
    }
}
