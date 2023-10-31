using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public float speed;
    public float hp;
    public float mhp;
    protected Animator anim;
    protected AudioSource HitSound;
    protected AudioSource KillSound;
    protected bool isHitable;
    protected bool isWalkable;
    protected bool isWalking;
    protected Rigidbody rb;

    private Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Stopper();
    }

    public void Walk(float Direction)
    {
        Direction = Mathf.Clamp(Mathf.Round(Direction), -1,1);
        if (Direction == 0)
        {
            isWalking = false;
            return;
        }
        isWalking = true;
        playerVelocity.x = speed * Direction;
        playerVelocity.y = rb.velocity.y;
        rb.velocity = playerVelocity;
    }

    protected void Stopper()
    {
        if (isWalking == false && Mathf.Abs(rb.velocity.x) > 1)
        {
            playerVelocity.y = rb.velocity.y;
            playerVelocity.x = Mathf.Lerp(0, rb.velocity.x, 0.5f);
            rb.velocity = playerVelocity;
        }
    }

    public void GetHit(float dmg)
    {
        hp -=dmg;
        HitSound?.Play();
        if(hp <= 0)
        {
            Kill();
        }
    }
    IEnumerator Kill() 
    {
        DropItem();
        anim.Play("Dead");
        do
        {
            yield return new WaitForEndOfFrame();
        } while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        Destroy(gameObject);
    }

    public void DropItem()
    {

    }
}
