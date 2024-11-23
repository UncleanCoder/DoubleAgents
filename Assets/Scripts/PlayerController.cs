using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private int spriteRotationDegrees = 90;
    private Rigidbody2D rb;
    private Vector2 moveDirectionRequested = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
            case InputActionPhase.Performed:
                moveDirectionRequested = context.ReadValue<Vector2>().normalized;
                break;
            default:
                moveDirectionRequested = Vector2.zero;
                break;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirectionRequested * speed;

        if (moveDirectionRequested != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirectionRequested.y, moveDirectionRequested.x) * Mathf.Rad2Deg;
            rb.rotation = angle - spriteRotationDegrees;
        }
    }
}

