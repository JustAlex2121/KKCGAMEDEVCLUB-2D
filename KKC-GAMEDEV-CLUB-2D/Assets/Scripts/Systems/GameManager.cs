using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    private int playerHealth;
    private int playerXP;
    private int difficulty;

    public GameState State;

    public static event System.Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
         
        }
        else
        {
            Destroy(gameObject);
        }
    }
   


    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    public int PlayerXP
    {
        get { return playerXP; }
        set { playerXP = value; }
    }

    public int Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }


    void Start()
    {
        UpdateGameState(GameState.Title);
    }
    public void UpdateGameState(GameState newState)
    {
        Debug.Log($"UpdateGameState called: {newState} (previous: {State})");
        State = newState;

        switch (newState)
        {
            case GameState.Title:
                HandleTitle();
                Debug.Log("Game State: Title");
                                break;
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                Debug.Log("Game State: Player Turn");
                break;
            case GameState.EnemyTurn:
                Debug.Log("Game State: Enemy Turn");
                // HandleEnemyTurn();  
                break;
            case GameState.Victory:
               Debug.Log("Game State: Victory");
                // Handle Victory state
                break;
            case GameState.Defeat:
               Debug.Log("Game State: Defeat");
                // Handle Defeat state
                break;
                default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
       
        OnGameStateChanged?.Invoke(newState);

    }
    private void HandleTitle()
    {
        // Implement Title logic here
    }
    private void HandlePlayerTurn()
    {
        // Implement Title logic here
    }
}
public enum GameState
{
    Title,
    PlayerTurn,
    EnemyTurn,
    Victory,
    Defeat,

}
    
