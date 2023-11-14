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
    public Enemy[] enemies;
    [SerializeField]
    public Vector3[] enemiesSpawnPlace;
    [SerializeField]
    public Box[] boxes;
    [SerializeField]
    public Vector3[] BoxSpawnPlace;
    [SerializeField]
    public bool[] boxesOpened;
     
    public Plane Clone()
    {
        Plane plane = new Plane();
        plane.PlaneType = PlaneType;
        plane.PlaneStyle = PlaneStyle;
        plane.Prefab = Prefab;
        plane.PlayerStartPoint = PlayerStartPoint;
        plane.enemies=enemies;
        plane.enemiesSpawnPlace = enemiesSpawnPlace;
        plane.boxes=boxes;
        plane.BoxSpawnPlace = BoxSpawnPlace;
        
        plane.boxesOpened = new bool[boxesOpened.Length];

        for(int i = 0, count = boxesOpened.Length; i < count; i++)
        {
            plane.boxesOpened[i] = boxesOpened[i];
        }

        return plane;
    }

    
}

