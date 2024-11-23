using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public float speed = 5f;
    public float waitTime = 5f;
    public Transform[] waypoints;

    private int spriteRotationDegrees = 90;
    private Rigidbody2D rb;
    private Vector2 moveDirectionRequested = Vector2.zero;

    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveTowardDestination();
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

    private void MoveTowardDestination()
    {
        if (isWaiting) return;

        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Rotate to face the direction of movement
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        // Check if the guard has reached the waypoint
        if (distance < 0.001f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1f); // Wait for 1 second at the waypoint
        isWaiting = false;

        // Move to the next waypoint, looping back to the start if at the end
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}

