using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaneSceneManager : MonoBehaviour
{
    
    List<Box> boxes = new List<Box>();
    public TMP_Text StageText;
    public TMP_Text StageTypeText;
    public TMP_Text StageStyleText;
    public TMP_Text MonsterCountText;
    int Monstercount;
    int ThisStage;
    EStageType ThisStageType;
    EStageStyle ThisStageStyle;

    private void Awake()
    {

        
    }
    private void Start()
    {

        ThisStage = GameManager.Instance.thisStage;
        ThisStageType = GameManager.Instance.thisStageType;
        ThisStageStyle = GameManager.Instance.thisStageStyle;
        StageText.text = "Stage : " + ThisStage;
        StageTypeText.text = "Concept" + ThisStageType;
        StageStyleText.text = "Style" + ThisStageStyle; 

    }

    private void Update()
    {
        Monstercount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        MonsterCountText.text = "Monster Count : " + Monstercount;
    }

    void StageReset()
    {
       
        
    }

}
