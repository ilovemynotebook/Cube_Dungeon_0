using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public EqupimentDataBase EDB;

    //public List<SceneData> SceneDatas;//면에대한 정보를가지고 있는 리스트

    //public  MapData  mapDatas;

    public GameObject[] mapdatas;

    public StageDatabase StageDatabase; //스테이지에대한 데이타베이스

   // public PlaneSceneManager PlaneSceneManager;

    public int thisStage; //현재 스테이지
    public int thisPlane; // 현재  면
    public int MaxPlane;
    //public EStageStyle thisStageStyle;//현재 스테이지의 컨셉
    //public EStageType thisStageType;//현재 스테이지의 역할
    void Awake()
    {
        mapdatas = new GameObject[9];

        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        thisStage = 1; MaxPlane = 1;thisPlane = 1;
    }

    private void Start()
    {

      
    }

    void Update()
    {
        
    }

   
 
}
