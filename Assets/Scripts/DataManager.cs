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
    [SerializeField]Player player;
    PlaneSceneManager planeSceneManager;
    // Start is called before the first frame update
    void Start()
    {
        FilePath = Application.persistentDataPath + "/Playerdata";
        planeSceneManager=GameManager.Instance._PlaneSceneManager;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            DataSave();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            DataLoad();
        }
        if (player == null)
        {
            player = GameManager.Instance._Player.GetComponent<Player>();
        }

    }


    public void DataSave()
    {
        SaveData savedata = new SaveData();
        savedata = DataGet(player);
        string jsondata = JsonUtility.ToJson(savedata);
        //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsondata);
        //string code = System.Convert.ToBase64String(bytes);
        File.WriteAllText(FilePath, jsondata);
        Debug.Log(jsondata);
    }

    public void DataLoad()
    {
        //if (!File.Exists(FilePath)) { ResetPlayer(); return; }
        //string code = File.ReadAllText(FilePath);
        //byte[] bytes = Convert.FromBase64String(code);
        //string jsondata = System.Text.Encoding.UTF8.GetString(bytes);
        player = GameManager.Instance._Player.GetComponent<Player>();
        string jsondata = File.ReadAllText(FilePath);
        SaveData saveData = new SaveData();
        saveData = JsonUtility.FromJson<SaveData>(jsondata);
        PlayerDataLoad(saveData, player);
        PlaneDataLoad(saveData, planeSceneManager);
        SceneManager.LoadScene("Stage" + planeSceneManager.thisStage);
    }

    void ResetPlayer()
    {
        DataSave();
        DataLoad();
    }

    SaveData DataGet(Player player)
    {
        SaveData savedata = new SaveData();
        savedata.playerData = new PlayerData();
        savedata.playerData.SetPlayerData(player.hp, player.mhp, player.sta, player.msta, player.hpPotion, player.staPotion, player.dmgPotion);
        savedata.thisStage = planeSceneManager.thisStage;
        savedata.thisPlane = planeSceneManager.thisPlane;
        savedata.planes = planeSceneManager.planes.ToArray();
        return savedata;
    }
    void PlayerDataLoad(SaveData saveData,Player player)
    {
        player.GetData(saveData.playerData);
    }
    void PlaneDataLoad(SaveData saveData,PlaneSceneManager planemanager)
    {
        planemanager.planes = saveData.planes.ToArray();
        planemanager.thisStage = saveData.thisStage;
        planemanager.thisPlane = saveData.thisPlane;
    }

}
