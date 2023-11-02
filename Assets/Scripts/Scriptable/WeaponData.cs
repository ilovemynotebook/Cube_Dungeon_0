using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


[Serializable]
public class WeaponData
{
    [SerializeField]
    private string weaponName;
    public string WeaponName { get { return weaponName; } }

    [SerializeField]
    private float dmg;
    public float Dmg { get { return dmg; } }

    [SerializeField]
    private Sprite pic;
    public Sprite Pic { get { return pic; } }

    [SerializeField]
    private bool canChargeAttack = false;
    public bool CanChargeAttack { get {  return canChargeAttack; } }

}
