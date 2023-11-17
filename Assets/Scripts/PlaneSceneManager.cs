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
    //public SceneData sceneData;
    public TMP_Text StageText;
    public TMP_Text StageTypeText;
    public TMP_Text StageStyleText;
    public TMP_Text MonsterCountText;
    public TMP_Text PlayerText;
    public GameObject MapPrefab;

    public int Monstercount;
    public int thisStage; //현재 스테이지
    public int thisPlane; // 현재  면


   // EStageType ThisStageType;
    //EStageStyle ThisStageStyle;
   // [SerializeField]Enemy[] SummonedEnemy;
   // [SerializeField] Box[] SummonedBoxes;
    public StageDatabase StageDatabase; //스테이지에대한 데이타베이스
    //public GameObject[] mapdatas;

    public Player Player;

    private void Awake()
    {
        if (PlaneSceneManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        thisStage = 1; thisPlane = 1;
        planes = new Plane[9];

      
       // mapdatas = new GameObject[9];
       //sceneData =GetComponent<SceneData>();
       //DontDestroyOnLoad(gameObject);

    }
    private void OnEnable()
    {
       
        
    }
    private void Start()
    {
       // GameManager.Instance.DataLoad();
        StageSet(thisStage);
        CreateMap();
        panel.gameObject.SetActive(false);

    }

    

    private void Update()
    {
        Monstercount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //ThisStageType = planes[thisPlane-1].PlaneType;
        //ThisStageStyle = planes[thisPlane - 1].PlaneStyle;
        //MonsterCountText.text = "Monster : " + Monstercount;
        //StageText.text = "Stage : " + thisStage +" Plane : "+thisPlane;
        //StageTypeText.text = "Concept : " + ThisStageType;
        //StageStyleText.text = "Style : " + ThisStageStyle;

     
        
    }

 

    public void PlaneUp()
    {
        //다음 면 이동
        if (Monstercount == 0) {
            StageSave(thisPlane - 1);
            Clear();
            if (planes.Length<= thisPlane)
            {   //Stage가 바뀔경우
                thisStage++;
                thisPlane = 1;

                StageSet(thisStage);
            }
            else
            {
           
                thisPlane++;
            }
            CreateMap();
        }
    }
    public void PlaneDown()
    {
     
        if(Monstercount==0)
        {
            //이전 면 이동
            if ( thisPlane > 1)
            {
                StageSave(thisPlane - 1);
                Clear();
                thisPlane--;
                CreateMap();
                
            }

        }
      


    }

    void MonsterSpawn(Plane Plane)
    {
       // SummonedEnemy=new Enemy[Plane.enemies.Length];
        for(int i = 0; i < Plane.enemies.Length; i++)
        {
        
              enemies.Add( Instantiate(Plane.enemies[i], Plane.enemiesSpawnPlace[i], Quaternion.identity));
        }
        
    }

    void BoxSpawn(Plane Plane)
    {
      
        for(int i = 0; i < Plane.boxes.Length; i++)
        {
            boxes.Add(Instantiate(Plane.boxes[i], Plane.BoxSpawnPlace[i],Quaternion.identity));
            boxes[i].isOpen = Plane.boxesOpened[i];
        }
    }

    public void CreateMap()
    {
        //StageSet(thisStage);
        Fade();
        Clear();
        MapPrefab = Instantiate(planes[thisPlane - 1].Prefab);            
        Player = FindAnyObjectByType<Player>();
        Player.transform.position = planes[thisPlane - 1].PlayerStartPoint;
        MonsterSpawn(planes[thisPlane - 1]);
        BoxSpawn(planes[thisPlane - 1]);
    }
    public void StageSet(int stage)
    {
        for(int i = 0; i < StageDatabase.stages[stage-1].CubePlanes.Length;i++)
        {
            planes[i] = StageDatabase.stages[stage - 1].CubePlanes[i].Clone();
           
        }
    }
    
    public void StageSave(int Plane)
    {
        for(int i = 0; i < boxes.Count; i++)
        {
            planes[Plane].boxesOpened[i] = boxes[i].isOpen;
        }
       
    }
    
    void Clear()
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

    public void Texting(string str)
    {
        PlayerText.text = str;
    }

}
