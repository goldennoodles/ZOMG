using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private GameObject currentWeapon;
    public GameObject getCurrentWeapon
    {
        get
        {
            return currentWeapon;
        }
    }

    private List<GameObject> weaponGameObjects = new List<GameObject>();
    public List<GameObject> getAllWeaponsFromList
    {
        get
        {
            return weaponGameObjects;
        }
    }
    private Hashtable unlockedWeapons = new Hashtable();
    public List<GameObject> getAllUnclockedWeapons
    {
        get
        {
            List<GameObject> toReturn = new List<GameObject>();
            foreach (DictionaryEntry allUnlWeap in unlockedWeapons)
            {
                toReturn.Add((GameObject)allUnlWeap.Value);
            }
            return toReturn;
        }
    }


    // This constructor is used to unlock a weapon && || Add that weapon to the weaponsList;
    public Weapon(GameObject go, WeaponType wp)
    {
        this.weaponGameObjects.Add(go);
    
    }

    public void UnlockWeapon(GameObject go, WeaponType wp)
    {
        this.unlockedWeapons.Add(wp, go);
    }

    public GameObject getAWeapon(WeaponType wp)
    {
        Debug.Log("Assigned Weapon : " + wp.ToString());

        foreach (GameObject unlockedWp in weaponGameObjects)
        {
            switch (wp)
            {
                default:
                case WeaponType.PISTOL:
                    if (unlockedWeapons.ContainsKey(wp))
                    {
                        currentWeapon = unlockedWp;
                        return unlockedWp;
                    } 
                    break;

                case WeaponType.RIFLE:
                    if (unlockedWeapons.ContainsKey(wp))
                    {
                        currentWeapon = unlockedWp;
                        return unlockedWp;
                    }
                    break;

                case WeaponType.SHOTGUN:
                    if (unlockedWeapons.ContainsKey(wp))
                    {
                        currentWeapon = unlockedWp;
                        return unlockedWp;
                    }
                    break;

                case WeaponType.MINI_GUN:
                    if (unlockedWeapons.ContainsKey(wp))
                    {
                        currentWeapon = unlockedWp;
                        return unlockedWp;
                    }
                    break;

            }
        }

        if(!unlockedWeapons.ContainsKey(wp)){
            Debug.Log("Weapon is still locked noob");
        }

        return currentWeapon;
    }
}