using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageManager : MonoBehaviour
{
    DataManager dataManager;
    public GameStageDB gameStageDB;


    private void Start()
    {
        dataManager = GameManager.Instance._DataManager;
        PlaneSceneManager.Instance.StageSet(dataManager.saveData.planes);
        PlaneSceneManager.Instance.CreateMap();
    }
}
