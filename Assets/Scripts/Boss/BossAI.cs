using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossAI
{
    private BehaviorTree _tree;

    private Boss _boss;

    private SkillPattern _currentSkillPattern = new SkillPattern();



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
            SecondNode(),
            new ActionNode(Waiting)
        };

        return new SelectorNode(nodes);
    }


    //두번째 노드
    private INode SecondNode()
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


    //공격 체크 노드
    private INode.ENodeState CheckAttackDistance()
    {
        _currentSkillPattern = _boss.GetUsableSkill();
        if (_currentSkillPattern != default && _currentSkillPattern != null)
            return INode.ENodeState.Success;

        return INode.ENodeState.Failure;
    }


    //공격 행동 노드
    private INode.ENodeState StartAttack()
    {
        Debug.Log("공격");
        _boss.State = _currentSkillPattern.SkillState;
        _currentSkillPattern.CurrentCoolTime = _currentSkillPattern.CoolTime;
        _boss.SetWaingTimer();
        return INode.ENodeState.Running;
    }


    private INode TrackingNode()
    {
        List<INode> nodes = new List<INode>()
        {
            //노드를 순서대로 입력한다.
            WaitingCheckNode(),
            new ActionNode(Tracking)
        };

        return new SequenceNode(nodes);
    }


    //추적 행동
    private INode.ENodeState Tracking()
    {
        if (Vector3.Distance(_boss.gameObject.transform.position, _boss.Target.transform.position) > 2)
        {
            Debug.Log("쫒아간다.");
            _boss.State = BossState.Tracking;
            return INode.ENodeState.Running;

        }
        return INode.ENodeState.Failure;
    }


    private INode.ENodeState Waiting()
    {
        Debug.Log("대기중 입니다.");
        _boss.State = BossState.Idle;
        return INode.ENodeState.Running;
    }

}
