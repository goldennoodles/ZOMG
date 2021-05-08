using UnityEngine;
using System.Collections.Generic;


namespace Util
{
    public class ResourceLoader
    {

        private string _buildingPath = "Models/Buildings/";
        private string _weaponsPath = "Models/Weapons/";

        public GameObject GetBuilding(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.WOODEN_BARRICADE:
                    return Resources.Load(_buildingPath + "Wooden_Barricade", typeof(GameObject)) as GameObject;

                case BuildingType.STONE_BARRICADE:
                    return Resources.Load(_buildingPath + "Stone_Barricade", typeof(GameObject)) as GameObject;

                case BuildingType.STEEL_BARRICADE:
                    return Resources.Load(_buildingPath + "Steel_Barricade", typeof(GameObject)) as GameObject;

                default:
                    Debug.LogError("No Building Matched BuildingType : " + buildingType);
                    break;
            }

            return null;
        }

        public GameObject GetWeapon(WeaponType weaponType)
        {
            switch (weaponType)
            {
                case WeaponType.PISTOL:
                    return Resources.Load(_weaponsPath + "pistol", typeof(GameObject)) as GameObject;

                case WeaponType.RIFLE:
                    return Resources.Load(_weaponsPath + "rifle", typeof(GameObject)) as GameObject;

                case WeaponType.SHOTGUN:
                    return Resources.Load(_weaponsPath + "shotgun", typeof(GameObject)) as GameObject;

                case WeaponType.MINI_GUN:
                    return Resources.Load(_weaponsPath + "mini_gun", typeof(GameObject)) as GameObject;

                default:
                    Debug.LogError("No Weapons Matched WeaponType : " + weaponType);
                    break;
            }

            return null;
        }
    }
}
