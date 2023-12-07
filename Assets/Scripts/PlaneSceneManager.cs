using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlaneSceneManager : MonoBehaviour
{
    public static PlaneSceneManager Instance;

    [SerializeField] List<Box> boxes = new List<Box>();
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] public Plane[] planes;
    [SerializeField] Stage stage;
    public UnityEngine.UI.Image panel;
    public DroppedItem[] dropitems;
    //public SceneData sceneData;
    public GameObject MapPrefab;
    public GameObject PlayerPf;
    public GameObject Player;
    public int Monstercount;
    public int bosscount;
    public int thisStage; //현재 스테이지
    public int thisPlane; // 현재 면
    public CinemachineVirtualCamera VirtualCamera;
    Box deadbox;

    //EStageType ThisStageType;
    //EStageStyle ThisStageStyle;
    //[SerializeField]Enemy[] SummonedEnemy;
    //[SerializeField] Box[] SummonedBoxes;
    //public StageDatabase StageDatabase; //스테이지에대한 데이타베이스
    //public GameObject[] mapdatas;
    //public StageManager GameStage;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
           
        }
        else
        {
            Destroy(gameObject);
        }
        thisStage = 1; thisPlane = 1;
        planes = new Plane[4];
        
    }
    private void Start()
    {
        GameManager.Instance._PlaneSceneManager= this;
        DataManager.Instance.StageDataLoad(DataManager.Instance.saveData, this);
        Player = GameManager.Instance.Player;
    }
    private void Update()
    {
        Monstercount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        bosscount=GameObject.FindGameObjectsWithTag("Boss").Length;
    }

    public void SpawnPlayer()
    {
        Player = Instantiate(PlayerPf, Vector3.zero, PlayerPf.transform.rotation);
        //Player = GameManager.Instance._DataManager.PlayerDataLoad(GameManager.Instance._DataManager.playerData);
        //VirtualCamera = Instantiate(VirtualCamera);
        VirtualCamera.Follow = Player.transform;
        VirtualCamera.LookAt = Player.transform;
      
    }
    public void PlaneUp()
    {
        if (Monstercount == 0&&bosscount==0) {
            StageSave(thisPlane);
            if (planes.Length<= thisPlane)
            {   //Stage가 바뀔경우
                thisStage++;
                thisPlane = 1;
                DataManager.Instance.saveData.thisStage=thisStage;
                DataManager.Instance.StageDataChange(thisStage);
                Player player = FindObjectOfType<Player>();
                DataManager.Instance.PlayerDataGet(player, DataManager.Instance.playerData,CanvasManager.Instance);
                SceneManager.LoadScene("Stage"+thisStage);
            }
            else
            {
                thisPlane++;
                Clear();
                CreateMap();
                //다음 면 이동
            }
        }
    }
    public void PlaneDown()
    {
     
        if(Monstercount==0&&bosscount==0)
        {
            //이전 면 이동
            if ( thisPlane > 1)
            {
                //StageSave(thisPlane);
                Clear();
                thisPlane--;
                CreateMap();
            }
        }
    }

    void MonsterSpawn(Plane Plane)
    {
       // SummonedEnemy=new Enemy[Plane.enemies.Length];
        //for(int i = 0; i < Plane.enemies.Length; i++)
        //{
        //      enemies.Add( Instantiate(Plane.enemies[i], Plane.enemiesSpawnPlace[i], Quaternion.identity));
        //}
        for (int i = 0; i < Plane.enemyData.Count; i++)
        {
            enemies.Add(Instantiate(Plane.enemyData[i].enemy, Plane.enemyData[i].spawnPos, Quaternion.identity));
        }
    }

    void BoxSpawn(Plane Plane)
    {
      
        //for(int i = 0; i < Plane.boxes.Length; i++)
        //{
        //    boxes.Add(Instantiate(Plane.boxes[i], Plane.BoxSpawnPlace[i],Quaternion.identity));
        //    boxes[i].isOpen = Plane.boxesOpened[i];
        //}
        for(int i = 0; i < Plane.boxData.Count; i++)
        {
            boxes.Add(Instantiate(Plane.boxData[i].box, Plane.boxData[i].spawnPos,Quaternion.identity));
            boxes[i].isOpen = Plane.boxData[i].isOpen;
            boxes[i].hpPotion = Plane.boxData[i].hpPotion;
            boxes[i].staPotion = Plane.boxData[i].staPotion;
            boxes[i].dmgPotion = Plane.boxData[i].dmgPotion;
            boxes[i].key = Plane.boxData[i].key;
            boxes[i].isUpgraded_weapon= Plane.boxData[i].isUpgraded_weapon;
            boxes[i].isUpgraded_shield = Plane.boxData[i].isUpgraded_shield;
            boxes[i].isUpgraded_Item_0 = Plane.boxData[i].isUpgraded_Item_0;
            boxes[i].isUpgraded_Item_1 = Plane.boxData[i].isUpgraded_Item_1;
            boxes[i].isUpgraded_Item_2 = Plane.boxData[i].isUpgraded_Item_2;
            boxes[i].isUpgraded_Item_3 = Plane.boxData[i].isUpgraded_Item_3;

        }
        DataManager dataManager = DataManager.Instance;
        if (thisPlane == dataManager.DeadPlane&& dataManager.DeadPlane !=0)
        {
            dataManager.DeadPlane = 0;
            deadbox = Instantiate(dataManager.deadBox.box, dataManager.deadBox.spawnPos, Quaternion.identity);
            deadbox.setitem(dataManager.deadBox.hpPotion, dataManager.deadBox.staPotion, dataManager.deadBox.dmgPotion);
        }
    }

    public void CreateMap()
    {
       
        //StageSet(thisStage);
        Fade();
        //Clear();
        MapPrefab = Instantiate(planes[thisPlane - 1].prefab);
        //if (Player == null)
        //{
        //    SpawnPlayer();
        //}
        MonsterSpawn(planes[thisPlane - 1]);
        BoxSpawn(planes[thisPlane - 1]);
        Player.transform.position = planes[thisPlane - 1].playerStartPoint;

    }
    public void StageSet(Plane[] plane)
    {
        if(panel != null)
        {
            panel.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < plane.Length;i++)
        {
            planes[i] = plane[i].Cloneing();
           
        }
    }
    
    public void StageSave(int PlaneNumber)
    {
        for(int i = 0; i < boxes.Count; i++)
        {
            //planes[Plane].boxesOpened[i] = boxes[i].isOpen;
            planes[PlaneNumber - 1].boxData[i].isOpen = boxes[i].isOpen;
        }
       
    }
    
   public void Clear()
    {
        if(MapPrefab != null)
        {
            Destroy(MapPrefab);
        }
        if(boxes.Count> 0)
        {
            for (int i = 0; i < boxes.Count; i++)
            {
                Destroy(boxes[i].gameObject);

            }
            boxes.Clear();
        }
        if(enemies.Count > 0)
        {
            for (int j = 0; j < enemies.Count; j++)
            {
                if (enemies[j] != null)
                Destroy(enemies[j].gameObject);
            }
            enemies.Clear();
        }
        if (deadbox != null)
        {
            Destroy(deadbox.gameObject);
            deadbox = null;
        }
        if (dropitems != null)
        {
            dropitems = FindObjectsOfType<DroppedItem>();
            for(int k = 0; k < dropitems.Length; k++)
            {
                Destroy(dropitems[k].gameObject);
            }
            dropitems = null;


        }
        
    }
    public void Fade()
    {
        StartCoroutine(FadeFlow(0.2f));
    }

    IEnumerator FadeFlow(float F_time)
    {
        panel.gameObject.SetActive(true);
        float time = 0f;
        Color alpha = panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            panel.color = alpha;
            yield return null;
        }
        time = 0f;
        yield return new WaitForSeconds(0.2f);
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1,0, time);
            panel.color = alpha;
            yield return null;
        }
        panel.gameObject.SetActive(false);
        yield return null;
    }


}
