
public interface INode
{
    public enum ENodeState {Running, Success, Failure}

    /// <summary> ���� ��尡 � �������� ��ȯ </summary>
    public ENodeState Evaluate();
}
