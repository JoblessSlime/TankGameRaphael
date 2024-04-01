using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeSlider;
    public float maxHealth;
    public float health;
    private float lerpSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != health) 
        {
            healthSlider.value = health;
        }
        if (healthSlider.value != easeSlider.value)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, health, lerpSpeed);
        }
    }
}
