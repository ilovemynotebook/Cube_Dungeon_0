using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        PlaneSceneManager.Instance.PlayerPoint=this.gameObject.transform.position;
    }

    
}
