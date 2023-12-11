using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class LavaBlock : MonoBehaviour
{

    [SerializeField] private float _tickTime;

    [SerializeField] private float _tickDamage;

    private Dictionary<Character, float> _hitCharacterDic = new Dictionary<Character, float>();

    private BoxCollider _boxCollider;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _rigidBody = GetComponent<Rigidbody>();

        _rigidBody.isKinematic = true;
        _rigidBody.useGravity = false;
    }

    private void Update()
    {
        foreach (Character character in _hitCharacterDic.Keys)
        {
            _hitCharacterDic[character] -= Time.deltaTime;
            Debug.Log(_hitCharacterDic[character]);
            if (_hitCharacterDic[character] <= 0)
            {
                _hitCharacterDic.Remove(character);
            }
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Character>(out Character character))
        {
            if (!_hitCharacterDic.ContainsKey(character))
            {
                character.GetHit(_tickDamage);
                _hitCharacterDic.Add(character, _tickTime);
            }
        }
    }


}
