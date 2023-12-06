using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.Numerics;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public GameStageDB[] StageDB;
    string filePathStage;
    string filePathPlayer;
    [SerializeField]Player player;
    PlaneSceneManager planeSceneManager;
    public SaveData saveData;
    public PlayerData playerData;
    public bool FileNotExist;
    
    private void Awake()
    {
        if (DataManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        filePathStage = Application.persistentDataPath + "/Stagedata";
        filePathPlayer= Application.persistentDataPath + "/Playerdata";

        if (!File.Exists(filePathStage) && !File.Exists(filePathStage)) { FileNotExist = true; }
        else
        {
            FileNotExist = false;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void DataSave()
    {
        player = GameManager.Instance.Player.GetComponent<Player>();
        StageDataGet(saveData);
        PlayerDataGet(player, playerData,CanvasManager.Instance);
        string Stagejsondata = JsonUtility.ToJson(saveData);
        string Playerjsondata = JsonUtility.ToJson(playerData);
        //string jsondataPlayer= JsonUtility.ToJson(savedata.playerData);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsondata);
        //string code = System.Convert.ToBase64String(bytes);
        File.WriteAllText(filePathStage, Stagejsondata);
        File.WriteAllText(filePathPlayer, Playerjsondata);
        Debug.Log(Stagejsondata);
        Debug.Log(Playerjsondata);
        //Debug.Log(jsondataPlayer);
    }

   
    public void DataLoad()
    {
        if (!File.Exists(filePathStage)&&!File.Exists(filePathStage)) { FileNotExist = true; return; }
        //string code = File.ReadAllText(FilePath);
        //byte[] bytes = Convert.FromBase64String(code);
        //string jsondata = System.Text.Encoding.UTF8.GetString(bytes);
        //player = GameManager.Instance._Player.GetComponent<Player>();
        string Stagejsondata = File.ReadAllText(filePathStage);
        string Playerjsondata = File.ReadAllText(filePathPlayer);
        saveData = JsonUtility.FromJson<SaveData>(Stagejsondata);
        playerData = JsonUtility.FromJson<PlayerData>(Playerjsondata);

    }

   public void DataCreate()
    {
        string Stagejsondata = JsonUtility.ToJson(saveData);
        string Playerjsondata = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePathStage, Stagejsondata);
        File.WriteAllText(filePathPlayer, Playerjsondata);
    }
    public void PlayerDataGet(Player player, PlayerData playerData,CanvasManager canvasManager)
    {
        //현재 플레이어 데이터를 데이터매니저에 담는다.
        playerData.SetPlayerData(player.hp, player.mhp, player.sta, player.msta);
        playerData.SetCanvasData(canvasManager.hpPotion, canvasManager.staPotion, canvasManager.dmgPotion, canvasManager.isUpgraded_weapon, canvasManager.isUpgraded_shield, canvasManager.isUpgraded_Item_0, canvasManager.isUpgraded_Item_1, canvasManager.isUpgraded_Item_2, canvasManager.isUpgraded_Item_3);
        
    }

    public void StageDataChange(int Stage)
    {
        //스테이지 DB에있는 데이터를 DataManager에 복사한다.
        for (int i = 0; i < StageDB[Stage-1].stages.CubePlanes.Length; i++)
        {
            saveData.planes[i]= StageDB[Stage-1].stages.CubePlanes[i].Cloneing();

        }
    }


    void StageDataGet(SaveData saveData)
    {
        planeSceneManager = GameManager.Instance._PlaneSceneManager;
        // savedata.playerData.SetPlayerData(player.hp, player.mhp, player.sta, player.msta, player.hpPotion, player.staPotion, player.dmgPotion);
        saveData.thisStage = planeSceneManager.thisStage;
        saveData.thisPlane = planeSceneManager.thisPlane;
        saveData.planes = planeSceneManager.planes.ToArray();
    }
    public void PlayerDataLoad(PlayerData playerData,Player player,CanvasManager canvasManager)
    {
        player.GetData(playerData);
        canvasManager.isUpgraded_weapon=playerData.isUpgraded_weapon;
        canvasManager.isUpgraded_shield=playerData.isUpgraded_shield;
        canvasManager.isUpgraded_Item_0 = playerData.isUpgraded_Item_0;
        canvasManager.isUpgraded_Item_1 = playerData.isUpgraded_Item_1;
        canvasManager.isUpgraded_Item_2 = playerData.isUpgraded_Item_2;
        canvasManager.isUpgraded_Item_3 = playerData.isUpgraded_Item_3;
        canvasManager.dmgPotion = playerData.dmgPotion;
        canvasManager.hpPotion = playerData.hpPotion;
        canvasManager.staPotion = playerData.staPotion;
    }
    public void StageDataLoad(SaveData saveData,PlaneSceneManager planemanager)
    {
        planemanager.planes = saveData.planes.ToArray();
        planemanager.thisStage = saveData.thisStage;
        planemanager.thisPlane = saveData.thisPlane;
    }

    public void NewCloneData()
    {
        saveData=new SaveData();
        playerData=new PlayerData();
    }

}
