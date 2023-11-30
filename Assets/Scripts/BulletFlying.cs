using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletFlying : MonoBehaviour
{
    [SerializeField]
    Vector3 Direction = Vector3.up;
    Rigidbody rb;

    public float speed;

    GameObject head;
    void Start()
    {
        rb= GetComponent<Rigidbody>();  


        head = transform.Find("Head").gameObject;
        Direction = head.transform.position - transform.position;
        Direction = Direction.normalized;
    }

    void Update()
    {
        FlyToDirection();
    }

    void FlyToDirection()
    {
        rb.velocity = Direction * speed;
    }
}
