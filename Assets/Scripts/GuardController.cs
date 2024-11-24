using System.Collections;
using UnityEngine;

public class GuardController : MonoBehaviour
{
    public float speed = 5f;
    public float waitTime = 5f;
    public Transform[] waypoints;

    private int currentWaypointIndex = 0;
    private bool isWaiting = false;
    private bool isEnabled = true;

    void Update()
    {
        MoveTowardWaypoint();
    }
    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
    }

    private void MoveTowardWaypoint()
    {
        if (! isEnabled)
        {
            return;
        }
        if (isWaiting)
        {
            return;
        }

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        if (distance < 0.001f)
        {
            StartCoroutine(WaitAtWaypoint());
        }
    }

    private IEnumerator WaitAtWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Spotted! Game Over.");
        }
    }

}

