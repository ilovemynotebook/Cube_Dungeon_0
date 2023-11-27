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
    [Tooltip("스킬 이름")]
    public string Name;

    [Tooltip("공격 사거리")]
    public float AttackDistance;

    [Tooltip("쿨타임")]
    public float CoolTime;

    [HideInInspector] public float CurrentCoolTime;
}


[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Boss : MonoBehaviour
{
    [Tooltip("이름")]
    [SerializeField] private string _name;

    [Tooltip("체력")]
    [SerializeField] private int _maxHp;

    [Tooltip("이동 속도")]
    [SerializeField] private float _speed;

    [Tooltip("AI 패턴 갱신 시간")]
    [SerializeField] private float _patternUpdateTime;

    [Tooltip("공격 패턴 데이터")]
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


    /// <summary> 스킬 쿨타임들을 관리하는 함수 </summary>
    private void SkillCoolTimeUpdate()
    {
        foreach(SkillPattern pattern in _skillPatterns)
        {
            if (pattern.CurrentCoolTime > 0)
                pattern.CurrentCoolTime = 0;

            Debug.Log(pattern.CurrentCoolTime);
        }
    }


    /// <summary> 현재 사용 가능한 스킬을 배치하는 함수 </summary>
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
