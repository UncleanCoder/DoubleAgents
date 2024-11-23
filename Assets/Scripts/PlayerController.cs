using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

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
                moveDirectionRequested = context.ReadValue<Vector2>();
                break;
            default:
                moveDirectionRequested = Vector2.zero;
                break;
        }
    }


    void Update()
    {
        rb.MovePosition(rb.position + (moveDirectionRequested * speed) * Time.deltaTime);
    }
}

