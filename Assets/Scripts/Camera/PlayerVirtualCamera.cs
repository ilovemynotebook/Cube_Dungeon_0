using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVirtualCamera : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
