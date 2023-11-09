using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStageType { 도시낮, 바다멸망후, 우주, 소인국, 바다멸망전, 도시밤 }
public enum EStageStyle { 시작의방,일반몹,저장공간,중간보스하,중간보스상,보스}
[Serializable]
public class Plane
{
    //면

    [SerializeField]
    public EStageType PlaneType;//면의 컨셉
    [SerializeField]
    public EStageStyle PlaneStyle;//면의 역할
}

