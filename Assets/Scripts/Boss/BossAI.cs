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


    //BehaviorTree에 들어갈 INode들을 설정하여 반환하는 함수
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
            //노드를 순서대로 입력한다.
            new ActionNode(CheckAttackDistance),
            new ActionNode(StartAttack)
        };

        return new SequenceNode(nodes);
    }


    //공격 체크 노드
    private INode.ENodeState CheckAttackDistance()
    {
        _currentSkillPattern = _boss.GetUsableSkill();

        if (_currentSkillPattern != default || _currentSkillPattern != null)
            return INode.ENodeState.Success;

        return INode.ENodeState.Failure;
    }


    //공격 행동 노드
    private INode.ENodeState StartAttack()
    {
        Debug.Log("공격");
        _boss.State = BossState.Attack;
        _currentSkillPattern.CurrentCoolTime = _currentSkillPattern.CoolTime;
        return INode.ENodeState.Success;
    }


    private INode TrackingNode()
    {
        List<INode> nodes = new List<INode>()
        {
            //노드를 순서대로 입력한다.
            new ActionNode(Tracking)
        };

        return new SequenceNode(nodes);
    }

    //추적 행동
    private INode.ENodeState Tracking()
    {
        Debug.Log("추적중");
        if (true)
        {
            _boss.State = BossState.Tracking;
            return INode.ENodeState.Success;

        }
        return INode.ENodeState.Failure;
    }

}
