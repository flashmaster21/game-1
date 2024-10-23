using System.Collections;
using UnityEngine;  // For UnityEngine.Vector3
using Vector3Unity = UnityEngine.Vector3;  // Alias to avoid ambiguity with Unity's Vector3

public class CoinScript : MonoBehaviour
{
    public float moveSpeed = 2.0f;        // Speed of movement
    public float moveInterval = 0.5f;     // Time interval for movement

    private PlayerScript playerScript;    // Reference to the PlayerScript to update the score

    private void Start()
    {
        // Find the player GameObject by tag and get the PlayerScript component
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerScript>();
        }

        if (playerScript == null)
        {
            Debug.LogError("PlayerScript not found on the Player GameObject. Ensure the player is tagged 'Player' and has a PlayerScript attached.");
        }

        // Start the coroutine for random movement
        StartCoroutine(MoveRandomly());
    }

    // Coroutine to move the coin randomly at specified intervals
    private IEnumerator MoveRandomly()
    {
        while (true)  // Infinite loop for continuous movement
        {
            yield return new WaitForSeconds(moveInterval);

            // Generate a new random position
            Vector3Unity newPosition = GenerateRandomPosition();

            // Move the coin smoothly to the new position
            while (Vector3Unity.Distance(transform.position, newPosition) > 0.1f)
            {
                transform.position = Vector3Unity.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
                yield return null;  // Wait for the next frame
            }

            // Snap to the final position to avoid jitter
            transform.position = newPosition;
        }
    }

    private Vector3Unity GenerateRandomPosition()
    {
        return new Vector3Unity(
            Random.Range(-8f, 8f),  // X boundaries for movement
            Random.Range(-4f, 4f),  // Y boundaries for movement
            0f                      // Z remains fixed
        );
    }

    // Method called when the player bumps into the coin
    public void GetBumped()
    {
        // Ensure that the PlayerScript is available to update the score
        if (playerScript != null)
        {
            //playerScript.Score++;  // Increment the player's score
            playerScript.UpdateScore();  // Update the score display
        }
        else
        {
            Debug.LogError("PlayerScript not found. Unable to update the score.");
        }

        // Move the coin to a new random position after being bumped
        transform.position = GenerateRandomPosition();
    }
}
