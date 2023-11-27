using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;





public class BossAI
{
    private BehaviorTree _tree;

    private Boss _boss;

    private SkillPattern _currentSkillPattern;

    public BossAI(Boss boss)
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
            AttackNode(),
            TrackingNode()
        };

        return new SelectorNode(nodes);
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


    //���� üũ ���
    private INode.ENodeState CheckAttackDistance()
    {
        _currentSkillPattern = _boss.GetUsableSkill();

        if (_currentSkillPattern != default || _currentSkillPattern != null)
            return INode.ENodeState.Success;

        return INode.ENodeState.Failure;
    }


    //���� �ൿ ���
    private INode.ENodeState StartAttack()
    {
        Debug.Log("����");
        _boss.State = BossState.Attack;
        _currentSkillPattern.CurrentCoolTime = _currentSkillPattern.CoolTime;
        return INode.ENodeState.Success;
    }


    private INode TrackingNode()
    {
        List<INode> nodes = new List<INode>()
        {
            //��带 ������� �Է��Ѵ�.
            new ActionNode(Tracking)
        };

        return new SequenceNode(nodes);
    }

    //���� �ൿ
    private INode.ENodeState Tracking()
    {
        Debug.Log("������");
        if (true)
        {
            _boss.State = BossState.Tracking;
            return INode.ENodeState.Success;

        }
        return INode.ENodeState.Failure;
    }

}
