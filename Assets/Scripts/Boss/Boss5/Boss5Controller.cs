using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss5Controller : BossController
{
    [Space(10)]
    [Header("5�ܰ� ������ ���� �ɷ�")]
    [SerializeField] private SkillPattern _teleportClass;
    public SkillPattern TeleportClass => _teleportClass;

    [Tooltip("�÷��̾ �� �ʵ��� ������������ �ڷ���Ʈ�� �ұ�?")]
    [SerializeField] private float _targetAccessTime;

    [Tooltip("�÷��̾ �󸶳� �����ϸ� �ڷ���Ʈ�� �ұ�?")]
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


    /// <summary> ���� ��� ������ ��ų�� ��ġ�ϴ� �Լ� </summary>
    public override SkillPattern GetUsableSkill()
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
