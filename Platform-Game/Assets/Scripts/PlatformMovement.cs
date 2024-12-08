using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    
    public float moveDistance = 2f;
    public float moveSpeed = 2f;

    
    private Vector3 startPosition;

    private int direction = 1;

    void Start()
    {
        // Record the starting position
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
    }
}
