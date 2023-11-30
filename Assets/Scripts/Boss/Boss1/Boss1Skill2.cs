using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ShockwaveData
{
    [Tooltip("소환 오브젝트 프리팹")]
    [SerializeField] private Shockwave _shockwavePrefab;
    public Shockwave ShockwavePrefab => _shockwavePrefab;

    [Tooltip("충격파 소환 위치(보스 기준)")]
    [SerializeField] private Vector3 _spawnPos;
    public Vector3 SpawnPos => _spawnPos;

    [Tooltip("소환 까지 걸리는 시간")]
    [SerializeField] private float _spawnTime;
    public float SpawnTime => _spawnTime;
}

public class Boss1Skill2 : BossAttackBehaviour
{

    [SerializeField] private ShockwaveData[] _shockwaveDatas;


    public override void SkillStart()
    {
        foreach(ShockwaveData data in _shockwaveDatas)
        {
            StartCoroutine(Spawn(data));
        }
        
    }


    public override void SkillEnd()
    {

    }


    private IEnumerator Spawn(ShockwaveData data)
    {
        float timer = 0;
        float endTime = data.SpawnTime + 0.1f;

        while(timer < endTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        SpawnShockwave(data);
    }

    private void SpawnShockwave(ShockwaveData data)
    {
        Vector3 dir = (_boss.Target.transform.position - _boss.gameObject.transform.position).normalized;
        int dirX = dir.x > 0 ? 1 : dir.x < 0 ? -1 : 0;

        Vector3 dataSpawnPos = new Vector3(data.SpawnPos.x * dirX, data.SpawnPos.y, data.SpawnPos.z);
        Vector3 spawnPos = transform.position + dataSpawnPos;
        Shockwave shockwave = Instantiate(data.ShockwavePrefab, spawnPos, Quaternion.identity);
        shockwave.SetPower(_boss.Power * _powerMul);
       
    }
}
