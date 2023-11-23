using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public Enemy enemy;
    public Vector3 spawnPos;
}

public class Enemy : Character
{
    float direction = 1;
    Vector3 floorCheck;
    bool isFrontHaveGround;
    
    public float dmg;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        Patrol();
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
            Walk(direction);
        }
        else
        {
            direction = direction * -1;
        }

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
