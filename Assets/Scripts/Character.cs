using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public float speed;
    public float hp;
    public float mhp;
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
    protected int layerMask = 1 << 8;
    protected Vector3 characterVelocity;
    protected Vector3 characterRotation;
    protected bool isGrounded;
    protected CapsuleCollider col;
    protected float currentWalkSpeed;

    protected Coroutine DeathCoroutine = null;

    public bool isCharacterLookRight;
    
    public event Action OnEnemyDied;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        Stopper();
        groundCheck();
    }

    public void Walk(float Direction)
    {
        if (DeathCoroutine != null) return;
        Direction = Mathf.Clamp(Mathf.Round(Direction), -1,1);
        if (Direction == 0)
        {
            isWalking = false;
            return;
        }

        isWalking = true;
        WalkSpeedAdjust(Direction);

        characterRotation = Vector3.zero;
        characterRotation.y = 180 * (Direction - 1)/2;
        
        if (-90 < characterRotation.y) isCharacterLookRight = false;
        else isCharacterLookRight = true;

        transform.rotation = Quaternion.Euler(characterRotation);

    }

    protected void WalkSpeedAdjust(float Direction)
    {
        currentWalkSpeed = rb.velocity.x;
        if(Mathf.Abs(currentWalkSpeed - ((speed + buffedSpeed) * Direction)) > 0.1f)
        {
            currentWalkSpeed = Mathf.Lerp(currentWalkSpeed, (speed + buffedSpeed) * Direction, 5 * Time.deltaTime);
        }
        else currentWalkSpeed = (speed + buffedSpeed) * Direction;

        characterVelocity.x = currentWalkSpeed;
        characterVelocity.y = rb.velocity.y;
        rb.velocity = characterVelocity;
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

    public virtual void GetHit(float dmg)
    {
        if (DeathCoroutine != null) return;
        hp -=dmg;
        HitSound?.Play();
        anim.Play("Hit",0,0f);

        if(hp <= 0)
        {
            DeathCoroutine = StartCoroutine(Kill());
        }
    }

    public void KnockBack(Vector3 Power)
    {
        //Debug.Log(Power);
        rb.AddForce(Power, ForceMode.Impulse);
    }

    protected IEnumerator Kill() 
    {
        DropItem();
        anim.Play("Death");
        isWalking = false;
        do
        {
            yield return new WaitForEndOfFrame();
        } while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        Destroy(gameObject);
    }

    public void DropItem()
    {

    }

    public void Jump()
    {
        characterVelocity.x = rb.velocity.x;
        characterVelocity.z = 0;
        characterVelocity.y = jumpForce;
        rb.velocity = characterVelocity;
        Debug.Log(rb.velocity);
    }


    protected void groundCheck()
    {
        Physics.Raycast(transform.position, Vector3.down, out hitInfo, col.bounds.extents.y + 0.01f, layerMask);
        Debug.DrawRay(transform.position, Vector3.down * (col.bounds.extents.y + 0.01f), Color.green);

        if (hitInfo.collider != null)
        {
            isGrounded = true;
        }
        else isGrounded = false;
    }
}
