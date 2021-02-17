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
    private Vector3 buildingPosition;
    public Vector3 BuildingPosition {
        get {
            return buildingPosition;
        }
        set {
            buildingPosition = value;
        }
    }

    public Building(GameObject gameObject, BuildingType buildigType){
        this.gameObject = gameObject;
        this.buildingType = buildigType;
    }

    public Building(GameObject gameObject, BuildingType buildigType, Vector3 position){
        this.gameObject = gameObject;
        this.buildingType = buildigType;
        this.buildingPosition = position;
    }

    public bool isObjectAlreadyPresent () {
        Collider[] hitColliders = Physics.OverlapBox(
            gameObject.transform.position, 
            gameObject.transform.localScale / 2,
            Quaternion.identity
            );

        if(hitColliders.Length > 1){
            return true;
        }

        return false;
    }

    public GameObject getGhostItem {
        get {
            return gameObject;
        }
    }

}
