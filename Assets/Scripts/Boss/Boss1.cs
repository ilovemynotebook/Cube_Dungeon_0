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
    Skill4,
    Skill5,
    Die,
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

    [Tooltip("피격 범위 관리 클래스")]
    public BossAttackBehaviour AttackTrigger;

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

    [Tooltip("애니메이션 스피드")]
    [SerializeField] private float _animeSpeed;
    public float AnimeSpeed => _animeSpeed;

    [Tooltip("공격력")]
    [SerializeField] private float _power;
    public float Power => _power;

//===============================================================================================

    [Space(10)]
    [Header("AI")]

    [Tooltip("공격 패턴 데이터")]
    [SerializeField] private SkillPattern[] _skillPatterns;
    public SkillPattern[] SkillPatterns => _skillPatterns;

    [Tooltip("AI 패턴 갱신 시간")]
    [SerializeField] private float _patternUpdateTime;

    [Tooltip("공격 후 대기 시간")]
    [SerializeField] private float _waitTime;
    private float _waitTimer;



    private List<SkillPattern> _usableSkillList = new List<SkillPattern>();

    private Boss1AI _ai;

    private BossStateMachineBehaviour[] _bossStateMachines;

    private Animator _animator;

    private float _hp;

    [HideInInspector] public Rigidbody Rigidbody;

    public BossState State;

    public GameObject Target;

    public event Action OnGetHitHandler;
    public event Action OnDeathEventHandler;


    public float TargetDistance { get { return Vector3.Distance(Target.transform.position, gameObject.transform.position); } }

    public float Hp => _hp;

    protected virtual void Awake()
    {
        Init();
    }


    protected virtual void Start()
    {
        SetWaingTimer();
        InvokeRepeating("AIUpdate", _patternUpdateTime, _patternUpdateTime);
    }


    protected virtual void Update()
    {
        _animator.SetInteger("State", (int)State);
        _animator.SetFloat("AnimeSpeed", AnimeSpeed);
        SkillCoolTimeUpdate();
        UpdateWaitTimer();
    }


    protected virtual void Init()
    {
        _animator = GetComponent<Animator>();
        _bossStateMachines = _animator.GetBehaviours<BossStateMachineBehaviour>();
        Rigidbody = GetComponent<Rigidbody>();
        _ai = new Boss1AI(this);

        foreach (BossStateMachineBehaviour bossStateMachine in _bossStateMachines)
        {
            bossStateMachine.Init(this);
        }

        for (int i = 0; i < _skillPatterns.Length; i++)
        {
            if (i == 5)
                break;

            _skillPatterns[i].SkillState = BossState.Skill1 + i;
        }

        _hp = _maxHp;
    }

    public void GetHit(float dmg)
    {
        _hp -= dmg;
        _hp = Mathf.Clamp(_hp, 0, _maxHp);

        if (_hp <= 0)
            OnGetHitHandler?.Invoke();
        else
            OnGetHitHandler?.Invoke();

        //anim.Play("Hit", 0, 0f);
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

    public void AddWaingTimer(float value)
    {
        _waitTimer += value;
    }
}
