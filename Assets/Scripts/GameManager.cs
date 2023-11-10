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

    public List<PlaneSceneManager> planeSceneManager;//�鿡���� ������������ �ִ� �迭

    public StageDatabase StageDatabase; //�������������� ����Ÿ���̽�

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
        {   //Stage�� �ٲ���

        }
        else
        {
            planeSceneManager.Add(GameObject.Find("PlaneSceneManager").GetComponent<PlaneSceneManager>());
            Stageset(thisStage, thisPlane);
        }
    }

 
}
