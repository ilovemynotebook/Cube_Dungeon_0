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
        
    }
    private void Start()
    {

    }

    private void Update()
    {

        ThisStage = GameManager.Instance.thisStage;
        ThisStageType = GameManager.Instance.thisStageType;
        ThisStageStyle = GameManager.Instance.thisStageStyle;
        Monstercount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        MonsterCountText.text = "Monster : " + Monstercount;
        StageText.text = "Stage : " + ThisStage;
        StageTypeText.text = "Concept : " + ThisStageType;
        StageStyleText.text = "Style : " + ThisStageStyle;

    }

    void PlaneSetting()
    {
       
        
    }

    public void PlaneUp()
    {
        //다음 면 이동

        if(Monstercount == 0) {
            GameManager.Instance.PlaneUp();
            sceneData.boxes = boxes;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }


    }
    public void PlaneDown()
    {
        //이전 면 이동

    }
 
}
