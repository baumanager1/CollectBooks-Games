using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera { get; set; }
    private GameObject scootingCameraObject { get; set; }
    private Camera scootingCamera { get; set; }
    private Camera cameraUsed { get; set; }

    public CharacterControllerCollegeStudent characterControllerCollegeStudentScript;
    private Action playerMovementType;
    // Start is called before the first frame update
    private void Start()
    {
        //Main Camera
        mainCamera = Camera.main;
        cameraUsed = mainCamera;

        //Scooting Camera
        scootingCameraObject = GameObject.FindWithTag("ScootingCamera");
        scootingCamera = scootingCameraObject.GetComponent<Camera>();
        scootingCamera.enabled = false;

        characterControllerCollegeStudentScript  = player.GetComponent<CharacterControllerCollegeStudent>();
    }

    // Update is called once per frame
    private void Update()
    {
        FollowPlayer(cameraUsed);
        Debug.Log("Scooting: " + characterControllerCollegeStudentScript.Scooting);
        Debug.Log("FOV: " + GetComponent<Camera>().fieldOfView);
        
    }
    private void OnEnable()
    {
        //Get Notified, when ever the player changes their Movement
        characterControllerCollegeStudentScript.PlayerMovementChanged += ChangeCameraUsed;

    }
    private void ChangeCameraUsed (PlayerConstats.PlayerMovementTypes playerAction)
    {
        if(playerAction == PlayerConstats.PlayerMovementTypes.Scooting)
        {
            cameraUsed = scootingCamera;
            scootingCamera.enabled = true;
            mainCamera.enabled = false;
        }
        else
        {
            mainCamera.enabled = true;
            scootingCamera.enabled = false;
            cameraUsed = mainCamera;
        }
    }
    private void FollowPlayer(Camera camera)
    {
        camera.transform.position = player.transform.position + new Vector3(0, 4, -10);
    }
}
