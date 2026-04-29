using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour{
    private static bool UIManagerExists;

    public Image HpBar;
    public Health3 PlayHealth; //PlayerHealth is the script that manages player health. Needs to return health value.
    float HealthLerp;
    [SerializeField] private float testHP;

    void Start()
    {
        if (!UIManagerExists)
        {
            UIManagerExists = true;
            DontDestroyOnLoad(this.gameObject);
        }
        else { Destroy(gameObject); }

        PlayHealth = GameObject.Find("Player").GetComponent<Health3>();

    }


    // Update is called once per frame
    void Update()
    {
        HealthLerp = 10f * Time.deltaTime;
        FillHpBar();
        testHP = (float)PlayHealth.health/(float)PlayHealth.maxHealth;
    }

    void FillHpBar()
    {
        if (!PlayHealth)
        {
            PlayHealth = GameObject.Find("Player").GetComponent<Health3>();
        }
        HpBar.fillAmount = (float)PlayHealth.health / (float)PlayHealth.maxHealth;
    }

}
