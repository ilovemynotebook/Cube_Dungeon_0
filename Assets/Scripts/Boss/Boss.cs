using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public enum BossState
{
    Idle,
    Tracking,
    Attack,
}

[Serializable]
public class SkillPattern
{
    [Tooltip("��ų �̸�")]
    public string Name;

    [Tooltip("���� ��Ÿ�")]
    public float AttackDistance;

    [Tooltip("��Ÿ��")]
    public float CoolTime;

    [HideInInspector] public float CurrentCoolTime;
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


    private List<SkillPattern> _usableSkillList = new List<SkillPattern>();

    private BossAI _ai;

    private BossStateMachineBehaviour[] _bossStateMachines;

    private Animator _animator;

    private Rigidbody _rigidBody;

    private int _hp;

    public BossState State;

    public GameObject Target;


    public float TargetDistance { get { return Vector3.Distance(Target.transform.position, gameObject.transform.position); } }

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
        InvokeRepeating("AIUpdate", _patternUpdateTime, _patternUpdateTime);
    }


    protected virtual void Update()
    {
        _animator.SetInteger("State", (int)State);
        SkillCoolTimeUpdate();
    }


    private void AIUpdate()
    {
        _ai?.Update();
    }


    /// <summary> ��ų ��Ÿ�ӵ��� �����ϴ� �Լ� </summary>
    private void SkillCoolTimeUpdate()
    {
        foreach(SkillPattern pattern in _skillPatterns)
        {
            if (pattern.CurrentCoolTime > 0)
                pattern.CurrentCoolTime = 0;

            Debug.Log(pattern.CurrentCoolTime);
        }
    }


    /// <summary> ���� ��� ������ ��ų�� ��ġ�ϴ� �Լ� </summary>
    public SkillPattern GetUsableSkill()
    {
        _usableSkillList.Clear();

        foreach (SkillPattern pattern in _skillPatterns)
        {
            if (pattern.CurrentCoolTime > 0)
                continue;

            if (pattern.AttackDistance < TargetDistance)
                continue;

            _usableSkillList.Add(pattern);
        }

        int randInt = Random.Range(0, _usableSkillList.Count);

        if (_usableSkillList.Count == 0)
            return default;

        return _usableSkillList[randInt];
    }
}
