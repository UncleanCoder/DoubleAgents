using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private int spriteRotationDegrees = 90;
    private Rigidbody2D rb;
    public Vector2 moveDirectionRequested = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

