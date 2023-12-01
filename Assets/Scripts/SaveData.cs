using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData 
{
    //public PlayerData playerData;
    public int thisStage;
    public int thisPlane;
    public Plane[] planes;

    public SaveData()
    {
        //playerData = new PlayerData();
        thisStage = 1; thisPlane = 1;
        planes = new Plane[4];
    }
}
