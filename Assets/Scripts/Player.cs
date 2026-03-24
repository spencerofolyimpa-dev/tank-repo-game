using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb; // Holds reference to player's rigidbody.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(InputAction.CallbackContext context)  // TODO: Figure out how to update it
    {
        Debug.Log(context.ReadValue<Vector2>());
        Vector2 inputVector = context.ReadValue<Vector2>();

        rb.AddForce(new Vector3(inputVector.x, 0f, inputVector.y) * speed, ForceMode.Force);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // Execute only when the jump button is down
        if (context.performed)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            Debug.Log("Jump!");
        }
    }
}
