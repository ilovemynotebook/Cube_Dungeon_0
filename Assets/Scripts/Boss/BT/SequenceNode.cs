using System.Collections.Generic;


/// <summary> �ڽ� ��带 ������� �����ϸ鼭 Failure ���°� ���� ������ �����ϴ� ��� </summary>
public class SequenceNode : INode
{
    private List<INode> _childs;

    public SequenceNode(List<INode> childs)
    {
        _childs = childs;
    }

    public INode.ENodeState Evaluate()
    {

        if (_childs == null || _childs.Count == 0)
            return INode.ENodeState.Failure;

        foreach (INode child in _childs)
        {
            switch (child.Evaluate())
            {
                case INode.ENodeState.Running:
                    return INode.ENodeState.Running;

                //�ڽ� ����: Success �� ��->���� �ڽ����� �̵�
                case INode.ENodeState.Success:
                    continue;

                case INode.ENodeState.Failure:
                    return INode.ENodeState.Failure;
            }
        }

        return INode.ENodeState.Failure;
    }

}
