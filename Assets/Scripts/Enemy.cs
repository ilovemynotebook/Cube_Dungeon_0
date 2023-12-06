using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Vector3 floorCheck;
    bool isFrontHaveGround;

    public float dmg;

    public bool canWork = true;

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

            Patrol();
        }
    }

    void Patrol()
    {
        floorCheck.x = direction;
        floorCheck.y = -1;
        floorCheck.z = 0;
        isFrontHaveGround = Physics.Raycast(transform.position, floorCheck.normalized, out hitInfo, 2, layerMask);
        Debug.DrawRay(transform.position, floorCheck.normalized * 2, Color.green);

        if (hitInfo.collider != null)
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
        Debug.Log(2);
        Vector3 _stopped = new Vector3(0, 0, 0);
        _stopped.y = rb.velocity.y;
        _stopped.z = rb.velocity.z;
        _stopped.x = 0;
        rb.velocity = _stopped;

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