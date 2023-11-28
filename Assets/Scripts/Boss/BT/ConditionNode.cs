using System;
using System.Diagnostics;
using Unity.VisualScripting;

/// <summary> ���ǹ��� Ȯ�� �� true�� ��� Success��ȯ false�ϰ�� Failure�� ��ȯ�ϴ� ���</summary>
public class ConditionNode : INode
{
    private Func<bool> _condition;

    public ConditionNode(Func<bool> condition)
    {
        _condition = condition;
    }

    public INode.ENodeState Evaluate()
    {         
        bool conditionResult = _condition.Invoke();
        return conditionResult ? INode.ENodeState.Success : INode.ENodeState.Failure;
    }
}
