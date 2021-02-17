using UnityEngine;
using System.Collections.Generic;


public class ResourceLoader
{

    private string buildingPath = "Models/Buildings/";

    public GameObject GetBuilding(BuildingType buildingType)
    {
        switch (buildingType)
        {
            case BuildingType.WOODEN_BARRICADE:
                return Resources.Load(buildingPath + "Wooden_Barricade", typeof(GameObject)) as GameObject;

            case BuildingType.STONE_BARRICADE:
                return Resources.Load(buildingPath + "Stone_Barricade", typeof(GameObject)) as GameObject;

            case BuildingType.STEEL_BARRICADE:
                return Resources.Load(buildingPath + "Steel_Barricade", typeof(GameObject)) as GameObject;

            default:
                Debug.LogError("No Building Matched BuildingType : " + buildingType);
                break;
        }

        return null;
    }
}
