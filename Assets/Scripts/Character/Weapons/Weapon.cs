using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public enum WeaponType
{
    SHOTGUN,
    RIFLE,
    PISTOL,
    MINI_GUN
}

public class Weapon
{
    private readonly ResourceLoader _resourceLoader = new ResourceLoader();

    private WeaponType _currentWeaponType;

    public WeaponType GetCurrentWeaponType
    {
        get { return _currentWeaponType; }
    }

    private GameObject _currentWeapon;

    public GameObject getCurrentWeapon
    {
        get { return _currentWeapon; }
    }

    private List<GameObject> _weaponGameObjects = new List<GameObject>();

    public List<GameObject> getAllWeaponsFromList
    {
        get { return _weaponGameObjects; }
    }

    private Hashtable _unlockedWeapons = new Hashtable();

    public List<GameObject> GetAllUnlockedWeaponsObjects
    {
        get
        {
            List<GameObject> toReturn = new List<GameObject>();
            foreach (DictionaryEntry _unlockedWeapons in _unlockedWeapons)
            {
                toReturn.Add((GameObject) _unlockedWeapons.Value);
            }

            return toReturn;
        }
    }


    public Weapon()
    {
        // Unlocks The Starter Weapon So We can get it
        UnlockWeapon(WeaponType.PISTOL, _resourceLoader.GetWeapon(WeaponType.PISTOL));

        //Assigns the _currentWeapon as the StarterWeapon WeaponType.Pistol
        _currentWeapon = GetAWeapon(WeaponType.PISTOL);
    }

    // This constructor is used to add that weapon to the weaponsList;
    public Weapon(GameObject go, WeaponType wp)
    {
        this._weaponGameObjects.Add(go);
    }

    private void UnlockWeapon(WeaponType wp, GameObject go)
    {
        this._unlockedWeapons.Add(wp, go);
    }

    public GameObject GetAWeapon(WeaponType wp)
    {
        Debug.Log("Assigned Weapon : " + wp.ToString());

        if (_unlockedWeapons.ContainsKey(wp))
        {
            _currentWeaponType = wp;

            switch (wp)
            {
                case WeaponType.PISTOL:
                    _currentWeapon = _resourceLoader.GetWeapon(WeaponType.PISTOL);
                    break;

                case WeaponType.RIFLE:
                    _currentWeapon = _resourceLoader.GetWeapon(WeaponType.RIFLE);
                    break;

                case WeaponType.SHOTGUN:
                    _currentWeapon = _resourceLoader.GetWeapon(WeaponType.SHOTGUN);
                    break;

                case WeaponType.MINI_GUN:
                    _currentWeapon = _resourceLoader.GetWeapon(WeaponType.MINI_GUN);
                    break;

                default:
                    Debug.Log("Incorrect WeaponType");
                    break;
            }
        }
        else
        {
            Debug.Log("Weapon is still locked noob");
        }

        return _currentWeapon;
    }
}