using UnityEngine;

public class CardProjectile : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 controlPoint;
    private Vector3 targetPos;
    private float speed = 1f;
    private float t = 0f;
    private int damage;
    private bool isLaunched = false;

    public void Launch(Vector3 start, Vector3 control, Vector3 target, int cardDamage)
    {
        startPos = start;
        controlPoint = control;
        targetPos = target;
        damage = cardDamage;
        isLaunched = true;
    }

    void Update()
    {
        if (!isLaunched) return;

        // Move along the bezier curve
        t += Time.deltaTime * speed;
        t = Mathf.Clamp01(t);
        
        transform.position = QuadraticBezierPoint(startPos, controlPoint, targetPos, t);

        Debug.Log("Projectile position: " + transform.position + ", t: " + t);
        Debug.Log("Target position: " + targetPos);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D hit in hits)
        {
            Debug.Log("Found collider:" + hit.gameObject.name);
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log("Hit an Enemy! Dealing " + damage + " damage");
                EnemyBehavior enemy = hit.GetComponent<EnemyBehavior>();
                if (enemy != null) enemy.TakeDamage(damage);
                Destroy(gameObject);
                return;
           }
        }
        if (t >= 1f)
        {
            Debug.Log("Projectile reached the target position without hitting an enemy.");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isLaunched) return;
        Debug.Log("Hit something" + other.gameObject.name);
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit an enemy! Dealing " + damage + " damage.");
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            if (enemy == null) Debug.Log("EnemyBehavior not found!");
            else enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    Vector3 QuadraticBezierPoint(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 point = uu * start;
        point += 2 * u * t * control;
        point += tt * end;
        return point;
    }
}