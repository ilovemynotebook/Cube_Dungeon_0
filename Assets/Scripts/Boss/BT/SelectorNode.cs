using System.Collections.Generic;


/// <summary> �ڽ� ��忡�� ó������ Success �� Running ���¸� ���� ��尡 �߻��ϸ� �� ������ �����ϰ� ���ߴ� ��� </summary>
public class SelectorNode : INode
{
    private List<INode> _childs;

    public SelectorNode(List<INode> childs)
    {
        _childs = childs;
    }

    public INode.ENodeState Evaluate()
    {
        if (_childs == null)
            return INode.ENodeState.Failure;


        foreach (INode child in _childs)
        {
            switch (child.Evaluate())
            {
                //�ڽ� ����: Running�� �� -> Running ��ȯ
                case INode.ENodeState.Running:
                    return INode.ENodeState.Running;

                //�ڽ� ����: Success �� ��->Success ��ȯ
                case INode.ENodeState.Success:
                    return INode.ENodeState.Success;

                    //�ڽ� ����: Failure�� �� -> ���� �ڽ����� �̵�
            }
        }

        return INode.ENodeState.Failure;
    }
}
