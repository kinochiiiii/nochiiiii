using UnityEngine;
using UnityEngine.InputSystem;

// Required for InputAction

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.8f;

    private CharacterController controller;
    private Vector3 moveInput;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"Move Input: {moveInput}");
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log($"Jumping {context.performed} - IsGrounded: {controller.isGrounded}");
        if (context.performed && controller.isGrounded)
        {
            Debug.Log("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Update()
    {
        // Reset downward velocity when touching the ground
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Handle horizontal movement
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * speed * Time.deltaTime);

        // Handle gravity and vertical movement
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}