using UnityEngine;
using UnityEngine.SceneManagement;

public class Health3 : MonoBehaviour
{
    private Ui uiCenter;
    [SerializeField] public int health;
    [SerializeField] ParticleSystem particleSys;
    public int maxHealth = 50;
    //CameraShake cShake;
    
    

    void Awake()
    {

        health = maxHealth;
        //new
        uiCenter = FindFirstObjectByType<Ui>();
     
        // new
        UpdateHealth(this.tag);
        //cShake = FindFirstObjectByType<CameraShake>();
       
    }

  

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null )
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
        }
        UpdateHealth(this.tag);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (this.tag == "Player")
        {
            
        }
        if (health <= 0)
        {
            Debug.Log(this.tag);
            if (this.tag == "Enemy")  //New
            {

                uiCenter.enemyHp();
            }
            if (particleSys != null)
            {
                ParticleSystem pSys = Instantiate(particleSys, this.transform.position, Quaternion.identity);
                Destroy(pSys, 1f);
            }
            if (this.tag == "Player")
            {
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
                Destroy(gameObject);
        }
        
    }

    void UpdateHealth(string tag)
    {
        
        if (tag == "Player")
        {
            uiCenter.changeHealth(health);
        }
    }


}
