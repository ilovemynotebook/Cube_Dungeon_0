using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireAttackBehaviour : BossAttackBehaviour
{
    [SerializeField] private float _tickTime;

    private Dictionary<Character, float> _hitCharacterDic = new Dictionary<Character, float>();


    private void Start()
    {
        gameObject.SetActive(false);
    }


    private void Update()
    {
        foreach(Character character in _hitCharacterDic.Keys)
        {
            _hitCharacterDic[character] -= Time.deltaTime;
            Debug.Log(_hitCharacterDic[character]);
            if (_hitCharacterDic[character] <= 0)
            {
                _hitCharacterDic.Remove(character);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<Character>(out Character character))
        {
            if (!_hitCharacterDic.ContainsKey(character))
            {
                character.GetHit(_boss.Power * _powerMul);
                _hitCharacterDic.Add(character, _tickTime);
                Debug.Log("¸ÂÀ½");
            }
        }
    }

    public override void SkillStart()
    {
        gameObject.SetActive(true);
    }

    public override void SkillEnd()
    {
        _hitCharacterDic.Clear();
        gameObject.SetActive(false);
    }
}
