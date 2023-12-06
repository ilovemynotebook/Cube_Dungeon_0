using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EnemyData
{
    public GameObject enemy;
    public Vector3 spawnPos;
}

public class Enemy : Character
{
    //평범한 패트롤 에너미.
    //이 스크립트는 모든 에너미를 대표하지 않음. 잡몹 하나 정도임.
    float direction = 1;

    public UnityEvent EnemyActions;

    Vector3 floorCheck;
    bool isFrontHaveGround;
    Vector3 wallCheck;
    bool isFrontHaveWall;
    RaycastHit groundHit;
    RaycastHit wallHit;

    RaycastHit gunSightHit;
    public GameObject bullet;
    Coroutine _GunshotCoroutine;

    public float dmg;

    public bool canWork = true;
    public bool canShoot = true;

    Coroutine workCoroutine = null;


    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (canWork)
        {
            EnemyActions.Invoke();
        }
        //Patrol();
    }

    public void GunReady()
    {
        if (!canShoot) return;
        Physics.Raycast(transform.position, transform.right, out gunSightHit, 10, 1 << 9);

        Debug.DrawRay(transform.position, transform.right * 10, Color.green);
        if (gunSightHit.collider!=null && gunSightHit.collider.CompareTag("Player") == true)
        {
            _GunshotCoroutine = StartCoroutine(GunShotCoroutine());
        }
    }

    public IEnumerator GunShotCoroutine()
    {
        if (_GunshotCoroutine == null)
        {
            var bul = Instantiate(bullet, transform.position, transform.rotation);
            bul.GetComponent<BulletFlying>().speed = 5;

            anim.Play("Attack");

            canShoot = false;
            yield return new WaitForSeconds(2);
            canShoot = true;
            _GunshotCoroutine = null;
        }
    }

    public void Patrol()
    {
        if(!canWork) { 
            WalkSpeedAdjust(0, 1);
            return;
        }
        floorCheck.x = direction;
        floorCheck.y = -1;
        floorCheck.z = 0;
        wallCheck.x = direction ;
        wallCheck.y = 0;
        wallCheck.z = 0;
        isFrontHaveGround = Physics.Raycast(transform.position, floorCheck.normalized, out groundHit, 2, layerMask);
        isFrontHaveWall = Physics.Raycast(transform.position, wallCheck.normalized, out wallHit, 1, 1 << 8 | 1 << 11);

        Debug.DrawRay(transform.position, floorCheck.normalized * 2, Color.green);

        if (groundHit.collider != null && wallHit.collider == null)
        {
            Walk(direction, speed);
        }
        else
        {
            direction = direction * -1;
        }

    }

    public void StopForSec(float sec)
    {
        Vector3 _stopped = new Vector3(0,0,0);
        _stopped.y = rb.velocity.y;
        _stopped.z = rb.velocity.z;
        _stopped.x = 0;
        rb.velocity = _stopped;
        anim.Play("Attack");

        workCoroutine = StartCoroutine(StopForSecCoroutine(sec));
    }

    public IEnumerator StopForSecCoroutine(float sec)
    {
        canWork = false;
        yield return new WaitForSeconds(sec);
        canWork = true;
        workCoroutine = null;
    }
 

    /*private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.GetHit(dmg);
            player.KnockBack((other.transform.position - transform.position).normalized * 15);
        }
    }*/
}
