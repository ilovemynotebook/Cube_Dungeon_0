using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;


/// <summary> ���ǹ��� Ȯ�� �� true�� ��� Success��ȯ false�ϰ�� Failure�� ��ȯ�ϴ� ���</summary>
public class ConditionNode : INode
{
    private INode _node;

    private Func<bool> _condition;

    public ConditionNode(Func<bool> condition, INode node)
    {
        _condition = condition;
        _node = node;
    }


    public INode.ENodeState Evaluate()
    {         
        bool conditionResult = _condition.Invoke();
        return conditionResult ? _node.Evaluate() : INode.ENodeState.Failure;
    }
}
