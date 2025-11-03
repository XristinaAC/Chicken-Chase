using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AutoRunJump3D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float runSpeed = 6f;            // Constant running speed on X-axis
    public float jumpForce = 7f;           // Jump strength
    public float groundCheckDistance = 1.1f; // Distance for raycast ground check
    public LayerMask groundLayer;          // What counts as ground (optional)

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevents tumbling over
    }

    void Update()
    {
        // Ground detection (raycast down from player position)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Jump when space (old input system: "Jump") is pressed and grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Keep constant forward velocity only on X-axis
        Vector3 newVelocity = rb.velocity;
        newVelocity.x = runSpeed; // constant forward movement on X
        rb.velocity = newVelocity;
    }

    void Jump()
    {
        // Reset Y velocity for consistent jumps
        Vector3 v = rb.velocity;
        v.y = 0f;
        rb.velocity = v;

        // Add upward impulse
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    // Visualize ground check ray in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}