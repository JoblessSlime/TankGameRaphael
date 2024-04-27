using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public int damages;
    public float lifeTimeLimit = 2f;
    private float lifeTime = 0f;

    // vfx
    public GameObject explosionVFX;


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
        var vfx = Instantiate(explosionVFX, transform.position, transform.rotation);

        if (collision.gameObject.CompareTag("character") || collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthBarManager>().health -= damages;
        }
        if (collision.gameObject.CompareTag("destructible"))
        {
            Destroy(collision.gameObject);
        }
    }
}
