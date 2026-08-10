using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float HitPoints;
    public float MaxHitPoints =5;
    public EnemyHealth healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("EnemyBehavior Start called! HP:" + HitPoints + "Max" + MaxHitPoints);
        HitPoints = MaxHitPoints;
        healthBar.SetHealth(HitPoints, MaxHitPoints);
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
}
