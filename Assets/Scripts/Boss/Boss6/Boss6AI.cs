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




    //BehaviorTree에 들어갈 INode들을 설정하여 반환하는 함수
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


    //두번째 노드
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
        Debug.Log("죽었니 살았니");
        if(_boss6.Hp <= 0)
            return INode.ENodeState.Success;

        Debug.Log("살았다");
        return INode.ENodeState.Failure;

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


    private INode WaitingCheckNode(INode node)
    {
        ConditionNode conditionNode = new ConditionNode(_boss.WaitingTimeCheck, node);
        return conditionNode;
    }


    //공격 체크 노드
    private INode.ENodeState CheckAttackDistance()
    {
        _currentSkillPattern = _boss6.GetUsableSkill();
        if (_currentSkillPattern != default && _currentSkillPattern != null)
            return INode.ENodeState.Success;

        return INode.ENodeState.Failure;
    }


    //공격 행동 노드
    private INode.ENodeState StartAttack()
    {

        if (!(_boss6.State >= BossState.Skill1 && _boss6.State <= BossState.CustomSkill))
        {
            Debug.Log("공격");
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
            //노드를 순서대로 입력한다.
            WaitingCheckNode(new ActionNode(Tracking))
            
        };

        return new SequenceNode(nodes);
    }


    //추적 행동
    private INode.ENodeState Tracking()
    {
        if (Vector3.Distance(_boss6.gameObject.transform.position, _boss6.Target.transform.position) > 2)
        {
            Debug.Log("쫒아간다.");
            _boss6.State = BossState.Tracking;
            return INode.ENodeState.Running;

        }
        return INode.ENodeState.Failure;
    }


    private INode.ENodeState Waiting()
    {
        
        if(_boss6.State >= BossState.Skill1 && _boss6.State <= BossState.CustomSkill)
        {
            Debug.Log("대기중 입니다.");
            _boss6.State = BossState.Idle;
            return INode.ENodeState.Running;
        }
        Debug.Log("공격중 입니다.");
        return INode.ENodeState.Failure;

    }

}
