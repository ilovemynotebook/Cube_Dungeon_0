using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public enum BossState
{
    Idle,
    Tracking,
    Skill1,
    Skill2,
    Skill3,
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

    [HideInInspector] public BossState SkillState;
}


[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Boss : MonoBehaviour
{
    [Space(10)]
    [Header("능력치")]

    [Tooltip("이름")]
    [SerializeField] private string _name;
    public string Name => _name;

    [Tooltip("체력")]
    [SerializeField] private int _maxHp;

    [Tooltip("이동 속도")]
    [SerializeField] private float _speed;
    public float Speed => _speed;

    [Tooltip("공격력")]
    [SerializeField] private float _power;
    public float Power => _power;

//===============================================================================================

    [Space(10)]
    [Header("AI")]

    [Tooltip("공격 패턴 데이터")]
    [SerializeField] private SkillPattern[] _skillPatterns;

    [Tooltip("AI 패턴 갱신 시간")]
    [SerializeField] private float _patternUpdateTime;

    [Tooltip("공격 후 대기 시간")]
    [SerializeField] private float _waitTime;
    private float _waitTimer;



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

    protected void Awake()
    {
        Init();
    }


    protected void Start()
    {
        InvokeRepeating("AIUpdate", _patternUpdateTime, _patternUpdateTime);
    }


    protected void Update()
    {
        _animator.SetInteger("State", (int)State);
        SkillCoolTimeUpdate();
        UpdateWaitTimer();
    }


    private void Init()
    {
        _animator = GetComponent<Animator>();
        _bossStateMachines = _animator.GetBehaviours<BossStateMachineBehaviour>();
        _rigidBody = GetComponent<Rigidbody>();
        _ai = new BossAI(this);

        foreach (BossStateMachineBehaviour bossStateMachine in _bossStateMachines)
        {
            bossStateMachine.Init(this);
        }

        for (int i = 0; i < _skillPatterns.Length; i++)
        {
            if (i == 3)
                break;

            _skillPatterns[i].SkillState = BossState.Skill1 + i;
        }
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
            if (0 < pattern.CurrentCoolTime)
                pattern.CurrentCoolTime -= Time.deltaTime;
        }
    }


    /// <summary> 현재 사용 가능한 스킬을 배치하는 함수 </summary>
    public SkillPattern GetUsableSkill()
    {
        _usableSkillList.Clear();

        foreach (SkillPattern pattern in _skillPatterns)
        {
            if (0 < pattern.CurrentCoolTime)
            {
                Debug.Log("쿨타임 아직");
                continue;
            }
                

            if (pattern.AttackDistance < TargetDistance)
            {
                Debug.Log("거리가 멈");
                continue;
            }
                
            _usableSkillList.Add(pattern);
        }

        if (_usableSkillList.Count == 0)
            return default;

        int randInt = Random.Range(0, _usableSkillList.Count);
        return _usableSkillList[randInt];
    }


    private void UpdateWaitTimer()
    {
        if(0 < _waitTimer)
            _waitTimer -= Time.deltaTime;
    }

    public bool WaitingTimeCheck()
    {
        if (!(0 < _waitTimer))
            return true;


        return false;
    }

    public void SetWaingTimer()
    {
        _waitTimer = _waitTime;
    }
}
