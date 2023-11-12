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

    public List<SceneData> SceneDatas;//�鿡���� ������������ �ִ� ����Ʈ

    public  MapData  mapDatas;

    public StageDatabase StageDatabase; //�������������� ����Ÿ���̽�

    public PlaneSceneManager PlaneSceneManager;

    public int thisStage; //���� ��������
    public int thisPlane; // ����  ��
    //public EStageStyle thisStageStyle;//���� ���������� ����
    //public EStageType thisStageType;//���� ���������� ����
    void Awake()
    {

        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        thisStage = 1;thisPlane = 1;
    }

    private void Start()
    {

      
    }

    void Update()
    {
        
    }

    public void PlaneUP()
    {
        PlaneSceneManager = FindAnyObjectByType<PlaneSceneManager>();

        
        if ((mapDatas.mapdata?.Length??0) > thisPlane)
        {
            Debug.Log(mapDatas.mapdata);
        }
        else
        {
          Debug.Log(StageDatabase.stages[thisStage - 1].CubePlanes[thisPlane - 1].Prefab);
          mapDatas.mapdata[thisPlane-1]= StageDatabase.stages[thisStage - 1].CubePlanes[thisPlane - 1].Prefab;
            // SceneDatas.Add(PlaneSceneManager.sceneData);

        }
        if (StageDatabase.stages[thisStage - 1].CubePlanes.Length <= thisPlane)
        {   //Stage�� �ٲ���
            thisStage++;
            thisPlane = 1;
        }
        else
        {
            thisPlane++;


        }

    }
    

 
}
