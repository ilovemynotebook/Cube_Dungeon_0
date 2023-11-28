using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum EStageType { ���ó�, �ٴٸ����, ����, ���α�, �ٴٸ����, ���ù� }
public enum EStageStyle { �����ǹ�,�Ϲݸ�,�������,�߰�������,�߰�������,����}

[Serializable]
public class Plane
{
    //��

    public EStageType planeType;//���� ����
    public EStageStyle planeStyle;//���� ����
    public GameObject prefab;//���� ������
    public Vector3 playerStartPoint;//�÷��̾� ������ġ
    public List<EnemyData> enemyData;//�� ����������
    public List<BoxData> boxData;//�ڽ� ����������
    

     
    public Plane Cloneing()
    {
        Plane plane = new Plane();
        plane.planeType = planeType;
        plane.planeStyle = planeStyle;
        plane.prefab = prefab;
        plane.playerStartPoint = playerStartPoint;
        plane.enemyData = enemyData.ToList();

        List<BoxData> boxList = new List<BoxData>();
        foreach(var data in boxData)
        {
            BoxData box = new BoxData();
            box = data.Cloneing();
            boxList.Add(box);
        }
        plane.boxData = boxList;
        return plane;
    }

    
}

