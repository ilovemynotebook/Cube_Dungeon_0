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

    //public List<SceneData> SceneDatas;//�鿡���� ������������ �ִ� ����Ʈ

    //public  MapData  mapDatas;

    public GameObject[] mapdatas;

    public StageDatabase StageDatabase; //�������������� ����Ÿ���̽�

   // public PlaneSceneManager PlaneSceneManager;

    public int thisStage; //���� ��������
    public int thisPlane; // ����  ��
    public int MaxPlane;
    //public EStageStyle thisStageStyle;//���� ���������� ����
    //public EStageType thisStageType;//���� ���������� ����
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
