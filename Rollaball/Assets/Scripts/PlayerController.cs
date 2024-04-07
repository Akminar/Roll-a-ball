using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Player movement speed
    public float speed = 0;

    // UI text component to display count of Pickup objects
    public TextMeshProUGUI countText;

    // Player Rigidbody
    private Rigidbody rb;

    // Collected PickUp objects
    private int count;

    // X and Y axis movement
    private float movementX;
    private float movementY;

    // UI object to display win text
    public GameObject winTextObject;

    [SerializeField] float jumpPower = 5f;

    private bool isGrounded = true;

    private bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        // Get player Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Initialize count to zero
        count = 0;

        // Update count display
        SetCountText();

        // Set win text as inactive
        winTextObject.SetActive(false);
    }

    // OnMove is called on any movement input
    void OnMove(InputValue movementValue)
    {
        // Convert input value to Vector2 for movement
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store X and Y components of the movement
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // SetCountText updates displayed count of PickUp objects
    void SetCountText()
    {
        // Update count text with current count
        countText.text = "Count: " + count.ToString();

        // Check if count has reached or exceeded win condition
        if(count >= 5)
        {
            // Display win text
            winTextObject.SetActive(true);
        }
    }

    // FixedUpdate is called once per fixed frame-rate frame
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to Rigidbody
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the PickUp tag
        if(other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object
            other.gameObject.SetActive(false);

            // Increment PickUp count
            count = count + 1;

            // Update count display
            SetCountText();
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump") && (isGrounded || doubleJump)) {
            Jump();
        }

        if(isGrounded && !Input.GetButton("Jump")) {
            doubleJump = false;
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        isGrounded = false;
        doubleJump = !doubleJump;
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}
