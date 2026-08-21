using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float HitPoints;
    public float MaxHitPoints =5;
    public EnemyHealth healthBar;
    public int attackDamage;
    private Health3 health;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("EnemyBehavior Start called! HP:" + HitPoints + "Max" + MaxHitPoints);
        HitPoints = MaxHitPoints;
        healthBar.SetHealth(HitPoints, MaxHitPoints);
       health= FindFirstObjectByType<Health3>();
       // animator.SetTrigger("SnakeAttackTrigger");
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
    }
}
