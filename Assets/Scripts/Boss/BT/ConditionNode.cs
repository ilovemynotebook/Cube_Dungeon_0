using System;
using System.Diagnostics;
using Unity.VisualScripting;

/// <summary> 조건문을 확인 후 true일 경우 Success반환 false일경우 Failure을 반환하는 노드</summary>
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
