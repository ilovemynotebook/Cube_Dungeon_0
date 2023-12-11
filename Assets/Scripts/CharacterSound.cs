using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    [Header("Player Act")]
    public AudioSource Walk_AS;
    public AudioSource Jump_AS;
    public AudioSource Attack_AS;
    public AudioSource Charging_AS;
    public AudioSource ChargingAttack_AS;
    public AudioSource Climbing_AS;
    public AudioSource GetHit_AS;
    public AudioSource Death_AS;
    public AudioSource Win_AS;
    public AudioSource TryShield_AS;
    public AudioSource ShieldSucceed_AS;
    public AudioSource Run_AS;

    public AudioSource Active_AS;
    public AudioSource GetItem_AS;
}
