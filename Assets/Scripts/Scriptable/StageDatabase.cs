using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Stage DataBase", menuName = "Scriptable Object/Stage DataBase", order = int.MaxValue)]


public class StageDatabase : ScriptableObject
{
    [SerializeField]
    public Stage[] stages;
}
