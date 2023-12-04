using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[Serializable]
public class ProjectileData
{
    [Tooltip("��ȯ ������Ʈ ������")]
    [SerializeField] private Projectile _projectilePrefab;
    public Projectile ProjectilePrefab => _projectilePrefab;

    [Tooltip("����ü ��ȯ ��ġ(���� ����)")]
    [SerializeField] private Vector3 _spawnPos;
    public Vector3 SpawnPos => _spawnPos;

    [Tooltip("��ȯ ���� �ɸ��� �ð�")]
    [SerializeField] private float _spawnTime;
    public float SpawnTime => _spawnTime;
}


public class BossProjectileSkill: BossAttackBehaviour 
{
    [SerializeField] private ProjectileData[] _projectileDatas;

    [Tooltip("�� ���� ���̸� dir���� �׻� 1�� ����")]
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
