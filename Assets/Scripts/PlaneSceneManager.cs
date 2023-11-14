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
    [SerializeField] Plane[] planes;
    //public SceneData sceneData;
    public TMP_Text StageText;
    public TMP_Text StageTypeText;
    public TMP_Text StageStyleText;
    public TMP_Text MonsterCountText;
    public GameObject MapPrefab;

    public int Monstercount;
    public int thisStage; //현재 스테이지
    public int thisPlane; // 현재  면
    public int MaxPlane;

    EStageType ThisStageType;
    EStageStyle ThisStageStyle;
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
        thisStage = 1; MaxPlane = 1; thisPlane = 1;
        planes = new Plane[9];
        StageSet(thisStage);
       // mapdatas = new GameObject[9];
       //sceneData =GetComponent<SceneData>();
       //DontDestroyOnLoad(gameObject);

    }
    private void OnEnable()
    {
       
        
    }
    private void Start()
    {

        CreateMap();
       panel.gameObject.SetActive(false);

    }

    

    private void Update()
    {

        ThisStageType = planes[thisPlane-1].PlaneType;
        ThisStageStyle = planes[thisPlane - 1].PlaneStyle;
        Monstercount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        MonsterCountText.text = "Monster : " + Monstercount;
        StageText.text = "Stage : " + thisStage +" Plane : "+thisPlane;
        StageTypeText.text = "Concept : " + ThisStageType;
        StageStyleText.text = "Style : " + ThisStageStyle;

     
        
    }

 

    public void PlaneUp()
    {
        //다음 면 이동
        if (Monstercount == 0) {
            StageSave(thisPlane - 1);
            Destroy(MapPrefab);
            Clear();
            if (planes.Length<= thisPlane)
            {   //Stage가 바뀔경우
                thisStage++;
                thisPlane = 1;
                MaxPlane = 1;
                StageSet(thisStage);
            }
            else
            {
                if (MaxPlane == thisPlane)
                {

                    MaxPlane++;
                }
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
            if (MaxPlane > 1 && thisPlane > 1)
            {
                StageSave(thisPlane - 1);
                Destroy(MapPrefab);
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
        
               Instantiate(Plane.enemies[i], Plane.enemiesSpawnPlace[i], Quaternion.identity);
        }
        
    }

    void BoxSpawn(Plane Plane)
    {
      
        for(int i = 0; i < Plane.boxes.Length; i++)
        {
            boxes.Add (Instantiate(Plane.boxes[i], Plane.BoxSpawnPlace[i],Quaternion.identity));
            boxes[i].isOpen = planes[thisPlane-1].boxesOpened[i];
        }
    }

    void CreateMap()
    {

        Fade();
        MapPrefab = Instantiate(planes[thisPlane - 1].Prefab);            
        Player = FindAnyObjectByType<Player>();
        Player.transform.position = planes[thisPlane - 1].PlayerStartPoint;
        MonsterSpawn(planes[thisPlane - 1]);
        BoxSpawn(planes[thisPlane - 1]);
    }
    void StageSet(int stage)
    {
        for(int i = 0; i < StageDatabase.stages[stage-1].CubePlanes.Length;i++)
        {
            planes[i] = StageDatabase.stages[stage - 1].CubePlanes[i].Clone();
           
        }
    }
    
    void StageSave(int Plane)
    {
        for(int i = 0; i < boxes.Count; i++)
        {
            planes[Plane].boxesOpened[i] = boxes[i].isOpen;
        }
       
    }
    
    void Clear()
    {
        for(int i=0; i<boxes.Count; i++)
        {
            Destroy(boxes[i].gameObject);
            
        }
        boxes.Clear();
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
