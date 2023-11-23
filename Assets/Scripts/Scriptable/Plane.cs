using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EStageType { 도시낮, 바다멸망후, 우주, 소인국, 바다멸망전, 도시밤 }
public enum EStageStyle { 시작의방,일반몹,저장공간,중간보스하,중간보스상,보스}

[Serializable]
public class Plane
{
    //면

    public EStageType planeType;//면의 컨셉
    public EStageStyle planeStyle;//면의 역할
    public GameObject prefab;//맵의 프리팹
    public Vector3 playerStartPoint;//플레이어 생성위치
    public List<EnemyData> enemyData;//적 생성데이터
    public List<BoxData> boxData;//박스 생성데이터
    

     
    public Plane Clone()
    {
        Plane plane = new Plane();
        plane.planeType = planeType;
        plane.planeStyle = planeStyle;
        plane.prefab = prefab;
        plane.playerStartPoint = playerStartPoint;
        plane.enemyData = enemyData.ToList();
        plane.boxData= boxData.ToList();

        return plane;
    }

    
}

