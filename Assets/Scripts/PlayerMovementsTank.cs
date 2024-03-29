using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovementsTank : MonoBehaviour
{
    // "Transform" is position, rotation, scale.
    // With     [SerializeField],     we can assign which object we want the transform values in Unity.
    // We could also     "GetComponent<Transform>()"     to have values of object sript is assigned to.
    [SerializeField] private Transform objectTransform;
    [SerializeField] private Rigidbody objectRigidBody;

    public float accelerationConst;
    // must be less than accelerationConst
    public float decelerationConst;

    public float maxAcceleration;
    public float minAcceleration;
    public float acceleration;

    public float speed = 5.0f;
    public float rotationTime = 0.2f;

    private bool isPlayerMoving = false;

    PlayerInput playerInput;
    InputAction moveAction;

    // Start is called before the first frame update
    void Start()
    {
        // Find right Inputs
        playerInput = GetComponent<PlayerInput>();

        // Action must be given the accurate name
        moveAction = playerInput.actions.FindAction("Move");

        acceleration = minAcceleration;
    }

    private void FixedUpdate()
    {
        isPlayerMoving = false;
        PlayerMovement();
        if(isPlayerMoving)
        {
            acceleration *= accelerationConst;
            acceleration = Mathf.Clamp(acceleration, minAcceleration, maxAcceleration);
        }

        else
        {
            acceleration *= decelerationConst;
            acceleration = Mathf.Clamp(acceleration, minAcceleration, maxAcceleration);
        }
    }

    private void PlayerMovement()
    {
        Debug.Log(moveAction.ReadValue<Vector2>());
        Debug.Log(objectRigidBody.velocity);

        Vector2 direction = moveAction.ReadValue<Vector2>();

        // Convert Vector2 to Vector3
        // We multiply "direction.x" by 0.1 so it does not hinder the rotation
        Vector3 movement = new Vector3(direction.x * 0.1f, 0.0f, direction.y);

        // Move player
        objectTransform.Translate(direction.x *  0.1f * speed * Time.deltaTime, 0.0f, 0.0f, Space.Self);


        if (movement != new Vector3(0.0f, 0.0f, 0.0f))
        {
            isPlayerMoving = true;
            objectRigidBody.velocity = (objectTransform.forward * direction.y * speed * Time.deltaTime * acceleration);
            objectRigidBody.AddForce(objectTransform.forward * direction.y * acceleration, ForceMode.Acceleration);
        }
        else
        {
            // objectRigidBody.velocity = objectRigidBody.velocity * decelerationConst * Time.deltaTime;
        }

        // Rotate player
        objectTransform.Rotate(0, direction.x * rotationTime, 0, Space.Self);
    }
}
