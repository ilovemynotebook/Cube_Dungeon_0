using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject Player;

    public EqupimentDataBase EDB;

    public PlaneSceneManager _PlaneSceneManager;
    public bool inDialog;
    public GameObject SettingPanel;
    public GameObject TitleButton;
   // public DataManager _DataManager;





    // Start is called before the first frame update
    void Awake()
    {

        if(GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SettingPanel.SetActive(false);
        TitleButton.SetActive(false);
    }

    void Update()
    {
        
    }

    public void FindPlayer()
    {
        Player = GameObject.Find("Player").gameObject;
        Debug.Log(Player);
    }

    public void OnSettingPanel(bool ison)
    {
        SettingPanel.SetActive(ison);
    }
    public void OnTitleButton(bool ison)
    {
        TitleButton.SetActive(ison);
    }

    public void Pause(bool Gamepause)
    {
        if (Gamepause&&Time.timeScale!=0f)
        {
            Time.timeScale = 0f;
        }
        else if (!Gamepause&&Time.timeScale!=1f)
        {
            Time.timeScale = 1f;
        }
    }
    public void OnclickSettingButton()
    {
        if (!inDialog)
        {
            Pause(true);
            OnSettingPanel(true);
            OnTitleButton(true);
        }
   
    }
    public void GotoTitle()
    {
        DataManager.Instance.DeadPlane = 0;
        SceneManager.LoadScene("MainScene");
    }
    public void CloseButton()
    {
        Pause(false);
        OnSettingPanel(false);
        OnTitleButton(false);
    }

}
