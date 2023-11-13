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
    [SerializeField]
    public GameObject Prefab;
    [SerializeField]
    public Vector3 PlayerStartPoint;
    [SerializeField]
    public bool arrvied=false;
    [SerializeField]
    public Enemy[] enemies;
    [SerializeField]
    public Vector3[] enemiesSpawnPlace;
    [SerializeField]
    public bool[] enemiesDied;
    [SerializeField]
    public Box[] boxes;
    [SerializeField]
    public Vector3[] BoxSpawnPlace;
    [SerializeField]
    public bool[] boxesOpened;
 
}

