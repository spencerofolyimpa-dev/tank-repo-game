using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 5f;  

    private Rigidbody rb; // Reference to player's rigidbody.
    private PlayerInput playerInput; // Reference to the PlayerInput component.
    public GameObject playerCamera; // Reference to the player's camera object.
    public float lookSensitivity = 0.05f; // 1 is VERY sensitive.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        // turn off the cursor
        Cursor.lockState = CursorLockMode.Locked;	
    }

    // Update is called once per frame
    void Update()
    {
        Move();  // Executes every frame to continuously update the player's position.
    }


    //CONTROLS///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Move()
    {
        // Reads the "Move" action value of the "PlayerMovement" input map every frame, then apply it to the player's position.
        Vector2 inputVector = playerInput.actions["PlayerMovement/Move"].ReadValue<Vector2>();
        rb.transform.Translate(new Vector3(inputVector.x, 0f, inputVector.y) * speed * Time.deltaTime);
    }

    public void Look(InputAction.CallbackContext context)  // Connected to the PlayerInput event function call.
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        // Debug.Log("Look input: " + lookInput);

        // Rotate camera only on the X-axis. Rotate player body only on the Y-axis. Camera will naturally follow player rotation.
        playerCamera.transform.Rotate(-lookInput.y * lookSensitivity, 0f, 0f, Space.Self);
        rb.transform.Rotate(0f, lookInput.x * lookSensitivity, 0f, Space.World);

        // TODO: Lock camera angle so player doesn't break their neck.
    }
    public void Jump(InputAction.CallbackContext context)  // Connected to the PlayerInput event function call.
    {
        // Execute only when the jump button is down
        if (context.performed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            Debug.Log("Jump!");
        }
    }
}
