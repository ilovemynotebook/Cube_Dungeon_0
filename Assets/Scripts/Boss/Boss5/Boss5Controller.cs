using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss5Controller : BossController
{
    [Space(10)]
    [Header("5단계 보스의 고유 능력")]
    [SerializeField] private SkillPattern _teleportClass;
    public SkillPattern TeleportClass => _teleportClass;

    [Tooltip("플레이어가 몇 초동안 접근해있으면 텔레포트를 할까?")]
    [SerializeField] private float _targetAccessTime;

    [Tooltip("플레이어가 얼마나 접근하면 텔레포트를 할까?")]
    [SerializeField] private float _targetAccessDistance;

    private float _targetAccessTimer;
    private float _teleportTimer;
    private bool _canTeleport;

    protected override void Update()
    {
        base.Update();
        UpdateTeleport();
    }


    protected override void Init()
    {
        base.Init();
        _teleportClass.SkillState = BossState.CustomSkill;
        _ai = new Boss5AI(this);
    }


    private void UpdateTeleport()
    {
        if(0 < _teleportTimer)
            _teleportTimer += Time.deltaTime;

        float accessDistance = Vector3.Distance(transform.position, Target.transform.position);

        if (accessDistance < _targetAccessDistance)
        {
            _targetAccessTimer += Time.deltaTime;

            if (_targetAccessTime < _targetAccessTimer)
            {
                _canTeleport = true;
            }
        }
        else
        {
            _canTeleport = false;
            _targetAccessTimer = 0;
        }

        Debug.Log(_targetAccessTimer);
    }


    /// <summary> 현재 사용 가능한 스킬을 배치하는 함수 </summary>
    public override SkillPattern GetUsableSkill()
    {
        _usableSkillList.Clear();

        foreach (SkillPattern pattern in _skillPatterns)
        {
            if (0 < pattern.CurrentCoolTime)
            {
                Debug.Log("쿨타임 아직");
                continue;
            }


            if (pattern.Distance < TargetDistance)
            {
                Debug.Log("거리가 멈");
                continue;
            }

            _usableSkillList.Add(pattern);
        }

        if (_canTeleport)
        {

            _teleportTimer = 0;
            _canTeleport = false;
            return _teleportClass;
        }

        if (_usableSkillList.Count == 0)
            return default;

        int randInt = Random.Range(0, _usableSkillList.Count);

        return _usableSkillList[randInt];
    }
}
