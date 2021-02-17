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
    public GameObject toPlace; GameObject toGhost;
    private Ray mousePosition; RaycastHit hit;
    private Building toBuild;
    private Hashtable buildings = new Hashtable();

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        IsBuildingEnabled = true;
        IsObjectSelectedForBuilding = true;

        if (
            isBuildingEnabled && isObjectSelectedForBuilding && 
            !buildings.ContainsValue(hit.point)
        )
        {
            if (Physics.Raycast(mousePosition, out hit, Mathf.Infinity, layerMask))
            {

                Vector3 hitPositionOffset = new Vector3(
                    hit.point.x,
                    hit.point.y + yOffset,
                    hit.point.z
                );

                Building building = new Building(
                    toPlace, BuildingType.WOODEN_BARRICADE
                );

                toGhost = building.getGhostItem;
                toGhost.transform.position = hitPositionOffset;

                rotationDirection(toGhost);

                if (Input.GetMouseButtonDown(0))
                {
                    if (!building.isObjectAlreadyPresent())
                    {
                        placeGhostItem(building, toGhost.transform);
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

    private void placeGhostItem(Building building, Transform position)
    {
        GameObject builtObject = Instantiate(building.getGhostItem, position.position, position.rotation);
        building.BuildingPosition = position;
        buildings.Add(builtObject, hit.point);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(mousePosition.origin, hit.point);

    }
}
