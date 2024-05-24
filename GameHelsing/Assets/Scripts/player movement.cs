using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class playerMovement : MonoBehaviour
{
    public UnityEvent OnDash;
    public UnityEvent OffDash;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public InputAction PlayerMovement;
    private Vector2 MoveDirection;
    private Vector2 SmoothMovement;
    private Vector2 MovementInputSmoothVelocity;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 3f;
    public bool isDashing;
    bool canDash = true;

    private void Start()
    {
        canDash = true;
    }

    private void OnEnable()
    {
        PlayerMovement.Enable();
    }

    private void OnDisable()
    {
        PlayerMovement.Disable();
    }

    void Update()
    {
        if (isDashing)
        {
            return; // Skip movement if currently dashing
        }

        ProcessInputs();

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return; // Skip physics update if currently dashing
        }
        SmoothMovement = Vector2.SmoothDamp(SmoothMovement, MoveDirection, ref MovementInputSmoothVelocity, 0.09f);
        Move();
    }

    void ProcessInputs()
    {
        // Read input from PlayerMovement action
        MoveDirection = PlayerMovement.ReadValue<Vector2>().normalized;
    }

    void Move()
    {
        // Apply velocity based on input
        rb.velocity = new Vector2(SmoothMovement.x * moveSpeed, SmoothMovement.y * moveSpeed);
    }

    private IEnumerator Dash()
    {
        OnDash.Invoke();
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(MoveDirection.x * dashSpeed, MoveDirection.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        OffDash.Invoke();
        canDash = true;
    }
}
