using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Util;
using static UnityEngine.Mathf;

public class BuildManager : MonoBehaviour
{
    private static readonly float BUILDING_TIMER = 2f;
    
    private bool _isBuildingEnabled = false;

    public bool IsBuildingEnabled
    {
        get => _isBuildingEnabled;
        set => _isBuildingEnabled = value;
    }

    private bool _isObjectSelectedForBuilding = false;

    public bool IsObjectSelectedForBuilding
    {
        get => _isObjectSelectedForBuilding;
        set => _isObjectSelectedForBuilding = value;
    }

    private int layerMask
    {
        get
        {
            int mask = 1 << 8;
            return mask = ~mask;
        }
    }

    private float rotationSpeed = 1f;

    public GameObject selectedBuilding;
    public GameObject ghostBuilding;

    public Material completedMaterial;

    private Ray _mousePosition;
    private RaycastHit _hit;

    private readonly List<Building> _building = new List<Building>();
    private readonly ResourceLoader _resourceLoader = new ResourceLoader();

    private BuildingType _buildingType;

    // Update is called once per frame
    void Update()
    {
        _mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _isBuildingEnabled = !_isBuildingEnabled;
            //Once UI has been implemented this will need to redone
            _isObjectSelectedForBuilding = !_isObjectSelectedForBuilding;

            ghostBuilding.SetActive(_isObjectSelectedForBuilding);
        }

        if (_isBuildingEnabled && _isObjectSelectedForBuilding)
        {
            if (Physics.Raycast(_mousePosition, out _hit, Infinity, layerMask))
            {
                Vector3 cell = _hit.transform.position;
                Vector3 pos = new Vector3(
                    cell.x,
                    cell.y + 1.1f,
                    cell.z);

                GetBuildingToPlace(pos);
                DoBuilding(pos);
            }
        }
    }
    private void DoBuilding(Vector3 hitPositionOffset) 
    {
        try
        {
            ghostBuilding.GetComponent<MeshFilter>().mesh =
                selectedBuilding.GetComponent<MeshFilter>().sharedMesh;

            ghostBuilding.transform.position = hitPositionOffset;
        }
        catch (UnassignedReferenceException e)
        {
            e.ToString();
        }

        if (Input.GetMouseButton(0))
        {
            if (!IsObjectAlreadyPresent())
            {
                StartCoroutine(PlaceGhostItem(ghostBuilding.transform));
            }
            else
            {
                Debug.Log("Collision is detected..");
            }
        }
    }

    private void GetBuildingToPlace(Vector3 pos)
    {
        if (!selectedBuilding.Equals(null))
        {
            RotationDirection();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _buildingType = BuildingType.WOODEN_BARRICADE;
            selectedBuilding = _resourceLoader.GetBuilding(BuildingType.WOODEN_BARRICADE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _buildingType = BuildingType.STONE_BARRICADE;
            selectedBuilding = _resourceLoader.GetBuilding(BuildingType.STONE_BARRICADE);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _buildingType = BuildingType.STEEL_BARRICADE;
            selectedBuilding = _resourceLoader.GetBuilding(BuildingType.STEEL_BARRICADE);
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

    private IEnumerator PlaceGhostItem(Transform a)
    {
        var buildObject = Instantiate(selectedBuilding, a.position, a.rotation);

        yield return new WaitForSeconds(BUILDING_TIMER);

        buildObject.GetComponent<MeshRenderer>().material = completedMaterial;

        _building.Add(new Building(selectedBuilding, _buildingType, a));
    }

    private bool IsObjectAlreadyPresent()
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
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_mousePosition.origin, _hit.point);
    }
}