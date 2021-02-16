using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    SHOTGUN,
    RIFLE,
    PISTOL,
    MINI_GUN
}

public class Weapons : MonoBehaviour
{

    public GameObject pistol;

    public GameObject test;

    public List<Weapon> unlockedWeapons = new List<Weapon>();

    private void Start()
    {
        getWeapon();
    }

    public void getWeapon()
    {
        Weapon wp = new Weapon(pistol, WeaponType.PISTOL, true);

        try
        {
            test.GetComponent<MeshFilter>().mesh = wp.getAWeapon(WeaponType.PISTOL).GetComponent<MeshFilter>().mesh;
            test.GetComponent<MeshFilter>().mesh = wp.getAWeapon(WeaponType.RIFLE).GetComponent<MeshFilter>().mesh;
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("If you're seeing this it means you're tryin to hack me game... asshole.");
        }
    }


}