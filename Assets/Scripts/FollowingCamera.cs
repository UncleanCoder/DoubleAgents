using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameManager gameManager;
    public Vector3 offset = new Vector3(0,0,0);
    public float smoothSpeed = 0.125f;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
    }

    void LateUpdate()
    {
        Transform playerTransform = gameManager.getActivePlayerTransform();
        Vector3 desiredPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}

