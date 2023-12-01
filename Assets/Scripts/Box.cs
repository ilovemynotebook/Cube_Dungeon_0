using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class BoxData
{
    public Box box;
    public Vector3 spawnPos;
    public bool isOpen;

    public BoxData Cloneing()
    {
        BoxData boxData = new BoxData();
        boxData.box = box;
        boxData.spawnPos = spawnPos;
        boxData.isOpen = isOpen;
        return boxData;

    }
}


public class Box : MonoBehaviour
{
    public bool isOpen;
    MeshRenderer box;

    public bool isInited;
    private void Awake()
    {
        box = GetComponent<MeshRenderer>();
        
    }

    private void Update()
    {
        if (isOpen)
        {
            box.material.color  = Color.red;
            PlaneSceneManager.Instance.StageSave(PlaneSceneManager.Instance.thisPlane);
            
        }
        else if (!isOpen)
        {
            box.material.color = Color.gray;
        }

    }

    //public Box clone()
    //{
    //    return 
    //}
}
