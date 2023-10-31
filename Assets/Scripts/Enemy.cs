using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    RaycastHit hitInfo;
    float direction = 1;
    Vector3 floorCheck;
    int layerMask = 1 << 8;
    bool isFrontHaveGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Stopper();
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

}
