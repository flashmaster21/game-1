using UnityEngine;

public class WallController : MonoBehaviour
{
    public float speed = 2f;  // Speed at which the wall moves
    public float minX = 0f;   // Left limit of the wall's movement
    public float maxX = 6f;   // Right limit of the wall's movement

    void Update()
    {
        // Use Mathf.PingPong to move the wall between minX and maxX
        float xPosition = Mathf.PingPong(Time.time * speed, maxX - minX) + minX;
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
    }
}