using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class Boss6ProjectileSkill: BossAttackBehaviour 
{
    [SerializeField] private ProjectileData[] _projectileDatas; 

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
        Vector3 dataSpawnPos = new Vector3(data.SpawnPos.x, data.SpawnPos.y, data.SpawnPos.z);
        Vector3 spawnPos = transform.position + dataSpawnPos;

        Projectile projectile = Instantiate(data.ProjectilePrefab, spawnPos, Quaternion.identity);
        projectile.SetPower(_boss, _boss.Power * _powerMul, 1);
    }
}
