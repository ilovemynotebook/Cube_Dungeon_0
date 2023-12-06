using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaitProjectileSkill : BossAttackBehaviour
{
    [SerializeField] private ProjectileData[] _projectileDatas;

    [SerializeField] private float _waitTime;

    [SerializeField] private Vector3 _parentRotate;

    private GameObject _parent;

    private Coroutine SpawnWaitProjectileRoutine;

    private Coroutine RotateParentRoutine;

    private float _spawnTime;

    public override void SkillStart()
    {
        if(SpawnWaitProjectileRoutine != null)
        {
            StopCoroutine(SpawnWaitProjectileRoutine);
            StopCoroutine(RotateParentRoutine);
        }

        SpawnWaitProjectileRoutine = StartCoroutine(SpawnWaitProjectile());
        RotateParentRoutine = StartCoroutine(RotateParent());



    }

    public override void SkillEnd()
    {
        Destroy(_parent, 10);
    }

    public IEnumerator SpawnWaitProjectile()
    {
        _parent = new GameObject("Projectile Parent");

        int dirX = transform.eulerAngles.y == 90 ? -1 : 1;

        _parent.transform.position = _boss.transform.position + new Vector3(dirX, 1, 0);
        _parent.transform.SetParent(_boss.transform);
        _spawnTime = _waitTime / _projectileDatas.Length;

        foreach (ProjectileData data in _projectileDatas)
        {
            Projectile obj = Instantiate(data.ProjectilePrefab, _parent.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
            obj.transform.SetParent(_parent.transform);
            obj.SetPower(_boss, _boss.Power * _powerMul, 1);

            yield return new WaitForSeconds(_spawnTime);
        }

    }

    public IEnumerator RotateParent()
    {
        while (_parent != null)
        {
            _parent.transform.Rotate(_parentRotate * (Time.deltaTime / _waitTime));
            yield return null;
        }
    }


}
