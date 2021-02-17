using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private bool isBuildingEnabled = false;
    public bool IsBuildingEnabled
    {
        get
        {
            return isBuildingEnabled;
        }
        set
        {
            this.isBuildingEnabled = value;
        }
    }

    private bool isObjectSelectedForBuilding = false;
    public bool IsObjectSelectedForBuilding
    {
        get
        {
            return isObjectSelectedForBuilding;
        }
        set
        {
            this.isObjectSelectedForBuilding = value;
        }
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
    private Ray mousePosition; RaycastHit hit;
    private List<Building> building = new List<Building>();
    private ResourceLoader resourceLoader = new ResourceLoader();
    private BuildingType buildingType;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        IsBuildingEnabled = true;
        IsObjectSelectedForBuilding = true;

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
                    rotationDirection(selectedBuilding);
                }

                getBuildingToPlace(hitPositionOffset);

                ghostBuilding.GetComponent<MeshFilter>().mesh = selectedBuilding.GetComponent<MeshFilter>().sharedMesh;
                ghostBuilding.transform.position = hitPositionOffset;

                if (Input.GetMouseButtonDown(0))
                {
                    if (!isObjectAlreadyPresent())
                    {
                        placeGhostItem(ghostBuilding.transform);
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

    private void getBuildingToPlace(Vector3 pos)
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

    private void rotationDirection(GameObject gameObject)
    {
        if (Input.GetKey(KeyCode.Q))
        {
            gameObject.transform.Rotate((Vector3.up * rotationSpeed), Space.World);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            gameObject.transform.Rotate((Vector3.down * rotationSpeed), Space.World);
        }
    }

    private void placeGhostItem(Transform position)
    {
        GameObject builtObject = Instantiate(selectedBuilding, position.position, position.rotation);
        building.Add(new Building(selectedBuilding, buildingType));

    }

    public bool isObjectAlreadyPresent()
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
