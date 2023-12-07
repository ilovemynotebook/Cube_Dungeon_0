using System;
using System.Collections;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public float speed;
    public float hp;
    [HideInInspector]
    public float mhp;
    public float mhpOrigin;
    public float jumpForce;
    public float buffedSpeed;
    public float buffedDmg;
    
    
    protected Animator anim;
    protected AudioSource HitSound;
    protected AudioSource KillSound;
    protected bool isHitable;
    protected bool isWalkable;
    protected bool isWalking;
    protected Rigidbody rb;
    protected RaycastHit hitInfo;
    protected RaycastHit wallHitInfo;
    protected int layerMask = 1 << 8;
    protected Vector3 characterVelocity;
    protected Vector3 characterRotation;
    protected Vector3 climbingPosition;
    protected Vector3 climbingRotation;
    protected bool isGrounded;
    protected CapsuleCollider col;
    protected float currentWalkSpeed;
    

    protected Coroutine DeathCoroutine = null;

    public bool isCharacterLookRight; // about shield
    public bool isClimbing;

    protected GameObject ladder;

    public GameObject boxPf;
    
    public GameObject DropPrefab;

    [Header("drop Things")]
    public bool isUpgraded_weapon_drop = false;
    public bool isUpgraded_shield_drop = false;
    public bool isUpgraded_Item_0_drop = false;
    public bool isUpgraded_Item_1_drop = false;
    public bool isUpgraded_Item_2_drop = false;
    public bool isUpgraded_Item_3_drop = false;
    public int hpPotion_drop = 0;
    public int staPotion_drop = 0;
    public int dmgPotion_drop = 0;
    public bool key_drop = false;



    // Start is called before the first frame update
    virtual protected void Start()
    {
        mhp = mhpOrigin;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        TryGetComponent<Animator>(out anim);
        climbingRotation = new Vector3(0,-90,0);
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        Stopper();
        groundCheck();

    }

    public virtual void Walk(float Direction,float moveSpeed)
    {
        if (DeathCoroutine != null) return;
        Direction = Mathf.Clamp(Mathf.Round(Direction), -1,1);
        if (Direction == 0)
        {
            isWalking = false;
            return;
        }

        isWalking = true;
        WalkSpeedAdjust(Direction,moveSpeed);
        RotationAdjust(Direction,moveSpeed);

        /*
        characterRotation = Vector3.zero;
        characterRotation.y = 180 * (Direction - 1)/2;
        
        if (-90 < characterRotation.y) isCharacterLookRight = false;
        else isCharacterLookRight = true;

        
        transform.rotation = Quaternion.Euler(characterRotation);
        */

    }

    protected void WalkSpeedAdjust(float Direction, float _speed)
    {
        currentWalkSpeed = rb.velocity.x;
        if(Mathf.Abs(currentWalkSpeed - ((_speed + buffedSpeed) * Direction)) > 0.1f)
        {
            currentWalkSpeed = Mathf.Lerp(currentWalkSpeed, (_speed + buffedSpeed) * Direction, 5 * Time.deltaTime);
        }
        else currentWalkSpeed = (_speed + buffedSpeed) * Direction;

        characterVelocity.x = currentWalkSpeed;
        characterVelocity.y = rb.velocity.y;
        rb.velocity = characterVelocity;
    }

    protected void RotationAdjust(float Direction, float _speed)
    {
        characterRotation = Vector3.zero;
        characterRotation.y = 180 * (Direction - 1) / 2;

        if (-90 < characterRotation.y) isCharacterLookRight = false;
        else isCharacterLookRight = true;



        //transform.rotation = Quaternion.Euler(characterRotation);
        //if (MathF.Abs(transform.rotation.y - characterRotation.y) < 1)
        //    transform.rotation = Quaternion.Euler(characterRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(characterRotation), Time.deltaTime * speed * 10);

        
    }

    protected void Stopper()
    {
        if (isWalking == false && Mathf.Abs(rb.velocity.x) > 0)
        {
            if(rb.velocity.x < 0.01f)
            {
                characterVelocity.y = rb.velocity.y;
                characterVelocity.x = 0;
                rb.velocity = characterVelocity;
                return;
            }
            characterVelocity.y = rb.velocity.y;
            characterVelocity.x = Mathf.Lerp(0, rb.velocity.x, 5 * Time.deltaTime);
            rb.velocity = characterVelocity;
        }

        if(rb.velocity.y > 10)
        {
            characterVelocity.x = rb.velocity.x;
            characterVelocity.y = 10;
            rb.velocity = characterVelocity;
        }
    }

    virtual public void GetHit(float dmg)
    {
        if (DeathCoroutine != null) return;
        isClimbing = false;
        hp -=dmg;
        //HitSound?.Play();
        //Debug.Log(anim);
        anim.Play("Hit",0,0f);

        if(hp <= 0)
        {
            DeathCoroutine = StartCoroutine(Kill());
        }
    }



    public virtual void KnockBack(Vector3 Power)
    {
        //Debug.Log(Power);
        rb.AddForce(Power, ForceMode.Impulse);
    }

    protected virtual IEnumerator Kill() 
    {
        anim.Play("Death");
        isWalking = false;
        do
        {
            yield return new WaitForEndOfFrame();
        } while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        //Destroy(gameObject);
        gameObject.SetActive(false);
        DropItem();
    }

    public virtual void DropItem()
    {
        if (boxPf != null) Instantiate(boxPf,transform.position,transform.rotation);

        var dropped = Instantiate(DropPrefab, transform.position, transform.rotation);
        Item _item;
        dropped.transform.GetChild(0).TryGetComponent<Item>(out _item);

        _item.setItem(hpPotion_drop, staPotion_drop, dmgPotion_drop, isUpgraded_weapon_drop, isUpgraded_shield_drop
            , isUpgraded_Item_0_drop, isUpgraded_Item_1_drop, isUpgraded_Item_2_drop, isUpgraded_Item_3_drop);


    }

    public void Jump()
    {
        isClimbing = false;
        anim.SetBool("isClimbing",false);
        characterVelocity.x = rb.velocity.x;
        characterVelocity.z = 0;
        characterVelocity.y = jumpForce;
        rb.velocity = characterVelocity;
        //Debug.Log(rb.velocity);
    }


    protected virtual void groundCheck()
    {
        Physics.Raycast(transform.position, Vector3.down, out hitInfo, col.bounds.extents.y + 0.01f, layerMask);
        Debug.DrawRay(transform.position, Vector3.down * (col.bounds.extents.y + 0.01f), Color.green);

        if (hitInfo.collider != null)
        {
            isGrounded = true;
            isClimbing = false;
            //anim.SetBool("isClimbing",false);
        }
        else isGrounded = false;



        /*
        Vector3 boxSize = new Vector3(1, 1, 1);
        if (Physics.BoxCast(cubePos, boxSize / 2, Vector3.down, out hit, Quaternion.identity, rayLength))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(cubePos, Vector3.down * hit.distance);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(cubePos + Vector3.down * hit.distance, boxSize);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
        */
    }


    protected void DoClimbing(float direction)
    {
        if (ladder == null)
        {
            isClimbing = false;
            anim.SetBool("isClimbing", false);
        }
        else if (isClimbing == true)
        {
            climbingPosition = transform.position;
            climbingPosition.x = ladder.transform.position.x;
            climbingPosition.y = transform.position.y + direction * Time.deltaTime * speed;
            transform.position = climbingPosition;
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(climbingRotation);
            anim.SetFloat("Climb",direction);
            anim.SetBool("isClimbing",true);
        }

    }
}
