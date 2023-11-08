using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public EqupimentDataBase EDB;

    public StageManager[] stageManagers;

    public int Stage;

    public int CubePlane;


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

    void Update()
    {
        
    }
}
