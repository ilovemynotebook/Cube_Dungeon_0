using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[Serializable]
public class ProjectileData
{
    [Tooltip("소환 오브젝트 프리팹")]
    [SerializeField] private Projectile _projectilePrefab;
    public Projectile ProjectilePrefab => _projectilePrefab;

    [Tooltip("투사체 소환 위치(보스 기준)")]
    [SerializeField] private Vector3 _spawnPos;
    public Vector3 SpawnPos => _spawnPos;

    [Tooltip("소환 까지 걸리는 시간")]
    [SerializeField] private float _spawnTime;
    public float SpawnTime => _spawnTime;
}


public class BossProjectileSkill: BossAttackBehaviour 
{
    [SerializeField] private ProjectileData[] _projectileDatas;

    [Tooltip("이 값이 참이면 dir값은 항상 1로 고정")]
    [SerializeField] private bool _isDirFixationEnabled;


    public override void SkillStart()
    {
        foreach (ProjectileData data in _projectileDatas)
        {
            StartCoroutine(Spawn(data));
        }
    }

    public override void SkillEnd()
    {
    }


    private IEnumerator Spawn(ProjectileData data)
    {
        float timer = 0;
        float endTime = data.SpawnTime + 0.1f;

        while (timer < endTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        SpawnProjectile(data);
    }

    private void SpawnProjectile(ProjectileData data)
    {
        Vector3 dir = (_boss.Target.transform.position - _boss.gameObject.transform.position).normalized;
        int dirX = dir.x > 0 ? 1 : dir.x < 0 ? -1 : 0;

        if (_isDirFixationEnabled)
            dirX = 1;

        Vector3 dataSpawnPos = new Vector3(data.SpawnPos.x * dirX, data.SpawnPos.y, data.SpawnPos.z);
        Vector3 spawnPos = transform.position + dataSpawnPos;

        Projectile projectile = Instantiate(data.ProjectilePrefab, spawnPos, Quaternion.identity);
        projectile.SetPower(_boss, _boss.Power * _powerMul, dirX);
    }
}
