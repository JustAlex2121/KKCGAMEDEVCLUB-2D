using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    private int playerHealth;
    private int playerXP;
    private int difficulty;

    public OptionManager optionManager { get; private set; }
   public AudioManager audioManager { get; private set; }
    public DeckManager deckManager { get; private set; }

    public GameState State;

    public static event System.Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void InitializeManagers()
    {
        optionManager = GetComponent<OptionManager>();
       deckManager = GetComponent<DeckManager>();
        audioManager = GetComponent<AudioManager>();

        if (optionManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionManager");
            if (prefab == null)
            {
                Debug.LogError("OptionManager prefab not found in Resources/Prefabs.");

            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                optionManager = GetComponentInChildren<OptionManager>();
            }
        }

        

        if (audioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if (prefab == null)
            {
                Debug.LogError("AudioManager prefab not found in Resources/Prefabs.");
                    }
                    else
                    {
                        Instantiate(prefab, transform.position, Quaternion.identity, transform);
                        audioManager = GetComponentInChildren<AudioManager>();
                    }
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
        UpdateGameState(GameState.MainMenu);
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                //HandleMainMenu();
                break;
            case GameState.PlayerTurn:
               // HandlePlayerTurn();
                break;
            case GameState.EnemyTurn:
               // HandleEnemyTurn();  
                break;
            case GameState.Victory:
                // Handle Victory state
                break;
            case GameState.Defeat:
                // Handle Defeat state
                break;
                default:
                throw new System.ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
       
        OnGameStateChanged?.Invoke(newState);

    }
    private void HandleMainMenu()
    {
        // Implement MainMenu logic here
    }

}
public enum GameState
{
    MainMenu,
    PlayerTurn,
    EnemyTurn,
    Victory,
    Defeat,

}
    
