using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Ui : MonoBehaviour

{
    private int enemyhealth = 45;
    // int Health;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI enemyHpText;

    private void Awake()
    {
        enemyHp();
       // changeHealth(100);
    }
    public void enemyHp()
    {


        enemyHpText.text = ("Rib Cobra Health: " + enemyhealth);

    }
   
    public void changeHealth(int health)
    {
        
        healthText.text = "Health: " + health;
    }
}
