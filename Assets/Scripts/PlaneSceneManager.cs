using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneSceneManager : MonoBehaviour
{

    List<Box> boxes = new List<Box>();
    //public SceneData sceneData;
    public TMP_Text StageText;
    public TMP_Text StageTypeText;
    public TMP_Text StageStyleText;
    public TMP_Text MonsterCountText;
    public GameObject MapPrefab;
    int Monstercount;
    [SerializeField]int ThisStage;
    [SerializeField] int ThisPlane;
    EStageType ThisStageType;
    EStageStyle ThisStageStyle;



   

    private void Awake()
    {
        
        //sceneData =GetComponent<SceneData>();
        //DontDestroyOnLoad(gameObject);

    }
    private void OnEnable()
    {
       
        
    }
    private void Start()
    {
       
        PlaneSetting();
        if (GameManager.Instance.MaxPlane >ThisPlane)
        {
            MapPrefab = GameManager.Instance.mapdatas[ThisStage - 1];
            MapPrefab.SetActive(true);
        }
        else
        {
            MapPrefab = Instantiate(GameManager.Instance.StageDatabase.stages[ThisStage - 1].CubePlanes[ThisPlane - 1].Prefab);
        }
       
    }

    private void Update()
    {

      
        Monstercount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        MonsterCountText.text = "Monster : " + Monstercount;
        StageText.text = "Stage : " + ThisStage+" Plane : "+ThisPlane;
        StageTypeText.text = "Concept : " + ThisStageType;
        StageStyleText.text = "Style : " + ThisStageStyle;

    }

    public void PlaneSetting()
    {
      
        
     ThisStage = GameManager.Instance.thisStage;
     ThisPlane = GameManager.Instance.thisPlane;
     ThisStageType = GameManager.Instance.StageDatabase.stages[ThisStage - 1].CubePlanes[ThisPlane-1].PlaneType;
     ThisStageStyle = GameManager.Instance.StageDatabase.stages[ThisStage - 1].CubePlanes[ThisPlane - 1].PlaneStyle;

     
        
            
    }

    public void PlaneUp()
    {
        //다음 면 이동

        if (Monstercount == 0) {


            //GameManager.Instance.PlaneUP();
            if (GameManager.Instance.StageDatabase.stages[ThisStage - 1].CubePlanes.Length <= ThisPlane)
            {   //Stage가 바뀔경우
                GameManager.Instance.thisStage++;
                GameManager.Instance.thisPlane = 1;
                GameManager.Instance.MaxPlane = 1;
                PlaneSetting();
            }
            else
            {
                GameManager.Instance.thisPlane++;
                PlaneSetting();
                if (GameManager.Instance.MaxPlane > GameManager.Instance.thisPlane)
                {

                }
                else
                {

                    if (MapPrefab != null)
                    {
                        GameManager.Instance.MaxPlane++;
                        MapPrefab.SetActive(false);
                        DontDestroyOnLoad(MapPrefab);
                        GameManager.Instance.mapdatas[GameManager.Instance.thisPlane - 2] = MapPrefab;
                    }
                    

                }

            }
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        }


    }
    public void PlaneDown()
    {
        Debug.Log(ThisPlane);
        //이전 면 이동
        if (GameManager.Instance.MaxPlane > 1 && ThisPlane > 1)
        {
            GameManager.Instance.thisPlane--;
            PlaneSetting();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }

    void MonsterSpawn()
    {

    }

    

}
