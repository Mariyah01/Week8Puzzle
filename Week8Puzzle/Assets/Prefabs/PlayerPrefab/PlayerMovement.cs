using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // How fast the Player will move
    [SerializeField] private float speed;

    // The reference to the Camera's Transform that will allow our player to move directionally bbased on which way it is facing the world.
    [SerializeField] private Transform cameraReference;

    // The Player's Rigidbody component
    private Rigidbody rb;

    private float facing;

    // Start is called before the first frame update
    private void Start()
    {
        // Directly grabs the Player's Rigidbody and plugs it in as "rb"
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Creates values out of our inputs multiplied by the amount of speed we set
        float xMovement = Input.GetAxis("Horizontal") * speed;
        float zMovement = Input.GetAxis("Vertical") * speed;

        // Vector3 values made up of our input values
        Vector3 movement = new Vector3(xMovement, 0, zMovement);

        // Bases the player's directional movement on which way the camera is facing
        movement = cameraReference.TransformDirection(movement);

        // Generates movement in the Player's Rigidbody using the Vector3 values from above
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Rotates the player to face towards the direction it is moving in
        if (movement.x != 0 || movement.z != 0)
            facing = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;

        rb.rotation = Quaternion.Euler(0, facing, 0);

        // Locks our camera reference's X rotation to 0, that way the player does not move forward into the ground
        cameraReference.eulerAngles = new Vector3(0, cameraReference.eulerAngles.y, cameraReference.eulerAngles.z);
    }
}
