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
        dataManager = DataManager.Instance;
        PlaneSceneManager.Instance.StageSet(dataManager.saveData.planes);
        PlaneSceneManager.Instance.CreateMap();
        GameoverPanel.gameObject.SetActive(false);
        //Player player = FindObjectOfType<Player>();
        //dataManager.PlayerDataLoad(dataManager.playerData,player);
    }
    

    public void GameOver()
    {
        //GameManager.Instance.Player.SetActive(false);
        GameoverPanel.gameObject.SetActive(true);
        GoTitleButton.onClick.AddListener(GotoTitle);
        GoRePlayButton.onClick.AddListener(GotoReplay);
    }

    void GotoTitle()
    {
        DataManager.Instance.DeadPlane = 0;
        SceneManager.LoadScene("MainScene");
    }
    
    void GotoReplay()
    {
        Player player= GameManager.Instance.Player.GetComponent<Player>();
        player.hp = player.mhp;
        CanvasManager.Instance.UpdateHud();
        dataManager.PlayerDataGet(player, dataManager.playerData,CanvasManager.Instance);
        dataManager.StageDataLoad(dataManager.saveData, PlaneSceneManager.Instance);
        PlaneSceneManager.Instance.Clear();
        Destroy(Playable.instance.gameObject);
        SceneManager.LoadScene("LoadingScene");
    }

}
