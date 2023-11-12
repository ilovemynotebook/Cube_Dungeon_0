using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public PlaneSceneManager SceneManager;
    public bool isup;
    private void OnEnable()
    {
        SceneManager=FindAnyObjectByType<PlaneSceneManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isup)
            {
                SceneManager.PlaneUp();
            }
            else
            {
                SceneManager.PlaneDown();
            }
           
        }
    }
}
