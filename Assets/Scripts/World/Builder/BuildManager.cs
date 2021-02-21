using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class BuildManager : MonoBehaviour
{
    private bool isBuildingEnabled = false;

    public bool IsBuildingEnabled
    {
        get { return isBuildingEnabled; }
        set { this.isBuildingEnabled = value; }
    }

    private bool isObjectSelectedForBuilding = false;

    public bool IsObjectSelectedForBuilding
    {
        get { return isObjectSelectedForBuilding; }
        set { this.isObjectSelectedForBuilding = value; }
    }

    private int layerMask
    {
        get
        {
            int mask = 1 << 8;
            return mask = ~mask;
        }
    }

    private float yOffset = 0.53f;
    private float rotationSpeed = 1f;

    public GameObject selectedBuilding;
    public GameObject ghostBuilding;

    private Ray mousePosition;
    RaycastHit hit;

    private List<Building> building = new List<Building>();

    private ResourceLoader resourceLoader = new ResourceLoader();

    private BuildingType buildingType;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isBuildingEnabled = !isBuildingEnabled;
            
            //Once UI has been implemented this will need to redone
            isObjectSelectedForBuilding = !isObjectSelectedForBuilding;

            ghostBuilding.SetActive(isObjectSelectedForBuilding);
        }

        if (
            isBuildingEnabled && isObjectSelectedForBuilding
        )
        {
            if (Physics.Raycast(mousePosition, out hit, Mathf.Infinity, layerMask))
            {
                Vector3 hitPositionOffset = new Vector3(
                    hit.point.x,
                    hit.point.y + yOffset,
                    hit.point.z
                );

                if (selectedBuilding != null)
                {
                    RotationDirection();
                }

                GetBuildingToPlace(hitPositionOffset);

                try
                {
                    ghostBuilding.GetComponent<MeshFilter>().mesh =
                        selectedBuilding.GetComponent<MeshFilter>().sharedMesh;
                    ghostBuilding.transform.position = hitPositionOffset;
                }
                catch (UnassignedReferenceException e)
                {
                    
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (!IsObjectAlreadyPresent())
                    {
                        PlaceGhostItem(ghostBuilding.transform);
                        Debug.Log("Placing Block On Vector : " + hitPositionOffset);
                    }
                    else
                    {
                        Debug.Log("Collision is detected..");
                    }
                }
            }
        }
    }

    private void GetBuildingToPlace(Vector3 pos)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            buildingType = BuildingType.WOODEN_BARRICADE;
            selectedBuilding = resourceLoader.GetBuilding(BuildingType.WOODEN_BARRICADE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            buildingType = BuildingType.STONE_BARRICADE;
            selectedBuilding = resourceLoader.GetBuilding(BuildingType.STONE_BARRICADE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            buildingType = BuildingType.STEEL_BARRICADE;
            selectedBuilding = resourceLoader.GetBuilding(BuildingType.STEEL_BARRICADE);
        }
    }

    private void RotationDirection()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            ghostBuilding.transform.Rotate((Vector3.up * rotationSpeed), Space.World);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            ghostBuilding.transform.Rotate((Vector3.down * rotationSpeed), Space.World);
        }
    }

    private void PlaceGhostItem(Transform position)
    {
        GameObject builtObject = Instantiate(selectedBuilding, position.position, position.rotation);
        building.Add(new Building(selectedBuilding, buildingType));
    }

    public bool IsObjectAlreadyPresent()
    {
        Collider[] hitColliders = Physics.OverlapBox(
            ghostBuilding.transform.position,
            ghostBuilding.transform.localScale / 2,
            Quaternion.identity
        );

        if (hitColliders.Length > 1)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(mousePosition.origin, hit.point);
    }
}