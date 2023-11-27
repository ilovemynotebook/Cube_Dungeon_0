
/// <summary> BehaviorTree�� �����ϴ� Ŭ����</summary>
public class BehaviorTree
{
    private INode _rootNode;

    public BehaviorTree(INode rootNode)
    {
        _rootNode = rootNode;
    }

    public void Operate() => _rootNode.Evaluate();
}
