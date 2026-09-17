using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GameOverUi : MonoBehaviour
    
{
    [SerializeField] private GameOverManager gameOverManager;
    private int score = 0;
    // int Health;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI scoreText;
    
    private void Awake()
    {
        changeScore();
       // changeHealth(100);
    }
    public void changeScore()
    {
        
        score += 10;
        scoreText.text = ("Score: " + score);

    }
   
    public void changeHealth(int health)
    {
        
        healthText.text = "Health: " + health;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
            // Checks if the player hit an obstacle tag
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOverManager.SetupGameOver();
        }
    }
}
