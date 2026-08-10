using System.Collections;
using UnityEngine;

public class EnemySytem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerfomer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
    }
    private IEnumerator EnemyTurnPerfomer(EnemyTurnGA enemyTurnGA)
    {
        Debug.Log("Enemy turn started.");
        yield return new WaitForSeconds(2f);
        Debug.Log("Enemy turn ended.");
    }
}
