using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

using Random = UnityEngine.Random;

public enum SpawnPosType
{ Boss, Target }



[Serializable]
public class ProjectileData
{
    [Tooltip("����ü ���� ��ġ ����")]
    [SerializeField] private SpawnPosType _spawnPosType;
    public SpawnPosType SpawnPosType => _spawnPosType;

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

    [Tooltip("������ �������� �������� �����ǳ�?")]
    [SerializeField] private bool _isRandomSpawn;

    [Tooltip("���� ���� �ִ� ����")]
    [SerializeField] private float _randomSpawnRange;


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

    protected virtual void SpawnProjectile(ProjectileData data)
    {
        Debug.Log(_boss);
        Vector3 dir = (_boss.Target.transform.position - _boss.gameObject.transform.position).normalized;
        int dirX = dir.x > 0 ? 1 : dir.x < 0 ? -1 : 0;

        if (_isDirFixationEnabled)
            dirX = 1;

        Vector3 dataSpawnPos = new Vector3(data.SpawnPos.x * dirX, data.SpawnPos.y, data.SpawnPos.z);
        Vector3 spawnPos = Vector3.zero;

        if (_isRandomSpawn)
        {
            float randX = dataSpawnPos.x + Random.Range(-_randomSpawnRange, _randomSpawnRange);
            float randZ = dataSpawnPos.z + Random.Range(-_randomSpawnRange, _randomSpawnRange);
            dataSpawnPos = new Vector3(randX, dataSpawnPos.y, randZ);
        }

        if (data.SpawnPosType == SpawnPosType.Boss)
        {
            spawnPos = _boss.transform.position + dataSpawnPos;
        }
        else if(data.SpawnPosType == SpawnPosType.Target)
        {
            spawnPos = _boss.Target.transform.position + dataSpawnPos;
        }

        Projectile projectile = Instantiate(data.ProjectilePrefab, spawnPos, Quaternion.identity);
        projectile.SetPower(_boss, _boss.Power * _powerMul, dirX);
    }
}
