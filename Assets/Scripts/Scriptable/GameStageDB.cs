using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameStageDB", menuName = "Scriptable Object/Game StageDB", order = int.MaxValue)]


public class GameStageDB : ScriptableObject
{
    [SerializeField]
    public Stage stages;
}
