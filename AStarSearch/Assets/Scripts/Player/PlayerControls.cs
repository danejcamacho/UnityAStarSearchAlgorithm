using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private float acceleration = 500f;
    [SerializeField]
    private float breakingForce = 300f;
    [SerializeField]
    private float maxTurnAngle = 15f;
    [SerializeField]
    private float maxSpeed = 100;

    private float currentAcceleration = 0f;
    private float currentBrakingForce = 0f;
    private float currentTurnAngle = 0f;
    

    InputSystem_Actions inputActions;
    Rigidbody rb;
    Vector2 moveInput;

    List<WheelCollider> wheels = new();

    private void Awake() {
        inputActions = new();
        rb = GetComponent<Rigidbody>();    
        GetComponentsInChildren<WheelCollider>(wheels);
    }
    
    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }

    void Update() {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        currentTurnAngle = maxTurnAngle * moveInput.x;
        currentAcceleration = acceleration * moveInput.y;
        if (rb.linearVelocity.magnitude >= maxSpeed) currentAcceleration = 0f;
        currentBrakingForce = (inputActions.Player.Break.ReadValue<float>() > 0f) ? currentBrakingForce = breakingForce : 0f;
    }

    private void FixedUpdate() {
        

        // accelerate
        Debug.Log("Cur speed: " + rb.linearVelocity.magnitude);
        wheels[0].motorTorque = currentAcceleration;
        wheels[1].motorTorque = currentAcceleration;
        wheels[2].motorTorque = currentAcceleration;
        wheels[3].motorTorque = currentAcceleration;
        Debug.Log("Cur motorTorque: " + wheels[0].motorTorque);

        // break
        wheels[0].brakeTorque = currentBrakingForce;
        wheels[1].brakeTorque = currentBrakingForce;
        wheels[2].brakeTorque = currentBrakingForce;
        wheels[3].brakeTorque = currentBrakingForce;

        // turn
        wheels[0].steerAngle = currentTurnAngle;
        wheels[1].steerAngle = currentTurnAngle;

        wheels[0].transform.localRotation = Quaternion.Euler(0,currentTurnAngle, 0);
        wheels[1].transform.localRotation = Quaternion.Euler(0,currentTurnAngle, 0);
    }
}
