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
    [SerializeField] private Button GoTitleButton;
    [SerializeField] private Button GoRePlayButton;


    private void Start()
    {
        dataManager = GameManager.Instance._DataManager;
        PlaneSceneManager.Instance.StageSet(dataManager.saveData.planes);
        PlaneSceneManager.Instance.CreateMap();
        GameoverPanel.gameObject.SetActive(false);
        //Player player = FindObjectOfType<Player>();
        //dataManager.PlayerDataLoad(dataManager.playerData,player);
    }
    

    void GameOver()
    {
        GameoverPanel.gameObject.SetActive(true);
        GoTitleButton.onClick.AddListener(GotoTitle);
        GoRePlayButton.onClick.AddListener(GotoReplay);
    }

    void GotoTitle()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    void GotoReplay()
    {
        Player player= GameManager.Instance.Player.GetComponent<Player>();
        dataManager.PlayerDataGet(player, dataManager.playerData);
        SceneManager.LoadScene("LoadingScene");
    }

}
