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
    private bool attack;

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
    attack = true;
    if (state == GameState.EnemyTurn)
    {   
        if (turnRoutine != null) StopCoroutine(turnRoutine);
        turnRoutine = StartCoroutine(EnemyTurnRoutine());
    }

} 

    private IEnumerator EnemyTurnRoutine()
    {
        int timesran = 0;
        while (attack)
        { 
            timesran += 1;
            Debug.Log("Attack inside EnemyTurnRoutine is:" + attack);
            Debug.Log("coroutine ran" + timesran + "times");
         yield return new WaitForSeconds(attackDelay); // gives animation time to play
         BasicAttack();
        yield return new WaitForSeconds(0.5f); // optional pause after attack before handing back control
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);
        }


    }
  /*  private void EnemyTurnRoutine()
    {
        
        
         StartCoroutine(Timer(attackDelay));

         BasicAttack();

        StartCoroutine(Timer(0.5f));
        GameManager.Instance.UpdateGameState(GameState.PlayerTurn);

    }

    private IEnumerator Timer(float time)
    {
        float timer = 0f;
        if(timer < time)
        {
            timer += Time.deltaTime;
        }
       yield break;

    }
*/



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
        Debug.Log("enemy attack ran");
        if (health == null)
        {
            health = FindFirstObjectByType<Health3>();
        }
        if (health != null)
        {
            health.TakeDamage(attackDamage);
        }
         animator?.SetTrigger("SnakeAttackTrigger");
         stop();
         //attack = false;
        //  Debug.Log("attack is: " + attack );
    }

    private void stop()
    {
        
        attack = false;
        Debug.Log("Stop!!!! " + attack);
        StopCoroutine(turnRoutine);
    }

    
}
