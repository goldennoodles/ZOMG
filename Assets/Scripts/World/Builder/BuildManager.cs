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

    private int layerMask {
        get {
            int mask = 1 << 8;
            return mask = ~mask;
        }
    }

    public GameObject toPlace;
    private GameObject toGhost;
    private Ray mousePosition;
    private RaycastHit hit;
    private float yOffset = 0.6f;
    private Building toBuild;
    private Hashtable buildings = new Hashtable();

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        IsBuildingEnabled = true;
        IsObjectSelectedForBuilding = true;

        if (isBuildingEnabled && isObjectSelectedForBuilding)
        {
            if (Physics.Raycast(mousePosition, out hit, Mathf.Infinity, layerMask))
            {

                Vector3 hitPositionOffset = new Vector3(
                    hit.point.x,
                    hit.point.y + yOffset,
                    hit.point.z
                );

                Building building = new Building (
                    toPlace, BuildingType.WOODEN_BARRICADE
                );

                if (Input.GetMouseButtonDown(0) && !buildings.ContainsValue(hit.point))
                {
                    if(!building.isObjectAlreadyPresent()){
                        placeGhostItem(building, hitPositionOffset);
                        Debug.Log("Placing Block On Vector : " + hitPositionOffset);
                    } else {
                        Debug.Log("Collision is detected..");
                    }
                }

                toGhost = building.getGhostItem;
                toGhost.transform.position = hitPositionOffset;

            }
        }
    }

    private void placeGhostItem(Building building, Vector3 position)
    {
        GameObject builtObject = Instantiate(building.getGhostItem, position, Quaternion.identity);
        building.BuildingPosition = position;
        buildings.Add(builtObject, hit.point);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(mousePosition.origin, hit.point);
    }
}
