using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 5f;  

    private Rigidbody rb; // Holds reference to player's rigidbody.
    private PlayerInput playerInput; // Reference to the PlayerInput component.
    public GameObject playerCamera; // Reference to the player's camera object.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();  // Executes every frame to continuously update the player's position.
    }


    /*CONTROLS************************************************************************************************/

    private void Move()
    {
        // Reads the "Move" action value of the "PlayerMovement" input map every frame, then apply it to the player's position.
        Vector2 inputVector = playerInput.actions["PlayerMovement/Move"].ReadValue<Vector2>();
        rb.transform.Translate(new Vector3(inputVector.x, 0f, inputVector.y) * speed * Time.deltaTime);
    }

    public void Look(InputAction.CallbackContext context)  // Connected to the PlayerInput event function call.
    {
        Vector2 lookInput = context.ReadValue<Vector2>();
        Debug.Log("Look input: " + lookInput);

        //!\\ ERROR: The player's body spins but the camera does not folllow. //!\\
        playerCamera.transform.rotation = Quaternion.Euler(-lookInput.y, transform.rotation.y, transform.rotation.z); // Rotate camera only up and down
        transform.rotation = Quaternion.Euler(transform.rotation.x, -lookInput.x, transform.rotation.z);
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
