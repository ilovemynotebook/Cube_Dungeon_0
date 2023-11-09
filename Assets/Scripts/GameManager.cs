using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public EqupimentDataBase EDB;

    public PlaneSceneManager[] PlaneSceneManager;//�鿡���� ������������ �ִ� �迭

    public StageDatabase StageDatabase; //�������������� ����Ÿ���̽�

    public int thisStage; //���� ��������
    public EStageStyle thisStageStyle;//���� ���������� ����
    public EStageType thisStageType;//���� ���������� ����
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.Find("Player").gameObject;

        if(GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    void Update()
    {
        
    }


    void StageReset()
    {
        thisStage = 1;
        //thisStageStyle = StageDatabase.stages[thisStage-1].;
        
    }

 
}
