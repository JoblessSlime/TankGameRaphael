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

    private float maxAcelerationWhenRunning;
    private float directionYForDeceleration = 0;

    public float speed = 5.0f;
    public float rotationTime = 0.2f;

    private bool isPlayerMoving = false;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction accelerateAction;

    // Wheels rotation
    public float wheelRotationSpeed; // in degrees
    private float wheelAceleration = 1f; // For when player accelerates, wheel rotation accelerates too
    public Transform wheelATransform, wheelBTransform, wheelCTransform, wheelDTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Find right Inputs
        playerInput = GetComponent<PlayerInput>();

        // Action must be given the accurate name
        moveAction = playerInput.actions.FindAction("Move");
        accelerateAction = playerInput.actions.FindAction("Accelerate");

        acceleration = minAcceleration;
    }

    private void FixedUpdate()
    {
        isPlayerMoving = false;
        PlayerMovement();
        Accelerate();
        if(isPlayerMoving)
        {
            acceleration *= accelerationConst;
            acceleration = Mathf.Clamp(acceleration, minAcceleration, maxAcelerationWhenRunning);
        }

        else
        {
            acceleration *= decelerationConst;
            acceleration = Mathf.Clamp(acceleration, minAcceleration, maxAcelerationWhenRunning);
        }
    }

    private void PlayerMovement()
    {
        // Debug.Log(moveAction.ReadValue<Vector2>());
        Debug.Log(objectTransform.forward);

        Vector2 direction = moveAction.ReadValue<Vector2>();

        // Convert Vector2 to Vector3
        // We multiply "direction.x" by 0.1 so it does not hinder the rotation
        Vector3 movement = new Vector3(direction.x * 0.1f, 0.0f, direction.y);

        // Move player on x axis only
        objectTransform.Translate(direction.x *  0.1f * speed * Time.deltaTime, 0.0f, 0.0f, Space.Self);


        objectRigidBody.velocity = (objectTransform.forward * directionYForDeceleration * speed * Time.deltaTime * acceleration);
        objectRigidBody.AddForce(objectTransform.forward * directionYForDeceleration * acceleration, ForceMode.Acceleration);


        if (movement != new Vector3(0, 0, 0))
        {
            isPlayerMoving = true;
            directionYForDeceleration = direction.y;

            // Rotate wheels
            wheelATransform.Rotate(direction.y * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelBTransform.Rotate(direction.y * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelCTransform.Rotate(direction.y * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelDTransform.Rotate(direction.y * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
        }
        else
        {
            directionYForDeceleration *= 0.99f;
            directionYForDeceleration = Mathf.Clamp(directionYForDeceleration, -1, 1);
            // objectRigidBody.velocity = objectRigidBody.velocity * decelerationConst * Time.deltaTime;
        }

        // Rotate player
        objectTransform.Rotate(0, direction.x * rotationTime, 0, Space.Self);
    }

    private void Accelerate()
    {
        float accelerate =  accelerateAction.ReadValue<float>();
        if (accelerate == 1f)
        {
            maxAcelerationWhenRunning = maxAcceleration * 2f;
            wheelAceleration = 1.8f;
        }
        else
        {
            maxAcelerationWhenRunning = maxAcceleration;
            wheelAceleration = 1f;
        }
    }
}
