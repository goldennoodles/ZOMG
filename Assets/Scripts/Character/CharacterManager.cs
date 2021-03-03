using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public Camera mainCamera;
    
    private Character _character;
    private Rigidbody _rigidbody;
    private BuildManager _buildManager;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {

        _character = new Character
        {
            Weapon = new Weapon()
        };

        _buildManager = GetComponent<BuildManager>();
        _rigidbody = GetComponent<Rigidbody>();
        Debug.Log(_character.Weapon.GetAllUnlockedWeaponsObjects.Count);
    }




    // Update is called once per frame
    void FixedUpdate()
    {

        characterControls();
        CameraControls();
        
        /*
         * Player Movement [Done]
         * Player Shoot
         * Player Reload
         * Player Meele
         * Player DamageTaken
         * Player Powerups
         */
    }


    private void characterControls()
    {
        // Player Movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveBy = transform.right * x + transform.forward * z;
        
        _rigidbody.MovePosition(transform.position + moveBy.normalized * (speed * Time.deltaTime));
        
        // Player LookAt MousePosition
    }

    private void CameraControls()
    {
        float y = 0;
        
        float clampedCameraDepth = Mathf.Clamp(mainCamera.fieldOfView, 50, 70);
        float clampedMouseData = Input.mouseScrollDelta.y; 
        
        clampedCameraDepth += clampedMouseData;
        
        
        float step = 2f * Time.deltaTime;
        float smoothCameraMovement =
            Mathf.SmoothDamp(mainCamera.fieldOfView, clampedCameraDepth, ref y, step);

        mainCamera.fieldOfView = smoothCameraMovement;
    }
}