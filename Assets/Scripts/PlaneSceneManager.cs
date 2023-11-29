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
    public UnityEngine.UI.Image panel;
    [SerializeField] List<Box> boxes = new List<Box>();
    [SerializeField] List<Enemy> enemies = new List<Enemy>();
    [SerializeField] public Plane[] planes;
    [SerializeField] Stage stage;
    //public SceneData sceneData;
    public GameObject MapPrefab;
    public GameObject PlayerPf;
    public GameObject Player;
    public int Monstercount;
    public int thisStage; //���� ��������
    public int thisPlane; // ���� ��
    public CinemachineVirtualCamera VirtualCamera;
    
    //EStageType ThisStageType;
    //EStageStyle ThisStageStyle;
    //[SerializeField]Enemy[] SummonedEnemy;
    //[SerializeField] Box[] SummonedBoxes;
    //public StageDatabase StageDatabase; //�������������� ����Ÿ���̽�
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
        GameManager.Instance._DataManager.StageDataLoad(GameManager.Instance._DataManager.saveData, this);
        Player = GameManager.Instance.Player;
    }
    private void Update()
    {
        Monstercount = GameObject.FindGameObjectsWithTag("Enemy").Length;
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
        if (Monstercount == 0) {
            StageSave(thisPlane);
            if (planes.Length<= thisPlane)
            {   //Stage�� �ٲ���
                thisStage++;
                thisPlane = 1;
                GameManager.Instance._DataManager.saveData.thisStage=thisStage;
                GameManager.Instance._DataManager.StageDataChange(thisStage);
                Player player = FindObjectOfType<Player>();
                GameManager.Instance._DataManager.PlayerDataGet(player, GameManager.Instance._DataManager.playerData);
                SceneManager.LoadScene("Stage"+thisStage);
            }
            else
            {
                thisPlane++;
                Clear();
                CreateMap();
                //���� �� �̵�
            }
        }
    }
    public void PlaneDown()
    {
     
        if(Monstercount==0)
        {
            //���� �� �̵�
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