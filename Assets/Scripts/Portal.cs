using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    //public StageManager SceneManager;
    public bool isup;
    private void OnEnable()
    {
        //SceneManager=FindAnyObjectByType<StageManager>();
    }

    private void Update()
    {
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isup)
            {
                PlaneSceneManager.Instance.PlaneUp();
            }
            else
            {

                PlaneSceneManager.Instance.PlaneDown();
            }
           
        }
    }
}
