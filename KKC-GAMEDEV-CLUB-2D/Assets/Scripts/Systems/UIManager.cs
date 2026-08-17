using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _TitlePanel;
    private Button startButton;
    private GameManager gameManager;
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

    }
    private void Start()
    {
        startButton = GetComponent<Button>();
        gameManager = Object.FindAnyObjectByType<GameManager>();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        _TitlePanel.SetActive(state==GameState.Title);
    }
    void OnButtonClicked()
    { GameManager.Instance.UpdateGameState(GameState.PlayerTurn); }
  

}

