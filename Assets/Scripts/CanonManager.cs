using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanonManager : MonoBehaviour
{
    // private float turn;
    [SerializeField] private Transform canonTransform;
    [SerializeField] private Transform playerTransform;
    public float turnSpeed;

    PlayerInput playerInput;
    InputAction moveCanonAction;
    InputAction centerCanonAction;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Find right Inputs
        playerInput = GetComponent<PlayerInput>();

        // Action must be given the accurate name
        moveCanonAction = playerInput.actions.FindAction("MoveCanon");
        centerCanonAction = playerInput.actions.FindAction("centerCanon");
    }

    // Update is called once per frame
    void Update()
    {
        CanonRotation();
        CenterCanon();
    }

    private void CanonRotation()
    {
        Vector2 direction = moveCanonAction.ReadValue<Vector2>();

        canonTransform.Rotate(0, direction.x * turnSpeed, 0, Space.Self);
    }

    private void CenterCanon()
    {
        float center = centerCanonAction.ReadValue<float>();
        if (center == 1f) 
        {
            canonTransform.rotation = playerTransform.rotation;
        }
    }
}