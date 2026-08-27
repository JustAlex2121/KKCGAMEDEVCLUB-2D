using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float HitPoints;
    public float MaxHitPoints = 5;
    public EnemyHealth healthBar;
    public int attackDamage;
    public float attackDelay = 1.5f; // time before damage lands, e.g. for animation
    private Health3 health;
    private Animator animator;
    private Coroutine turnRoutine;

    void Start()
    {
        HitPoints = MaxHitPoints;
        healthBar.SetHealth(HitPoints, MaxHitPoints);
        health = FindFirstObjectByType<Health3>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }


private void HandleGameStateChanged(GameState state)
{
    if (state == GameState.EnemyTurn)
    {
        if (turnRoutine != null) StopCoroutine(turnRoutine);
        turnRoutine = StartCoroutine(EnemyTurnRoutine());
    }
}

    private IEnumerator EnemyTurnRoutine()
    {
        yield return new WaitForSeconds(attackDelay); // gives animation time to play
        BasicAttack();
        yield return new WaitForSeconds(0.5f); // optional pause after attack before handing back control
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
    }

    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
        healthBar.SetHealth(HitPoints, MaxHitPoints);
        if (HitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void BasicAttack()
    {
        if (health == null)
        {
            health = FindFirstObjectByType<Health3>();
        }
        if (health != null)
        {
            health.TakeDamage(attackDamage);
        }
         animator?.SetTrigger("SnakeAttackTrigger");
    }
}