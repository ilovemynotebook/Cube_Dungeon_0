using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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

    public float armorHP = 100;

    RaycastHit ladderRayHit;

    [Header("about stamina cost")]
    public float attackStaminaCost = 30;
    public float attackStaminaMinimumNeed = 30;
    public float chargeAttackStaminaCost = 100;
    public float chargeAttackStaminaMinimumNeed = 50;

    public float shieldStaminaCost = 20;
    public float shieldProtect = 2; // shiledStaminaCost/shieldProtect = cost

    override protected void Start()
    {
        buffs = new Buffs[2];
        SkillInit();
        base.Start();
        GameManager.Instance.Player = this.gameObject;

    }

    private void OnEnable()
    {
        //itemsSetup();
    }

    void SkillInit()
    {
        buffs = GameObject.Find("Actives").GetComponentsInChildren<Buffs>();
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
        CheckLadder();
        DoClimbing(Input.GetAxisRaw("Vertical"));
        ItemStats();

        if (isClimbing) jumpCount = mJumpCount;
    }

    public void ItemStats()
    {
        if (isUpgraded_Item_1) // armor
            mhp = mhpOrigin + armorHP;
        else mhp = mhpOrigin;
        if (isUpgraded_Item_2)
            mJumpCount = 2;
        else mJumpCount = 1;
    }
    public void itemsSetup()
    {

        isUpgraded_weapon = CanvasManager.Instance.isUpgraded_weapon;
        isUpgraded_shield = CanvasManager.Instance.isUpgraded_shield;
        isUpgraded_Item_0 = CanvasManager.Instance.isUpgraded_Item_0;
        isUpgraded_Item_1 = CanvasManager.Instance.isUpgraded_Item_1;
        isUpgraded_Item_2 = CanvasManager.Instance.isUpgraded_Item_2;
        isUpgraded_Item_3 = CanvasManager.Instance.isUpgraded_Item_3;
        staPotion = CanvasManager.Instance.staPotion;
        dmgPotion = CanvasManager.Instance.dmgPotion;
        key = CanvasManager.Instance.key;
    }
    void CheckLadder()
    {
        Physics.Raycast(transform.position - transform.right * 1.2f, transform.right, out ladderRayHit, 2.4f, 1 << 10);
        if (ladderRayHit.collider == null)
        {
            Physics.Raycast(transform.position + (1 * transform.right) * 1.2f, -1 * transform.right, out ladderRayHit, 2.4f, 1 << 10);
            //Debug.Log(ladderRayHit.collider?.gameObject);
        }
        Debug.DrawRay(transform.position + (1 * transform.right) * 1.2f, -transform.right * 2.4f, Color.red);

        if (ladderRayHit.collider?.tag == "Ladder")
        {

            if (Input.GetKey(KeyCode.W)) {
                ladder = ladderRayHit.collider.gameObject;
                Debug.Log(Mathf.Abs(transform.position.x - ladder.transform.position.x));
                if (Mathf.Abs(transform.position.x - ladder.transform.position.x) < 1)
                    isClimbing = true;
            }
        }
        else
        {
            ladder = null;
            isClimbing = false;
        }
    }

    void TimeHeal()
    {
        if (sta < msta)
        {
            sta += 10 * Time.deltaTime;
            if (sta > msta) sta = msta;
            if (sta < 0) sta = 0;
            CanvasManager.Instance.staminaBar.UpdateValue(sta, msta);
        }
        if (hp > mhp) hp = mhp;
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
        if (!canAttack || attackCoroutine != null) return;

        if (Input.GetKey(KeyCode.Mouse0) && isUpgraded_weapon)
        {
            charging += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (charging < needChargingWait && sta >= attackStaminaMinimumNeed)
            {
                attackCoroutine = StartCoroutine(Attack());
                sta -= attackStaminaCost;
                CanvasManager.Instance.staminaBar.UpdateValue(sta, msta);
            }
            else if(sta >= chargeAttackStaminaMinimumNeed)
            {
                attackCoroutine = StartCoroutine(ChargeAttack());
                sta -= chargeAttackStaminaCost;
                CanvasManager.Instance.staminaBar.UpdateValue(sta, msta);
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
        attackCoroutine = null;
    }

    IEnumerator ChargeAttack()
    {
        alreadyHit.Clear();
        anim.Play("charge_Attack");
        do { yield return new WaitForEndOfFrame(); }
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        alreadyHit.Clear();
        attackCoroutine = null;
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
            Debug.Log(buffs[0]);
            buffs[0].Activate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buffs[1].Activate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            buffs[2].Activate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            buffs[3].Activate();
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

    public void ShieldSucceed(float cost)
    {
        anim.Play("ShieldSucceed",1,0f);
        
        sta -= cost;
        CanvasManager.Instance.staminaBar.UpdateValue(sta, msta);

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

    override public void GetHit(float dmg)
    {
        base.GetHit(dmg);
        CanvasManager.Instance.hpBar.UpdateValue(hp, mhp);
    }
}
