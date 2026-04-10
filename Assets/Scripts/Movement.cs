using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdMovement : MonoBehaviour
{
    [Header("Speed")]
    public float baseSpeed = 4f;
    public float inputSpeedBoost = 3f;

    [Header("Gravity")]
    public float pressedGravity = 9f;
    public float releasedGravity = 2f;

    [Header("Physics")]
    public float maxFallSpeed = -20f;

    [Header("Slope Flight")]
    public float slopeLiftMultiplier = 1.7f;   
    public float minSpeedForLift = 4f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 groundNormal = Vector2.up;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        rb.gravityScale = releasedGravity;
    }

    void Update()
    {
        HandleGravity();
    }

    void FixedUpdate()
    {
        MoveRight();
        LimitFallSpeed();
    }

    
    bool InputHeld()
    {
        return
            (Keyboard.current != null && Keyboard.current.spaceKey.isPressed) ||
            (Mouse.current != null && Mouse.current.leftButton.isPressed);
    }

    void HandleGravity()
    {
        rb.gravityScale = InputHeld() ? pressedGravity : releasedGravity;
    }

    
    void MoveRight()
    {
        float speed = baseSpeed;
        if (InputHeld())
            speed += inputSpeedBoost;

        Vector2 velocity = rb.linearVelocity;

        if (isGrounded)
        {
            Vector2 slopeDir = new Vector2(groundNormal.y, -groundNormal.x).normalized;

            velocity.x = slopeDir.x * speed;
            velocity.y = slopeDir.y * speed;

           
            if (!InputHeld() && velocity.y > 0f && speed > minSpeedForLift)
            {
                velocity.y *= slopeLiftMultiplier;
            }
        }
        else
        {
            velocity.x = speed;
        }

        rb.linearVelocity = velocity;
    }

    void LimitFallSpeed()
    {
        if (rb.linearVelocity.y < maxFallSpeed)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, maxFallSpeed);
    }

    
    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.4f)
            {
                isGrounded = true;
                groundNormal = contact.normal;
                return;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        groundNormal = Vector2.up;
    }
}
