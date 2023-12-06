using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Boss6AI : BossAI
{
    private Boss6Controller _boss6;

    public Boss6AI(Boss6Controller boss) : base(boss)
    {
        _boss6 = boss;
    }




    //BehaviorTree�� �� INode���� �����Ͽ� ��ȯ�ϴ� �Լ�
    protected override INode SettingBT()
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
            WaitingCheckNode(AttackNode()),
            TrackingNode()
        };

        return new SelectorNode(nodes);
    }


    private INode.ENodeState IsDie()
    {
        Debug.Log("�׾��� ��Ҵ�");
        if(_boss6.Hp <= 0)
            return INode.ENodeState.Success;

        Debug.Log("��Ҵ�");
        return INode.ENodeState.Failure;

    }



    private INode AttackNode()
    {
        List<INode> nodes = new List<INode>()
        {
            //��带 ������� �Է��Ѵ�.
            new ActionNode(CheckAttackDistance),
            new ActionNode(StartAttack)
        };

        return new SequenceNode(nodes);
    }


    private INode WaitingCheckNode(INode node)
    {
        ConditionNode conditionNode = new ConditionNode(_boss.WaitingTimeCheck, node);
        return conditionNode;
    }


    //���� üũ ���
    private INode.ENodeState CheckAttackDistance()
    {
        _currentSkillPattern = _boss6.GetUsableSkill();
        if (_currentSkillPattern != default && _currentSkillPattern != null)
            return INode.ENodeState.Success;

        return INode.ENodeState.Failure;
    }


    //���� �ൿ ���
    private INode.ENodeState StartAttack()
    {

        if (!(_boss6.State >= BossState.Skill1 && _boss6.State <= BossState.CustomSkill))
        {
            Debug.Log("����");
            _boss6.State = _currentSkillPattern.SkillState;
            _currentSkillPattern.CurrentCoolTime = _currentSkillPattern.CoolTime;
            _boss6.SetWaingTimer();
        }

        return INode.ENodeState.Running;
    }


    private INode TrackingNode()
    {
        List<INode> nodes = new List<INode>()
        {
            //��带 ������� �Է��Ѵ�.
            WaitingCheckNode(new ActionNode(Tracking))
            
        };

        return new SequenceNode(nodes);
    }


    //���� �ൿ
    private INode.ENodeState Tracking()
    {
        if (Vector3.Distance(_boss6.gameObject.transform.position, _boss6.Target.transform.position) > 2)
        {
            Debug.Log("�i�ư���.");
            _boss6.State = BossState.Tracking;
            return INode.ENodeState.Running;

        }
        return INode.ENodeState.Failure;
    }


    private INode.ENodeState Waiting()
    {
        
        if(_boss6.State >= BossState.Skill1 && _boss6.State <= BossState.CustomSkill)
        {
            Debug.Log("����� �Դϴ�.");
            _boss6.State = BossState.Idle;
            return INode.ENodeState.Running;
        }
        Debug.Log("������ �Դϴ�.");
        return INode.ENodeState.Failure;

    }

}
