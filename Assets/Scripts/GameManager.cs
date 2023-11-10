using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public EqupimentDataBase EDB;

    public List<PlaneSceneManager> planeSceneManager;//면에대한 정보를가지고 있는 배열

    public StageDatabase StageDatabase; //스테이지에대한 데이타베이스

    public int thisStage; //현재 스테이지
    public int thisPlane; // 현재  면
    public EStageStyle thisStageStyle;//현재 스테이지의 컨셉
    public EStageType thisStageType;//현재 스테이지의 역할
    void Awake()
    {
        

        
    }

    private void Start()
    {
        if(GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Stageset(1,1);
        Player = GameObject.Find("Player").gameObject;
    }

    void Update()
    {
        
    }


    void Stageset(int Stage,int Plane)
    {
        thisStage = Stage;
        thisPlane = Plane;
        thisStageStyle = StageDatabase.stages[thisStage - 1].CubePlanes[thisPlane - 1].PlaneStyle;
        thisStageType = StageDatabase.stages[thisStage - 1].CubePlanes[thisPlane - 1].PlaneType;
        
    }
    public void PlaneUp()
    {
        if (StageDatabase.stages[thisStage-1].CubePlanes.Length < thisPlane)
        {   //Stage가 바뀔경우

        }
        else
        {
            planeSceneManager.Add(GameObject.Find("PlaneSceneManager").GetComponent<PlaneSceneManager>());
            Stageset(thisStage, thisPlane);
        }
    }

 
}
