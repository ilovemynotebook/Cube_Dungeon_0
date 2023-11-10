using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneSceneManager : MonoBehaviour
{

    List<Box> boxes = new List<Box>();
    public SceneData sceneData;
    public TMP_Text StageText;
    public TMP_Text StageTypeText;
    public TMP_Text StageStyleText;
    public TMP_Text MonsterCountText;

    int Monstercount;
    int ThisStage;
    int ThisPlane;
    EStageType ThisStageType;
    EStageStyle ThisStageStyle;

    private void Awake()
    {
        sceneData=GetComponent<SceneData>();
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        PlaneSetting();

    }

    private void Update()
    {

        ThisStage = GameManager.Instance.thisStage;
        ThisPlane= GameManager.Instance.thisPlane;
        ThisStageType = GameManager.Instance.thisStageType;
        ThisStageStyle = GameManager.Instance.thisStageStyle;
        Monstercount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        MonsterCountText.text = "Monster : " + Monstercount;
        StageText.text = "Stage : " + ThisStage+" Plane : "+ThisPlane;
        StageTypeText.text = "Concept : " + ThisStageType;
        StageStyleText.text = "Style : " + ThisStageStyle;

    }

    void PlaneSetting()
    {
        Instantiate(GameManager.Instance.StageDatabase.stages[ThisStage - 1].CubePlanes[ThisPlane - 1].Prefab);
        
            
    }

    public void PlaneUp()
    {
        //다음 면 이동

        if (Monstercount == 0) {
            sceneData.boxes = boxes;
            GameManager.Instance.PlaneUp();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }


    }
    public void PlaneDown()
    {

        //이전 면 이동
        

    }

    void MonsterSpawn()
    {

    }
 
}
