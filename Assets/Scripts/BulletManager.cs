using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public int damages;
    public float lifeTimeLimit = 2f;
    private float lifeTime = 0f;

    //sfx
    public AudioSource audioSource;
    public AudioClip explosionEndSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= lifeTimeLimit)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("character") || collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthBarManager>().health -= damages;
        }
        if (collision.gameObject.CompareTag("destructible"))
        {
            Destroy(collision.gameObject);
        }

        // sfx
        audioSource.clip = explosionEndSFX;
        audioSource.Play();

    }
}
