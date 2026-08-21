using CardClasses;
using UnityEngine;
using UnityEngine.UI;

public class EndButton : MonoBehaviour
{
    [SerializeField] private Button _EndButton;
    private GameManager gameManager;
    private Card card;
    public GameObject Handposition;
    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;

    }

   public void OnButtonClicked()
    { GameManager.Instance.UpdateGameState(GameState.EnemyTurn);
        Handposition.SetActive (false);

    }
  
    private void GameManagerOnOnGameStateChanged(GameState state)
    {
        _EndButton.interactable= state == GameState.PlayerTurn;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }
}
