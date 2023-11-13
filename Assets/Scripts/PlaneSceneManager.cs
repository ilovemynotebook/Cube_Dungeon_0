using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneSceneManager : MonoBehaviour
{
    public static PlaneSceneManager Instance;

    List<Box> boxes = new List<Box>();
    [SerializeField] Plane[] planes;
    //public SceneData sceneData;
    public TMP_Text StageText;
    public TMP_Text StageTypeText;
    public TMP_Text StageStyleText;
    public TMP_Text MonsterCountText;
    public GameObject MapPrefab;
    int Monstercount;

    public int thisStage; //���� ��������
    public int thisPlane; // ����  ��
    public int MaxPlane;
    EStageType ThisStageType;
    EStageStyle ThisStageStyle;

    public StageDatabase StageDatabase; //�������������� ����Ÿ���̽�
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
        //���� �� �̵�

        if (Monstercount == 0) {

            //GameManager.Instance.PlaneUP();
            if (planes.Length<= thisPlane)
            {   //Stage�� �ٲ���
                thisStage++;
                thisPlane = 1;
                MaxPlane = 1;
 
            }
            else
            {
            
                if (MaxPlane == thisPlane)
                {
                    if (MapPrefab != null)
                    {
                        MaxPlane++;
                        MapPrefab.SetActive(false);


                    }

                }
                else
                {


                }
                thisPlane++;

            }

            CreateMap();


        }


    }
    public void PlaneDown()
    {
        Debug.Log(thisPlane);
        //���� �� �̵�
        if (MaxPlane > 1 && thisPlane > 1)
        {
            MapPrefab.SetActive(false);
            thisPlane--;
            //PlaneSetting();
            CreateMap();
        }


    }

    void MonsterSpawn()
    {

    }

    void CreateMap()
    {
        MapPrefab = Instantiate(planes[thisPlane - 1].Prefab);            
        planes[thisPlane-1].arrvied= true;
        Player = FindAnyObjectByType<Player>();
        Player.transform.position = planes[thisPlane - 1].PlayerStartPoint;
    }
    void StageSet(int stage)
    {
        for(int i = 0; i < StageDatabase.stages[stage].CubePlanes.Length;i++)
        {
            planes[i] = StageDatabase.stages[stage].CubePlanes[i];
        }
    }
    
    void StageSave(int Plane, bool[] enemies, bool[] boxes)
    {
        planes[Plane].enemiesDied = enemies;
        planes[Plane].boxesOpened = boxes;
    }

}
