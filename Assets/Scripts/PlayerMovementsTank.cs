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

    public float maxAcceleration;
    private float maxAcelerationWhenRunning;

    public float speed;
    public float rotationSpeed;
    public float rotationTime = 0.2f;

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
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        Accelerate();
    }

    private void PlayerMovement()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();

        // Rotate player
        objectRigidBody.MoveRotation(objectRigidBody.rotation * Quaternion.Euler(Vector3.up * direction.x * rotationTime));

        // Move player on x axis only
        objectTransform.Translate(direction.x * 0.1f * rotationSpeed * Time.deltaTime, 0.0f, 0.0f, Space.Self);

        // Move player
        objectRigidBody.AddForce(objectTransform.forward * direction.y * speed, ForceMode.Acceleration);

        // Assure player never goes faster than "maxAcelerationWhenRunning"
        if (objectRigidBody.velocity.magnitude > maxAcelerationWhenRunning) 
        {
            objectRigidBody.velocity = Vector3.ClampMagnitude(objectRigidBody.velocity, maxAcelerationWhenRunning);
        }


        if (direction != new Vector2(0, 0))
        {
            // Rotate wheels
            wheelATransform.Rotate(direction.y * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelBTransform.Rotate(direction.y * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelCTransform.Rotate(direction.y * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelDTransform.Rotate(direction.y * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
        }
    }

    private void Accelerate()
    {
        float accelerate =  accelerateAction.ReadValue<float>();
        if (accelerate == 1f)
        {
            maxAcelerationWhenRunning = maxAcceleration * 2f;
            wheelAceleration = 2f;
        }
        else
        {
            maxAcelerationWhenRunning = maxAcceleration;
            wheelAceleration = 1f;
        }
    }
}
