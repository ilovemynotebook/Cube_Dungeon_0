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
    [Tooltip("��ų �̸�")]
    public string Name;

    [Tooltip("���� ��Ÿ�")]
    public float AttackDistance;

    [Tooltip("��Ÿ��")]
    public float CoolTime;

    [HideInInspector] public float CurrentCoolTime;

    [HideInInspector] public BossState SkillState;
}


[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class Boss : MonoBehaviour
{
    [Space(10)]
    [Header("�ɷ�ġ")]

    [Tooltip("�̸�")]
    [SerializeField] private string _name;
    public string Name => _name;

    [Tooltip("ü��")]
    [SerializeField] private int _maxHp;

    [Tooltip("�̵� �ӵ�")]
    [SerializeField] private float _speed;
    public float Speed => _speed;

    [Tooltip("���ݷ�")]
    [SerializeField] private float _power;
    public float Power => _power;

//===============================================================================================

    [Space(10)]
    [Header("AI")]

    [Tooltip("���� ���� ������")]
    [SerializeField] private SkillPattern[] _skillPatterns;

    [Tooltip("AI ���� ���� �ð�")]
    [SerializeField] private float _patternUpdateTime;

    [Tooltip("���� �� ��� �ð�")]
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


    /// <summary> ��ų ��Ÿ�ӵ��� �����ϴ� �Լ� </summary>
    private void SkillCoolTimeUpdate()
    {
        foreach(SkillPattern pattern in _skillPatterns)
        {
            if (0 < pattern.CurrentCoolTime)
                pattern.CurrentCoolTime -= Time.deltaTime;
        }
    }


    /// <summary> ���� ��� ������ ��ų�� ��ġ�ϴ� �Լ� </summary>
    public SkillPattern GetUsableSkill()
    {
        _usableSkillList.Clear();

        foreach (SkillPattern pattern in _skillPatterns)
        {
            if (0 < pattern.CurrentCoolTime)
            {
                Debug.Log("��Ÿ�� ����");
                continue;
            }
                

            if (pattern.AttackDistance < TargetDistance)
            {
                Debug.Log("�Ÿ��� ��");
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
