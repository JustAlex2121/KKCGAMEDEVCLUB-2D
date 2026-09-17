//StartButtonController.cs — attach to StartButton GameObject only
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button startButton;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveListener(OnStartButtonClicked);
    }
}
