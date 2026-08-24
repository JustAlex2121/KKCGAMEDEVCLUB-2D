using CardClasses;
using UnityEngine;
using UnityEngine.UI;

public class EndButton : MonoBehaviour
{
    [SerializeField] private Button _EndButton;
    private GameManager gameManager;
    private Card card;
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

    }

    void OnButtonClicked()
    { GameManager.Instance.UpdateGameState(GameState.EnemyTurn); }
    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        _EndButton.interactable= state == GameState.PlayerTurn;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }
}
