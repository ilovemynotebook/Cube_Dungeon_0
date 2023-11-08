using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public GameManager gameManager;
    List<Box> boxes = new List<Box>();
    public TMP_Text StageTestText;
    public TMP_Text PlaneTestText;
    public TMP_Text MonsterCountText;
    int Monstercount;
    int ThisStage;
    int ThisPlane;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Start()
    {
        
        StageReset();
        ThisStage = gameManager.Stage;
        ThisPlane = gameManager.CubePlane;
        StageTestText.text = "Stage : " + ThisStage;
        PlaneTestText.text = "Plane : " + ThisPlane;
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
