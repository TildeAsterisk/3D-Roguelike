using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharStats playerStats;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    public float zoomSpeed = 5.0f;
    public float minZoom = 1.0f;
    public float maxZoom = 50.0f;

    void Start(){
        playerStats=GetComponent<CharStats>();
        speed = playerStats.Speed.GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the forward direction of the camera
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // Make sure y is 0 so we only get the horizontal direction
        cameraForward.Normalize(); // Normalize the vector to get a unit vector

        // Get the right direction of the camera
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0; // Make sure y is 0 so we only get the horizontal direction
        cameraRight.Normalize(); // Normalize the vector to get a unit vector

        // Calculate the movement direction based on the camera's forward and right directions
        Vector3 movementDirection = cameraForward * Input.GetAxis("Vertical") + cameraRight * Input.GetAxis("Horizontal");
        speed = playerStats.Speed.GetValue();
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        Vector3 rotDir=new Vector3(Mathf.Round(movementDirection.x),movementDirection.y,Mathf.Round(movementDirection.z) );
        
        if(rotDir!=Vector3.zero){transform.rotation = Quaternion.LookRotation (rotDir);}
        /*
        if (movementDirection!=Vector3.zero){
            transform.rotation = Quaternion.LookRotation (movementDirection);
        }*/

        //RESTART MISSION
        if (Input.GetKeyDown("r")){
            Application.LoadLevel(Application.loadedLevel);
        }

         float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll>0 || scroll <0){
            float newSize = Camera.main.orthographicSize - scroll * zoomSpeed;
            Camera.main.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
        }
    }
}
