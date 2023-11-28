using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{

    [SerializeField] private Boss _boss;

    [SerializeField] private BoxCollider _skillBox;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Character character))
        {
            character.GetHit(_boss.Power);
        }
    }


}
