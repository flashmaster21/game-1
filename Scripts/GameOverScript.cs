using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Required for TextMeshPro
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public float speed = 2.0f;
    public GameObject player;
    public GameObject wall;
    public GameObject square;

    private bool hitWall = false;
    private bool hitSquare = false;
    private bool isGameOver = false;

    private TextMeshPro ScoreText;  // Reference to the ScoreText UI element

    public int score = 0;
    public int maxScore = 5;

    void Start()
    {
        // Find the existing ScoreText object that was carried over from the gameplay scene
        ScoreText = FindObjectOfType<TextMeshPro>();

        if (ScoreText != null)
        {
            // Display the final score using PlayerPrefs value
            int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
            ScoreText.text = "Final Score: " + finalScore;
        }
        else
        {
            Debug.LogError("ScoreText not found in the Game Over scene.");
        }
    }

    void Update()
    {
        // Move block up and down
        float newYPosition = Mathf.PingPong(Time.time * speed, 9) - 8;
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

        // Restart the game if space is pressed and game is over
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Example 3");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            if (collision.gameObject == wall)
            {
                hitWall = true;
                IncrementScore();
            }

            if (collision.gameObject == square)
            {
                hitSquare = true;
                IncrementScore();
            }

            if (hitWall && hitSquare && !isGameOver)
            {
                TriggerGameOver();
            }
        }
    }

    void IncrementScore()
    {
        score++;
        Debug.Log("Score: " + score);

        // Check for win condition
        if (score >= maxScore)
        {
            TriggerWin();
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over! Press Space to restart.");
    }

    void TriggerWin()
    {
        isGameOver = true;
        wall.SetActive(false);  // Optionally hide objects
        square.SetActive(false);

        Debug.Log("You Win! Score reached 5.");
        SceneManager.LoadScene("You win");  // Load the winning scene
    }
}