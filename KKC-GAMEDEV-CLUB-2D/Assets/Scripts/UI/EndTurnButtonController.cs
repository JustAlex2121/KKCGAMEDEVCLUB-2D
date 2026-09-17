// EndTurnButtonController.cs — attach to EndButton GameObject only
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtonController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button endButton;
    public GameObject Handposition;

    private void Awake()
    {
        // Use UnityEngine.UI.Button, which has the onClick event
        endButton.onClick.AddListener(OnEndTurnClicked);
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnEndTurnClicked()
    {
        GameManager.Instance.UpdateGameState(GameState.EnemyTurn);
        Handposition.SetActive(false);
    }

    private void HandleGameStateChanged(GameState state)
    {
        endButton.interactable = state == GameState.PlayerTurn;
    }

    private void OnDestroy()
    {
        endButton.onClick.RemoveListener(OnEndTurnClicked);
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }
}