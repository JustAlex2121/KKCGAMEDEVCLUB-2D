using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool ranalready;
    

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

    /* private void HandleGameStateChanged(GameState state)
    {
        Debug.Log("The State is :" + state);
        if (turnRoutine != null)
        {
            StopCoroutine(turnRoutine);
            turnRoutine = null;
        }

        if (state == GameState.EnemyTurn)
            turnRoutine = StartCoroutine(EnemyTurnRoutine(state));
        else if (state == GameState.PlayerTurn)
             StopCoroutine(turnRoutine);

    } */

    private void HandleGameStateChanged(GameState state)
    {
        Debug.Log("HandleGameStateChanged called with state:" + state);
        ranalready = false;
        if (state == GameState.EnemyTurn)
        {
            if (turnRoutine != null) StopCoroutine(turnRoutine);
            turnRoutine = StartCoroutine(EnemyTurnRoutine());
        }

    }
    private IEnumerator EnemyTurnRoutine()
    {
        Debug.Log("enemyroutineran");
        animator?.SetTrigger("SnakeAttackTrigger");
        yield return new WaitForSeconds(attackDelay); // gives animation time to play
        BasicAttack();
        yield return new WaitForSeconds(0.5f); // optional pause after attack before handing back control

        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
        
        stopturnroutine();
        yield break;
    }


    public void TakeDamage(float damage)
    {
        HitPoints -= damage;
        healthBar.SetHealth(HitPoints, MaxHitPoints);
        if (HitPoints <= 0)
        {
            SceneManager.LoadScene("GameOver");
            Destroy(gameObject);
        }
    }

    public void BasicAttack()
    {
        Debug.Log(ranalready + "is the value of ranalready");
        if (!ranalready)
        {
            Debug.Log("enemy attack ran");
            if (health == null)
            {
                health = FindFirstObjectByType<Health3>();
            }
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
            //animator?.SetTrigger("SnakeAttackTrigger");
            ranalready = true;
        }

    }

    private void stopturnroutine()
    {
        StopCoroutine(turnRoutine);
        
    }

    
}
