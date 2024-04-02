using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeSlider;
    public Transform camera;
    public Transform healthBarTransform;
    public float maxHealth;
    public float health;
    private float lerpSpeed = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthSlider.value = health * 0.01f;
        easeSlider.value = health * 0.01f;
        healthBarTransform.LookAt(healthBarTransform.position + camera.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != health * 0.01f) 
        {
            healthSlider.value = health * 0.01f;
        }

        if (easeSlider.value != health * 0.01f)
        {
            easeSlider.value = Mathf.Lerp(easeSlider.value, health * 0.01f, lerpSpeed);
        }

        if (easeSlider.value < 0.01f) 
        { 
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        healthBarTransform.LookAt(healthBarTransform.position + camera.forward);
    }
}
