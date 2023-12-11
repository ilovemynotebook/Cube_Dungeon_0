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
    [Tooltip("스킬 이름")]
    [SerializeField] private string _name;
    public string Name => _name;

    [Tooltip("최대 거리")]
    [SerializeField] private float _distance;
    public float Distance => _distance;

    [Tooltip("쿨타임")]
    [SerializeField] private float _coolTime;
    public float CoolTime => _coolTime;

    [Tooltip("설정한 스킬 클래스를 넣는 곳")]
    public BossAttackBehaviour AttackTrigger;

    [HideInInspector] public float CurrentCoolTime;

    [HideInInspector] public BossState SkillState;
}


[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class BossController : MonoBehaviour
{
    [Header("컴포넌트")]
    [SerializeField] protected AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;

    [SerializeField] protected UIBoss _uiBoss;
    protected UIBoss _createUiBoss;


    [Space(10)]

    [Header("능력이")]

    [Tooltip("이름")]
    [SerializeField] protected string _name;
    public string Name => _name;

    [Tooltip("최대 체력")]
    [SerializeField] protected int _maxHp;

    [Tooltip("이동 속도")]
    [SerializeField] protected float _speed;
    public float Speed => _speed;

    [Tooltip("애니메이션 전체 스피드 배율")]
    [SerializeField] protected float _animeSpeed;
    public float AnimeSpeed => _animeSpeed;

    [Tooltip("공격력")]
    [SerializeField] protected float _power;
    public float Power => _power;

//===============================================================================================

    [Space(10)]
    [Header("AI")]

    [Tooltip("스킬 데이터들")]
    [SerializeField] protected SkillPattern[] _skillPatterns;
    public SkillPattern[] SkillPatterns => _skillPatterns;

    [Tooltip("AI 패턴 갱신 시간")]
    [SerializeField] protected float _patternUpdateTime;

    [Tooltip("공격후 대기시간")]
    [SerializeField] protected float _waitTime;
    protected float _waitTimer;

    protected List<SkillPattern> _usableSkillList = new List<SkillPattern>();

    protected BossAI _ai;

    protected BossStateMachineBehaviour[] _bossStateMachines;

    protected Animator _animator;

    protected List<Renderer> _renderers = new List<Renderer>();

    protected Dictionary<Renderer, Material> _tempMaterialDic = new Dictionary<Renderer, Material>();

    protected Material _hitMaterial;

    protected Coroutine _hitEffectRoutine;

    protected float _hp;

    [HideInInspector] public Rigidbody Rigidbody;

    public BossState State;

    public GameObject Target;

    public event Action<float, float> OnGetHitHandler;
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
        Target = Target == null ? FindObjectOfType<Player>().gameObject : Target;

        _createUiBoss = Instantiate(_uiBoss);
        _createUiBoss.Init(this, _name);

    }

    protected virtual void OnDisable()
    {
        Destroy(_createUiBoss.gameObject);
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
            _skillPatterns[i].AttackTrigger.Init(this);
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
        if (0 < _hp)
        {
            _hp -= dmg;
            _hp = Mathf.Clamp(_hp, 0, _maxHp);

            if (_hitEffectRoutine != null)
                StopCoroutine(_hitEffectRoutine);
            _hitEffectRoutine = StartCoroutine(StartHitEffect());
        }
            
        else
        {
            State = BossState.Die;
            Destroy(gameObject, 10);
            OnDeathEventHandler?.Invoke();
        }
        OnGetHitHandler?.Invoke(_maxHp, _hp);

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
                continue;
            }
                

            if (pattern.Distance < TargetDistance)
            {
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

    public void AddWaitTimer(float value)
    {
        _waitTimer += value;
    }


    private IEnumerator StartHitEffect()
    {
        ChangeHitEffect();
        yield return new WaitForSeconds(0.05f);
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
