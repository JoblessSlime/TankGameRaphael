using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public float damages = 10;

    //sfx
    public AudioSource audioSource;
    public AudioClip explosionSFX;

    // vfx
    public GameObject explosionVFX;

    // Could Be OnTriggerEnter
    private void OnCollisionEnter(Collision objetDeCollision)
    {
        if (objetDeCollision.gameObject.CompareTag("character") || objetDeCollision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            var vfx = Instantiate(explosionVFX, transform.position, transform.rotation);
            // Could also be     this.gameObject.SetActive(false);

            // sfx
            audioSource.clip = explosionSFX;
            audioSource.Play();

            objetDeCollision.gameObject.GetComponent<HealthBarManager>().health -= damages;
        }
    }
}
