using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Diagnostics.Contracts;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject _Player;
    public PlaneSceneManager _PlaneSceneManager;
    public EqupimentDataBase EDB;
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
    }

    private void Start()
    {
    }

    void Update()
    {
   
    }
    
}
