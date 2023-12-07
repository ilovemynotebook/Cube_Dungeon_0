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
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&Input.GetKeyDown(KeyCode.E))
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
