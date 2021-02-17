using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingType
{
    WOODEN_BARRICADE,
    STONE_BARRICADE,
    STEEL_BARRICADE 
}

public class Building
{
    private GameObject gameObject;
    private BuildingType buildingType;
    private Transform buildingPosition;

    public Building(GameObject gameObject, BuildingType buildigType){
        this.gameObject = gameObject;
        this.buildingType = buildigType;
    }

    public Building(GameObject gameObject, BuildingType buildigType, Transform buildingPosition){
        this.gameObject = gameObject;
        this.buildingType = buildigType;
        this.buildingPosition = buildingPosition;
    }

    public GameObject GetGameObject {
        get {
            return gameObject;
        }
    }

}
