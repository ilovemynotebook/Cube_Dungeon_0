using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStageType { ���ó�, �ٴٸ����, ����, ���α�, �ٴٸ����, ���ù� }
public enum EStageStyle { �����ǹ�,�Ϲݸ�,�������,�߰�������,�߰�������,����}
[Serializable]
public class Plane
{
    //��

    [SerializeField]
    public EStageType PlaneType;//���� ����
    [SerializeField]
    public EStageStyle PlaneStyle;//���� ����
}

