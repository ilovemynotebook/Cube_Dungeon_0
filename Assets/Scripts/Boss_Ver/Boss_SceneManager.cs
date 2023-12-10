using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SceneManager : MonoBehaviour
{
    [SerializeField] private string _mapName;

    [SerializeField] private Transform _spawnPos;

    private void Start()
    {
        Boss_GameManager.Instance.Player.transform.position = _spawnPos.position;
    }
}
