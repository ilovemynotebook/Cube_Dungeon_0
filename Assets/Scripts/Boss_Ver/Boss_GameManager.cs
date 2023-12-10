using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_GameManager : SingletonHandler<Boss_GameManager>
{
    [SerializeField] private GameObject _player;
    public GameObject Player => _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>().gameObject;
    }
}
