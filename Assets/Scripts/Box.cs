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
           // PlaneSceneManager.Instance.StageSave(PlaneSceneManager.Instance.thisPlane);
            
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
