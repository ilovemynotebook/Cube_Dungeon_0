using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", 
    menuName ="Scriptable Object/Weapon Data",
    order = int.MaxValue)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string weaponName;
    public string WeaponName { get { return name; } }

    [SerializeField]
    private float dmg;
    public float Dmg { get { return dmg; } }
       
    
}
