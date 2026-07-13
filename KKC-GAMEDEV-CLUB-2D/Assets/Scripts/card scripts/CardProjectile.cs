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

        // Calculate position on arc
        transform.position = QuadraticBezierPoint(startPos, controlPoint, targetPos, t);

        // When it reaches the target
        if (t >= 1f)
        {
            Debug.Log("Card dealt " + damage + " damage!");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit" + other.gameObject.name + "for" + damage + "damage");
            //TODO: uncomment when one of you fuckers finish making the health script
            //other.Getcomponent<EnemyHealth>().TakeDamage(Damage);
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