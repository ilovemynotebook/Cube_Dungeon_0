using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;


public class Player : Character
{
    [Header("can and is stuffs")]
    public bool canMove = true;
    public bool canAttack = true;
    public bool canShield = true;
    public bool canJump = true;
    public bool canRun = true;
    public bool isHoldingShield = false;

    public float sta;
    public float msta;

    public float runSpeed;

    public WeaponData currentWeapon;
    public GameObject currentShield;

    public int jumpCount;
    public int mJumpCount;

    public float needChargingWait = 2;

    private float direction = 0;



    private Coroutine attackCoroutine;

    public Buffs[] buffs;

    private float charging = 0;

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

    public static Player _player;
  
    override protected void Start()
    {
        if(_player == null)
        {
            _player = this;
        }
        else
        {
            Destroy(_player);
        }
        buffs = new Buffs[2];
        SkillInit();
        base.Start();
        GameManager.Instance.Player = this.gameObject;
        DontDestroyOnLoad(this.gameObject);
        anim = GetComponentInChildren<Animator>();

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

        if (isClimbing)
        {
            canMove = false;
            canAttack = false;
            canShield = false;
            jumpCount = mJumpCount;
        }
        else
        {
            canMove = true;
            canAttack = true;
            canShield = true;
        }
        anim.SetBool("IsGround",isGrounded);
        
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
        hpPotion = CanvasManager.Instance.hpPotion;
        staPotion = CanvasManager.Instance.staPotion;
        dmgPotion = CanvasManager.Instance.dmgPotion;
        key = CanvasManager.Instance.key;
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

    protected override IEnumerator Kill()
    {
        anim.Play("Death");
        isWalking = false;
        do
        {
            yield return new WaitForEndOfFrame();
        } while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        //Destroy(gameObject);
        
        DropItem();
        StageManager stageManager = FindObjectOfType<StageManager>();
        stageManager.GameOver();
        gameObject.SetActive(false);
    }

    public override void DropItem()
    {
        hpPotion = 0;
        staPotion = 0;
        dmgPotion = 0;
        CanvasManager.Instance.hpPotion = 0;
        CanvasManager.Instance.staPotion = 0;
        CanvasManager.Instance.dmgPotion = 0;
        CanvasManager.Instance.UpdateHud();


        var dropped = Instantiate(DropPrefab, transform.position, transform.rotation);
        Item _item;
        dropped.transform.GetChild(0).TryGetComponent<Item>(out _item);

        _item.setItem(hpPotion, staPotion, dmgPotion);
    }


    //about move control========================================================
    void TryMove()
    {
        if (canMove)
        {
            direction = Input.GetAxisRaw("Horizontal");
            if (Input.GetAxisRaw("player run") > 0 && canRun) {
                
                Run(direction); 
            }
            else Walk(direction, speed);
        }
    }

    override public void Walk(float Direction, float speed)
    {
        base.Walk(Direction,speed);
        anim.SetFloat("Walk",Mathf.Abs(Direction));
        anim.SetFloat("Run", 0);
    }

    public void Run(float Direction)
    {
        base.Walk(Direction,runSpeed);
        anim.SetFloat("Run", Mathf.Abs(Direction));
        anim.SetFloat("Walk", 0);
    }

    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            if (isGrounded || jumpCount > 0)
            {
                if (isGrounded) { jumpCount = mJumpCount; }

                anim.Play("JumpStart",0);
                Jump();
                jumpCount--;
            }
        }
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

            if (Input.GetKey(KeyCode.W))
            {
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

    protected override void groundCheck()
    {
        Physics.Raycast(transform.position, Vector3.down, out hitInfo, col.bounds.extents.y + 0.01f, layerMask);
        Debug.DrawRay(transform.position, Vector3.down * (col.bounds.extents.y + 0.01f), Color.green);

        if (hitInfo.collider != null)
        {
            isGrounded = true;
            isClimbing = false;
            anim.SetBool("isClimbing", false);
        }
        else isGrounded = false;
    }

    //========================================



    /// about attack===================================
    void TryAttack()
    {
        if (!canAttack || attackCoroutine != null) return;

        if (Input.GetKey(KeyCode.Mouse0) && isUpgraded_weapon && sta >= attackStaminaMinimumNeed)
        {
            charging += Time.deltaTime;
            anim.SetBool("isCharging", true);
            canRun = false;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (charging < needChargingWait && sta >= attackStaminaMinimumNeed)
            {
                attackCoroutine = StartCoroutine(Attack());
                sta -= attackStaminaCost;
                CanvasManager.Instance.staminaBar.UpdateValue(sta, msta);
                anim.SetBool("isCharging", false);
            }
            else if(sta >= chargeAttackStaminaMinimumNeed)
            {
                attackCoroutine = StartCoroutine(ChargeAttack());
                sta -= chargeAttackStaminaCost;
                CanvasManager.Instance.staminaBar.UpdateValue(sta, msta);
                anim.SetBool("isCharging", false);
            }
            charging = 0;
            canRun = true;
        }
    }

    IEnumerator Attack()
    {
        anim.Play("attack",1);

        do
        {
            yield return new WaitForEndOfFrame();

        }
        while (anim.GetCurrentAnimatorStateInfo(1).IsName("attack"));
        //while (anim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f);
        attackCoroutine = null;
    }

    IEnumerator ChargeAttack()
    {
        anim.Play("charge_Attack",0);
        do { yield return new WaitForEndOfFrame(); }
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        attackCoroutine = null;
    }

    public void manualAlreadyHitClear() // be used in animator
    {
        //alreadyHit.Clear();
        //legacy. no use
    }

    public override void KnockBack(Vector3 Power)
    {
        base.KnockBack(Power);
        CinemachineCamera.Instance.CameraShake(0.5f);
        //CinemachineCamera.Instance.CameraShake(3f, 3f, 10.0f);
    }



    //==================================




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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            buffs[2].Activate();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            buffs[3].Activate();
        }
    }



    /// abous shields===================
    private void TryShield()
    {
        if(Input.GetKeyDown(KeyCode.E) && canShield)
        {
            anim.Play("shield",1);
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
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("ShieldBroke"));
        //while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        canMove = true;
        canAttack = true;
    }

    ///=================================================

    override public void GetHit(float dmg)
    {
        base.GetHit(dmg);
        CanvasManager.Instance.hpBar.UpdateValue(hp, mhp);
    }
    public void GetData(PlayerData playerData)
    {
        hp = playerData.hp;
        mhp = playerData.mhp;
        sta = playerData.sta;
        msta = playerData.msta;
        isUpgraded_Item_0 = playerData.isUpgraded_Item_0;
        isUpgraded_Item_1 = playerData.isUpgraded_Item_1;
        isUpgraded_Item_2 = playerData.isUpgraded_Item_2;
        isUpgraded_Item_3 = playerData.isUpgraded_Item_3;
        isUpgraded_shield = playerData.isUpgraded_shield;
        isUpgraded_weapon = playerData.isUpgraded_weapon;
        hpPotion = playerData.hpPotion;
        staPotion = playerData.staPotion;
        dmgPotion = playerData.dmgPotion;
        key = playerData.key;
    }
}
