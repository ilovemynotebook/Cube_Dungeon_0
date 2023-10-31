using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float sta;
    public float msta;
    public float strength;

    public GameObject currentWeapon;
    public GameObject currentShield;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Stopper();
        TryMove();
        TryAttack();
    }

    void TryMove()
    {
        Walk(Input.GetAxisRaw("Horizontal"));
    }

    void TryAttack()
    {

    }
}
