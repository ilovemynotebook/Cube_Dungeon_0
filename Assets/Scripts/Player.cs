using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    public bool canMove = true;
    public bool canAttack = true;

    public float sta;
    public float msta;

    public WeaponData currentWeapon;
    public GameObject currentShield;

    public int jumpCount;
    public int mJumpCount;

    public float needChargingWait = 2;

    private float direction = 0;
    private List<GameObject> alreadyHit = new List<GameObject>();

    [SerializeField]
    private BoxCollider attackHitBox;
    private Coroutine attackCoroutine;

    public Buffs[] buffs;

    private float charging = 0;


    public bool isHoldingShield = false;



    public Coroutine stunCoroutine;


    [Header("items and equipments")]
    public bool isUpgraded_weapon = false;
    public bool isUpgraded_shield = false;
    public bool isUpgraded_Item_0 = false;
    public bool isUpgraded_Item_1 = false;
    public bool isUpgraded_Item_2 = false;
    public bool isUpgraded_Item_3 = false;
    public int hpPotion = 5;
    public int staPotion = 5;
    public int dmgPotion = 5;
    public bool key = false;




    override protected void Start()
    {
        buffs = new Buffs[2];
        SkillInit();
        base.Start();
        GameManager.Instance._Player = this.gameObject;
    }

    void SkillInit()
    {
        buffs = GameObject.Find("skillSets").GetComponentsInChildren<Buffs>();
    }

    override protected void Update()
    {
        base.Update();
        TryMove();
        TryAttack();
        TryJump();
        TrySkill();
        TryShield();
        TimeHeal();

        if (Input.GetKeyDown(KeyCode.C))
        {
            hpPotion--;
        }
    }

    void TimeHeal()
    {
        if(sta < msta)
        {
            sta += 20 * Time.deltaTime;
        }
        if(hp > mhp) hp = mhp;
    }

    void TryMove()
    {
        if (canMove)
        {
            direction = Input.GetAxisRaw("Horizontal");
            Walk(direction);
        }
    }

    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canMove)
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
        if (!canAttack) return;

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
            en.GetHit(currentWeapon.Dmg + buffedDmg);
            en.KnockBack((other.transform.position - transform.position).normalized * 15);

            alreadyHit.Add(other.gameObject);
            Debug.Log(other.name);
        }
    }

    private void TrySkill()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            buffs[0].Activate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buffs[1].Activate();
        }
    }

    private void TryShield()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            anim.Play("shield");
            anim.SetBool("holdingShield",true);
            isHoldingShield = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool("holdingShield", false);
            isHoldingShield = false;
        }
    }

    public void ShieldSucceed()
    {
        Debug.Log(1);
        anim.Play("ShieldSucceed",1,0f);
        sta -= 10;

    }

    public void ShieldBroke()
    {
        stunCoroutine = StartCoroutine(ShieldBrokeAnimation());
    }

    protected IEnumerator ShieldBrokeAnimation()
    {
        canAttack = false;
        canMove = false;
        anim.Play("ShieldBroke",0,0);
        do { yield return new WaitForEndOfFrame(); }
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        canMove = true;
        canAttack = true;
    }
}
