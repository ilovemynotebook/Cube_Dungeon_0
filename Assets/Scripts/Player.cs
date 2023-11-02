using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public float sta;
    public float msta;

    public WeaponData currentWeapon;
    public GameObject currentShield;

    public int jumpCount;
    public int mJumpCount;

    public float needChargingWait = 2;

    public bool isUpgraded_weapon = false;
    public bool isUpgraded_shield = false;
    public bool isUpgraded_Item_0 = false;
    public bool isUpgraded_Item_1 = false;
    public bool isUpgraded_Item_2 = false;
    public bool isUpgraded_Item_3 = false;

    private float direction = 0;
    private List<GameObject> alreadyHit = new List<GameObject>();

    [SerializeField]
    private BoxCollider attackHitBox;
    private Coroutine attackCoroutine;


    private float charging = 0;

    override protected void Start()
    {
        base.Start();
    }

    override protected void Update()
    {
        base.Update();
        TryMove();
        TryAttack();
        TryJump();
    }

    void TryMove()
    {
        direction = Input.GetAxisRaw("Horizontal");
        Walk(direction);
    }

    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded || jumpCount > 0)
            {
                if (isGrounded) { jumpCount = mJumpCount; }

                Jump();
                jumpCount--;
            }
        }
    }


    /// about Weapon and Shields
    void TryAttack()
    {
        if (Input.GetKey(KeyCode.Mouse0) && currentWeapon.CanChargeAttack)
        {
            charging += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (charging < needChargingWait)
            {
                attackCoroutine = StartCoroutine(Attack());
            }
            else
            {
                attackCoroutine = StartCoroutine(ChargeAttack());
            }
            charging = 0;
        }
    }

    IEnumerator Attack()
    {
        alreadyHit.Clear();
        anim.Play("attack");

        do { yield return new WaitForEndOfFrame(); }
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        alreadyHit.Clear();
    }

    IEnumerator ChargeAttack()
    {
        alreadyHit.Clear();
        anim.Play("charge_Attack");

        do { yield return new WaitForEndOfFrame(); }
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        alreadyHit.Clear();
    }

    public void manualAlreadyHitClear() // be used in animator
    {
        alreadyHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Enemy" && !alreadyHit.Contains(other.gameObject))
        {
            Enemy en = other.GetComponent<Enemy>(); 
            en.GetHit(currentWeapon.Dmg);
            en.KnockBack((other.transform.position - transform.position).normalized * 15);

            alreadyHit.Add(other.gameObject);
            Debug.Log(other.name);
        }
    }

}
