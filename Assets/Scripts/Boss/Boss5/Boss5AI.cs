using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss5AI
{
    private BehaviorTree _tree;

    private Boss _boss;

    private SkillPattern _currentSkillPattern = new SkillPattern();



    public Boss5AI(Boss boss)
    {
        _boss = boss;
        _tree = new BehaviorTree(SettingBT());
    }


    public void Update()
    {
        StartBT();
    }

    private void StartBT()
    {
        _tree.Operate();
    }


    //BehaviorTree�� �� INode���� �����Ͽ� ��ȯ�ϴ� �Լ�
    private INode SettingBT()
    {
        List<INode> nodes = new List<INode>
        {
            new ActionNode(IsDie),
            SecondNode(),
            new ActionNode(Waiting)
        };

        return new SelectorNode(nodes);
    }


    //�ι�° ���
    private INode SecondNode()
    {
        List<INode> nodes = new List<INode>
        {
            AttackNode(),
            TrackingNode()
        };

        return new SelectorNode(nodes);
    }


    private INode.ENodeState IsDie()
    {
        Debug.Log("�׾��� ��Ҵ�");
        if(_boss.Hp <= 0)
            return INode.ENodeState.Success;

        Debug.Log("��Ҵ�");
        return INode.ENodeState.Failure;

    }



    private INode AttackNode()
    {
        List<INode> nodes = new List<INode>()
        {
            //��带 ������� �Է��Ѵ�.
            WaitingCheckNode(),
            new ActionNode(CheckAttackDistance),
            new ActionNode(StartAttack)
        };

        return new SequenceNode(nodes);
    }

    private INode WaitingCheckNode()
    {
        ConditionNode conditionNode = new ConditionNode(_boss.WaitingTimeCheck);
        return conditionNode;
    }


    //���� üũ ���
    private INode.ENodeState CheckAttackDistance()
    {
        _currentSkillPattern = _boss.GetUsableSkill();
        if (_currentSkillPattern != default && _currentSkillPattern != null)
            return INode.ENodeState.Success;

        return INode.ENodeState.Failure;
    }


    //���� �ൿ ���
    private INode.ENodeState StartAttack()
    {

        if (!(_boss.State >= BossState.Skill1 && _boss.State <= BossState.Skill4))
        {
            Debug.Log("����");
            _boss.State = _currentSkillPattern.SkillState;
            _currentSkillPattern.CurrentCoolTime = _currentSkillPattern.CoolTime;
            _boss.SetWaingTimer();
        }

        return INode.ENodeState.Running;
    }


    private INode TrackingNode()
    {
        List<INode> nodes = new List<INode>()
        {
            //��带 ������� �Է��Ѵ�.
            WaitingCheckNode(),
            new ActionNode(Tracking)
        };

        return new SequenceNode(nodes);
    }


    //���� �ൿ
    private INode.ENodeState Tracking()
    {
        if (Vector3.Distance(_boss.gameObject.transform.position, _boss.Target.transform.position) > 2)
        {
            Debug.Log("�i�ư���.");
            _boss.State = BossState.Tracking;
            return INode.ENodeState.Running;

        }
        return INode.ENodeState.Failure;
    }


    private INode.ENodeState Waiting()
    {
        
        if(_boss.State >= BossState.Skill1 && _boss.State <= BossState.Skill4)
        {
            Debug.Log("����� �Դϴ�.");
            _boss.State = BossState.Idle;
            return INode.ENodeState.Running;
        }
        Debug.Log("������ �Դϴ�.");
        return INode.ENodeState.Failure;

    }

}
