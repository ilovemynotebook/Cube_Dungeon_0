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

    public List<SceneData> SceneDatas;//�鿡���� ������������ �ִ� ����Ʈ

    public StageDatabase StageDatabase; //�������������� ����Ÿ���̽�

    public PlaneSceneManager PlaneSceneManager;

    public int thisStage; //���� ��������
    public int thisPlane; // ����  ��
    public EStageStyle thisStageStyle;//���� ���������� ����
    public EStageType thisStageType;//���� ���������� ����
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
        Stageset(1, 1);

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
        PlaneSceneManager = GameObject.Find("PlaneSceneManager").GetComponent<PlaneSceneManager>();
    }

    public void PlaneUp()
    {
        SceneDatas.Add(PlaneSceneManager.sceneData);
        if (StageDatabase.stages[thisStage-1].CubePlanes.Length <= thisPlane)
        {   //Stage�� �ٲ���
            Stageset(++thisStage, 1);
          
        }
        else
        {

            Stageset(thisStage,++thisPlane);
         
        }
    }

 
}
