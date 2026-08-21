using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;

    public void SetHealth(float health, float maxHealth)
    {

        Slider.gameObject.SetActive(true);
        Slider.maxValue = maxHealth;
        Slider.value = health;

        Color newColor = Color.Lerp(Low, High, Slider.normalizedValue);
        Debug.Log("Setting health bar color to: " + newColor);
    }

    void Update()
    {
        transform.position = transform.parent.position + new Vector3(0, 0.1f, 0);
    }
}
