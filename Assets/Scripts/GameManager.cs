using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public EqupimentDataBase EDB;

    public PlaneSceneManager[] PlaneSceneManager;//면에대한 정보를가지고 있는 배열

    public StageDatabase StageDatabase; //스테이지에대한 데이타베이스

    public int thisStage; //현재 스테이지
    public EStageStyle thisStageStyle;//현재 스테이지의 컨셉
    public EStageType thisStageType;//현재 스테이지의 역할
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
