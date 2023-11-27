using System;
using System.Diagnostics;
using Unity.VisualScripting;

/// <summary> ���ǹ��� Ȯ�� �� true�� ��� ���� Node���� false�ϰ�� Failure�� ��ȯ�ϴ� ���</summary>
public class ConditionNode : INode
{
    private INode _child;
    private Func<bool> _condition;

    public ConditionNode(INode child, Func<bool> condition)
    {
        _child = child;
        _condition = condition;
    }

    public INode.ENodeState Evaluate()
    {
        if(_condition == null)
            return INode.ENodeState.Failure;

        if (_condition())
        {
            return _child.Evaluate();
        }

        return INode.ENodeState.Failure;
    }
}
