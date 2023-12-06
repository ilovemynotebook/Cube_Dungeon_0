using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playable : MonoBehaviour
{
    public static Playable  instance;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
