using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool isOpen;
    MeshRenderer box;
    private void Awake()
    {
        box = GetComponent<MeshRenderer>();
        
    }
    private void Update()
    {
        if (isOpen)
        {
            box.material.color  = Color.red;
            PlaneSceneManager.Instance.StageSave(PlaneSceneManager.Instance.thisPlane-1);
            
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
