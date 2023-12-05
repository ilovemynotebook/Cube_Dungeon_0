using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageManager : MonoBehaviour
{
    DataManager dataManager;
    public GameStageDB gameStageDB;
    [SerializeField] private Canvas GameoverPanel;


    private void Start()
    {
        dataManager = GameManager.Instance._DataManager;
        PlaneSceneManager.Instance.StageSet(dataManager.saveData.planes);
        PlaneSceneManager.Instance.CreateMap();
        GameoverPanel.gameObject.SetActive(false);
        //Player player = FindObjectOfType<Player>();
        //dataManager.PlayerDataLoad(dataManager.playerData,player);
    }
    private void OnEnable()
    {
        
    }
}
