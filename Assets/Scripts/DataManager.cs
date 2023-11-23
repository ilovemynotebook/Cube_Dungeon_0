using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class DataManager : MonoBehaviour
{
    string FilePath;
    Player player;
    PlaneSceneManager planeSceneManager;
    // Start is called before the first frame update
    void Start()
    {
        FilePath = Application.persistentDataPath + "/Playerdata";
        player = GameManager.Instance._Player.GetComponent<Player>();
        planeSceneManager=GameManager.Instance._PlaneSceneManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DataSave()
    {
        SaveData savedata = new SaveData();
        savedata = PlayerDataGet(player);
        string jsondata = JsonUtility.ToJson(savedata);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsondata);
        //string code = System.Convert.ToBase64String(bytes);
        File.WriteAllText(FilePath, jsondata);
        Debug.Log(jsondata);
    }

    public void DataLoad()
    {
        SaveData saveData = new SaveData();
        if (!File.Exists(FilePath)) { ResetPlayer(); return; }
        //string code = File.ReadAllText(FilePath);
        //byte[] bytes = Convert.FromBase64String(code);
        //string jsondata = System.Text.Encoding.UTF8.GetString(bytes);
        string jsondata = File.ReadAllText(FilePath);
        saveData = JsonUtility.FromJson<SaveData>(jsondata);
        PlayerDataLoad(saveData, player);
        SceneManager.LoadScene("Stage" + planeSceneManager.thisStage);
        planeSceneManager.CreateMap();
        Debug.Log(jsondata);
    }

    void ResetPlayer()
    {
        DataSave();
        DataLoad();
    }

    SaveData PlayerDataGet(Player player)
    {
        SaveData savedata = new SaveData();
        savedata.playerData.SetPlayerData(player.hp, player.mhp, player.sta, player.msta, player.hpPotion, player.staPotion, player.dmgPotion);
        savedata.thisStage = planeSceneManager.thisStage;
        savedata.thisPlane = planeSceneManager.thisPlane;
        savedata.planes = planeSceneManager.planes.ToArray();
        return savedata;
    }
    void PlayerDataLoad(SaveData saveData,Player player)
    {
        player.GetData(saveData.playerData);
        planeSceneManager.thisStage = saveData.thisStage;
        planeSceneManager.thisPlane = saveData.thisPlane;

    }

}
