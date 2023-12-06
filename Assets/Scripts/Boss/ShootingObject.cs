using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _disabledTime;

    private BossController _boss;

    private float _power;

    private float _speed;
   


    public void Init(BossController boss,float speed, float power)
    {
        _boss = boss;
        _speed = speed;
        _power = power;
        Destroy(gameObject, _disabledTime);
    }

    public void Update()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Character character))
        {
            character.GetHit(_power);
            Destroy(gameObject);
        }
    }

}
