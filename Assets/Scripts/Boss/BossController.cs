using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    CustomSkill,
    Die = 10,
}

[Serializable]
public class SkillPattern
{
    [Tooltip("��ų �̸�")]
    [SerializeField] private string _name;
    public string Name => _name;

    [Tooltip("���� ��Ÿ�")]
    [SerializeField] private float _distance;
    public float Distance => _distance;

    [Tooltip("��Ÿ��")]
    [SerializeField] private float _coolTime;
    public float CoolTime => _coolTime;

    [Tooltip("�ǰ� ���� ���� Ŭ����")]
    public BossAttackBehaviour AttackTrigger;

    [HideInInspector] public float CurrentCoolTime;

    [HideInInspector] public BossState SkillState;
}


[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class BossController : MonoBehaviour
{
    [Space(10)]
    [Header("�ɷ�ġ")]

    [Tooltip("�̸�")]
    [SerializeField] protected string _name;
    public string Name => _name;

    [Tooltip("ü��")]
    [SerializeField] protected int _maxHp;

    [Tooltip("�̵� �ӵ�")]
    [SerializeField] protected float _speed;
    public float Speed => _speed;

    [Tooltip("�ִϸ��̼� ���ǵ�")]
    [SerializeField] protected float _animeSpeed;
    public float AnimeSpeed => _animeSpeed;

    [Tooltip("���ݷ�")]
    [SerializeField] protected float _power;
    public float Power => _power;

//===============================================================================================

    [Space(10)]
    [Header("AI")]

    [Tooltip("���� ���� ������")]
    [SerializeField] protected SkillPattern[] _skillPatterns;
    public SkillPattern[] SkillPatterns => _skillPatterns;

    [Tooltip("AI ���� ���� �ð�")]
    [SerializeField] protected float _patternUpdateTime;

    [Tooltip("���� �� ��� �ð�")]
    [SerializeField] protected float _waitTime;
    protected float _waitTimer;

    protected List<SkillPattern> _usableSkillList = new List<SkillPattern>();

    protected BossAI _ai;

    protected BossStateMachineBehaviour[] _bossStateMachines;

    protected Animator _animator;

    [SerializeField] protected List<Renderer> _renderers = new List<Renderer>();

    protected Dictionary<Renderer, Material> _tempMaterialDic = new Dictionary<Renderer, Material>();

    protected Material _hitMaterial;

    protected Coroutine _hitEffectRoutine;

    protected float _hp;

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

        _hp = _maxHp;

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

        if (transform.TryGetComponent(out Renderer myRenderer))
        {
            if (myRenderer.material != null)
            {
                _renderers.Add(myRenderer);
            }
        }

        foreach (Transform tfChild in transform)
        {
            if(tfChild.TryGetComponent(out Renderer renderer))
            {
                if(renderer.material != null)
                {
                    _renderers.Add(renderer);
                    _tempMaterialDic.Add(renderer, renderer.material);
                }
            }
        }

        _hitMaterial = (Material)Resources.Load("HitMaterial");
    }


    public void GetHit(float dmg)
    {
        _hp -= dmg;
        _hp = Mathf.Clamp(_hp, 0, _maxHp);

        if (_hitEffectRoutine != null)
            StopCoroutine(_hitEffectRoutine);
        _hitEffectRoutine = StartCoroutine(StartHitEffect());

        if (_hp <= 0)
            OnGetHitHandler?.Invoke();
        else
            OnGetHitHandler?.Invoke();

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
    public virtual SkillPattern GetUsableSkill()
    {
        _usableSkillList.Clear();

        foreach (SkillPattern pattern in _skillPatterns)
        {
            if (0 < pattern.CurrentCoolTime)
            {
                Debug.Log("��Ÿ�� ����");
                continue;
            }
                

            if (pattern.Distance < TargetDistance)
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

    public void AddWaingTimer(float value)
    {
        _waitTimer += value;
    }


    private IEnumerator StartHitEffect()
    {
        ChangeHitEffect();
        yield return new WaitForSeconds(0.1f);
        ChangeDefalutEffect();
        yield return new WaitForSeconds(0.1f);
        ChangeHitEffect();
        yield return new WaitForSeconds(0.1f);
        ChangeDefalutEffect();
    }


    private void ChangeHitEffect()
    {
        foreach(Renderer renderer in _renderers)
        {
            renderer.material = _hitMaterial;
        }
    }

    private void ChangeDefalutEffect()
    {
        foreach(Renderer renderer in _tempMaterialDic.Keys)
        {
            renderer.material = _tempMaterialDic[renderer];
        }
    }
}
