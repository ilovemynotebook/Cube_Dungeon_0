using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System.Diagnostics.Contracts;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject _Player;
    string FilePath;
    public EqupimentDataBase EDB;
    void Awake()
    {
        FilePath=Application.persistentDataPath+"/Playerdata";

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
        if (Input.GetKeyDown(KeyCode.O)) { DataSave(); }
        if (Input.GetKeyDown(KeyCode.P)) { DataLoad(); }
    }
    
    public void DataSave()
    {
        Player player = Instance._Player.GetComponent<Player>();
        PlayerData playerdata = new PlayerData();
        playerdata.hp = player.hp;
        playerdata.mhp = player.mhp;
        playerdata.sta = player.sta;
        playerdata.msta = player.msta;
        playerdata.isUpgraded_weapon = player.isUpgraded_weapon;
        playerdata.isUpgraded_shield = player.isUpgraded_shield;
        playerdata.isUpgraded_Item_0 = player.isUpgraded_Item_0;
        playerdata.isUpgraded_Item_1 = player.isUpgraded_Item_1;
        playerdata.isUpgraded_Item_2 = player.isUpgraded_Item_2;
        playerdata.isUpgraded_Item_3 = player.isUpgraded_Item_3;
        playerdata.dmgPotion = player.dmgPotion;
        playerdata.hpPotion = player.hpPotion;
        playerdata.staPotion = player.staPotion;
        playerdata.key = player.key;
        playerdata.thisStage = PlaneSceneManager.Instance.thisStage;
        playerdata.thisPlane = PlaneSceneManager.Instance.thisPlane;
        playerdata.Plane1boxesOpened = PlaneSceneManager.Instance.planes[0].boxesOpened;
        playerdata.Plane2boxesOpened = PlaneSceneManager.Instance.planes[1].boxesOpened;
        playerdata.Plane3boxesOpened = PlaneSceneManager.Instance.planes[2].boxesOpened;
        playerdata.Plane4boxesOpened = PlaneSceneManager.Instance.planes[3].boxesOpened;
        playerdata.Plane5boxesOpened = PlaneSceneManager.Instance.planes[4].boxesOpened;
        playerdata.Plane6boxesOpened = PlaneSceneManager.Instance.planes[5].boxesOpened;
        playerdata.Plane7boxesOpened = PlaneSceneManager.Instance.planes[6].boxesOpened;
        playerdata.Plane8boxesOpened = PlaneSceneManager.Instance.planes[7].boxesOpened;
        playerdata.Plane9boxesOpened = PlaneSceneManager.Instance.planes[8].boxesOpened;

        string jsondata = JsonUtility.ToJson(playerdata);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsondata);
        //string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(FilePath, jsondata);
        Debug.Log(jsondata);
    }

    public void DataLoad()
    {
        Player player = _Player.GetComponent<Player>();
        PlayerData playerdata = new PlayerData();
        if (!File.Exists(FilePath)) { ResetPlayer(); return; }

        //string code = File.ReadAllText(FilePath);
        //byte[] bytes = Convert.FromBase64String(code);
        //string jsondata = System.Text.Encoding.UTF8.GetString(bytes);
        string jsondata=File.ReadAllText(FilePath);
        playerdata=JsonUtility.FromJson<PlayerData>(jsondata);

        player.hp=playerdata.hp;
        player.mhp=playerdata.mhp;
        player.sta=playerdata.sta;
        player.msta=playerdata.msta;
        player.isUpgraded_weapon = playerdata.isUpgraded_weapon;
        player.isUpgraded_shield= playerdata.isUpgraded_shield;
        player.isUpgraded_Item_0 = playerdata.isUpgraded_Item_0;
        player.isUpgraded_Item_1 = playerdata.isUpgraded_Item_1;
        player.isUpgraded_Item_2= playerdata.isUpgraded_Item_2;
        player.isUpgraded_Item_3 = playerdata.isUpgraded_Item_3;
        player.dmgPotion= playerdata.dmgPotion;
        player.hpPotion= playerdata.hpPotion;
        player.staPotion= playerdata.staPotion;
        player.key= playerdata.key;
        PlaneSceneManager.Instance.thisStage = playerdata.thisStage;
        PlaneSceneManager.Instance.thisPlane = playerdata.thisPlane;
        SceneManager.LoadScene("Stage" + PlaneSceneManager.Instance.thisStage);
        PlaneSceneManager.Instance.StageSet();
        for (int i = 0; i < playerdata.Plane1boxesOpened.Length; i++)
        {
            PlaneSceneManager.Instance.planes[0].boxesOpened[i] = playerdata.Plane1boxesOpened[i];
        }
        for (int i =0; i < playerdata.Plane2boxesOpened.Length; i++)
        {
            PlaneSceneManager.Instance.planes[1].boxesOpened[i] = playerdata.Plane2boxesOpened[i];
        }
        for (int i = 0; i < playerdata.Plane3boxesOpened.Length; i++)
        {
            PlaneSceneManager.Instance.planes[2].boxesOpened[i] = playerdata.Plane3boxesOpened[i];
        }
        for (int i = 0; i < playerdata.Plane4boxesOpened.Length; i++)
        {
            PlaneSceneManager.Instance.planes[3].boxesOpened[i] = playerdata.Plane4boxesOpened[i];
        }
        for (int i = 0; i < playerdata.Plane5boxesOpened.Length; i++)
        {
            PlaneSceneManager.Instance.planes[4].boxesOpened[i] = playerdata.Plane5boxesOpened[i];
        }
        for (int i = 0; i < playerdata.Plane6boxesOpened.Length; i++)
        {
            PlaneSceneManager.Instance.planes[5].boxesOpened[i] = playerdata.Plane6boxesOpened[i];
        }
        for (int i = 0; i < playerdata.Plane7boxesOpened.Length; i++)
        {
            PlaneSceneManager.Instance.planes[6].boxesOpened[i] = playerdata.Plane7boxesOpened[i];
        }
        for (int i = 0; i < playerdata.Plane8boxesOpened.Length; i++)
        {
            PlaneSceneManager.Instance.planes[7].boxesOpened[i] = playerdata.Plane8boxesOpened[i];
        }
        for (int i = 0; i < playerdata.Plane9boxesOpened.Length; i++)
        {
            PlaneSceneManager.Instance.planes[8].boxesOpened[i] = playerdata.Plane9boxesOpened[i];
        }
        PlaneSceneManager.Instance.CreateMap();
        Debug.Log(jsondata);
    }

    void ResetPlayer()
    {
        DataSave();
        DataLoad();

    }
}
