using UnityEngine; 
using TMPro;  // Required for TextMeshPro
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D RB;
    public TextMeshPro ScoreText;  // UI Text to display the player's score
    public float Speed = 5;
    public int Score = 0;
    public string gameOverScene = "Game Over";

    void Start()
    {
        // If ScoreText is not assigned, try to find it programmatically
        if (ScoreText == null)
        {
            ScoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshPro>();

            if (ScoreText == null)
            {
                ScoreText = FindObjectOfType<TextMeshPro>();
            }

            if (ScoreText == null)
            {
                Debug.LogError("ScoreText is not assigned and could not be found in the scene.");
            }
        }

        // Ensure the ScoreText or its parent persists across scenes
        if (ScoreText != null)
        {
            DontDestroyOnLoad(ScoreText.transform.parent.gameObject); // Make the parent Canvas persistent
        }

        UpdateScore();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal") * Speed;
        float moveY = Input.GetAxis("Vertical") * Speed;
        RB.velocity = new Vector2(moveX, moveY);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            Die();  // Trigger death logic
        }

        CoinScript coin = other.gameObject.GetComponent<CoinScript>();
        if (coin != null)
        {
            coin.GetBumped();
            Score++;
            UpdateScore();
        }
    }

    public void UpdateScore()
    {
        if (ScoreText != null)
        {
            ScoreText.text = "Score: " + Score;
        }
        else
        {
            Debug.LogWarning("ScoreText is null. Unable to update the score display.");
        }
    }

    public void Die()
    {
        // Save the score for display on the Game Over screen
        PlayerPrefs.SetInt("FinalScore", Score);
        PlayerPrefs.Save();

        if (!string.IsNullOrEmpty(gameOverScene))
        {
            SceneManager.LoadScene(gameOverScene);
        }
        else
        {
            Debug.LogError("Game Over scene name is not set or is null.");
        }
    }
}