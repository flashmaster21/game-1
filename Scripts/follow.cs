using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Required for TextMeshPro
using UnityEngine.SceneManagement;

public class BasicFollower : MonoBehaviour
{
    public Transform target;  // Reference to the player
    public float within_range = 10f;  // Range within which the hazard follows the player
    public float speed = 2f;  // Speed of the hazard's movement

    void Start()
    {
        // Automatically find the player GameObject by tag or name
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.transform;  // Set the player's transform as the target
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure the player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (target == null) return;  // If the target is not set, do nothing

        // Get the distance between the player and this hazard
        float dist = Vector3.Distance(target.position, transform.position);

        // Check if the player is within range
        if (dist <= within_range)
        {
            // Move towards the player's position
            transform.position = Vector3.MoveTowards(
                transform.position, 
                target.position, 
                speed * Time.deltaTime
            );
        }
    }
}

