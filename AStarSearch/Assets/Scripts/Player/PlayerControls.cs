using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0;
    InputSystem_Actions inputActions;
    Rigidbody rb;
    Vector2 moveInput;

    private void Awake() {
        inputActions = new();
        rb = GetComponent<Rigidbody>();    
    }
    
    private void OnEnable() {
        inputActions.Enable();
    }

    private void OnDisable() {
        inputActions.Disable();
    }
    void Start()
    {
        
    }

    private void Update() {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        Move();
    }

    private void Move() {
        Vector3 moveForce = transform.forward * moveInput.y;
        moveForce *= moveSpeed;
        rb.AddForce(moveForce);
    }

    private void Turn() {
        
    }
}
