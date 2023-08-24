using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private CharStats playerStats;
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;

    void Start(){
        playerStats=GetComponent<CharStats>();
        speed = playerStats.Speed.GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

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

        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        if (movementDirection!=Vector3.zero){
            transform.rotation = Quaternion.LookRotation (movementDirection);
        }
    }
}
