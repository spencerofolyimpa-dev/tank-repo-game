using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 5f;  

    private Rigidbody rb; // Reference to player's rigidbody.
    private PlayerInput playerInput; // Reference to the PlayerInput component.
    public GameObject playerCamera; // Reference to the player's camera object.
    private float xRotation = 0f; // Used to track the camera's X-axis rotation for clamping.
    private float mouseYDelta = 0f; // Used to track the change in mouse Y input for clamping camera rotation.
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

        // Debug.Log(xRotation);
    }


    //CONTROLS///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Move()  // Not connected to the PlayerInput event function call.
    {
        // Reads the "Move" action value of the "PlayerMovement" input map every frame, then apply it to the player's position.
        Vector2 inputVector = playerInput.actions["PlayerMovement/Move"].ReadValue<Vector2>();
        rb.transform.Translate(new Vector3(inputVector.x, 0f, inputVector.y) * speed * Time.deltaTime);
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 lookInput = context.ReadValue<Vector2>();

        // Makes sure the xRotaion var doesn't cause overdrag. If xRotation is at max, ignore mouseYDelta if positive. If it be at min, ignore if negative.
        mouseYDelta = ((xRotation <= 90f && mouseYDelta > 0f) || (xRotation >= -90f && mouseYDelta < 0f)) ? 0f : lookInput.y * lookSensitivity;
        xRotation -= mouseYDelta;

        // Rotate camera only on the X-axis. Rotate player body only on the Y-axis. Camera will naturally follow player rotation.
        playerCamera.transform.rotation = Quaternion.Euler(Mathf.Clamp(xRotation, -90f, 90f), rb.transform.localEulerAngles.y, 0f);
        rb.transform.Rotate(0f, lookInput.x * lookSensitivity, 0f, Space.World);

    }
    public void Jump(InputAction.CallbackContext context)
    {
        // Execute only when the jump button is down
        if (context.performed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }

        // TODO: Implement a check to prevent double jumping, such as checking if the player is grounded before allowing another jump.
    }
}
