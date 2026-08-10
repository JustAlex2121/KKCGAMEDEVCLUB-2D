using UnityEngine;

public class EndTurnButtionUI : MonoBehaviour
{
 public void OnClick()
    {
     EnemyTurnGA enemyTurnGA = new EnemyTurnGA();
        ActionSystem.Instance.Perform(enemyTurnGA);
    }
}
