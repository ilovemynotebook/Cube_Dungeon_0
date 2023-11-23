using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageManager : MonoBehaviour
{

    public GameStageDB gameStageDB;


    private void Start()
    {
     
        PlaneSceneManager.Instance.StageSet(this);
        PlaneSceneManager.Instance.CreateMap();
    }
}
