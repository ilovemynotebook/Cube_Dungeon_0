using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Idle,
    Tracking,
    Attack,
}

[Serializable]
public class SkillPattern
{
    [Tooltip("���� ��Ÿ�")]
    public float _attackDistance;

    [Tooltip("��Ÿ��")]
    public float _coolTime;
}


[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Boss : MonoBehaviour
{
    [Tooltip("�̸�")]
    [SerializeField] private string _name;

    [Tooltip("ü��")]
    [SerializeField] private int _maxHp;

    [Tooltip("�̵� �ӵ�")]
    [SerializeField] private float _speed;

    [Tooltip("AI ���� ���� �ð�")]
    [SerializeField] private float _patternUpdateTime;

    [Tooltip("���� ���� ������")]
    [SerializeField] private SkillPattern[] _skillPatterns;

    private int _hp;

    private BossAI _ai;

    private BossStateMachineBehaviour[] _bossStateMachines;

    private Animator _animator;

    private Rigidbody _rigidBody;

    public BossState State;

    public GameObject Target;

    public float CurrentAttackDistance => _skillPatterns[0]._attackDistance;

    public int Hp => _hp;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _bossStateMachines = _animator.GetBehaviours<BossStateMachineBehaviour>();
        _rigidBody = GetComponent<Rigidbody>();

        foreach(BossStateMachineBehaviour bossStateMachine in _bossStateMachines)
        {
            bossStateMachine.Init(this);
        }

        _ai = new BossAI(this);
    }

    protected virtual void Start()
    {
        InvokeRepeating("AIUpdate", 1, 1);
    }


    protected virtual void Update()
    {

        _animator.SetInteger("State", (int)State);
    }

    private void AIUpdate()
    {
        _ai?.Update();
    }
}
