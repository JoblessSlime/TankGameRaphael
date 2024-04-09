using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public float damages = 10;

    //sfx
    public AudioSource audioSource;
    public AudioClip explosionSFX;

    // Could Be OnTriggerEnter
    private void OnCollisionEnter(Collision objetDeCollision)
    {
        Debug.Log("CollisionDetected");
        if (objetDeCollision.gameObject.CompareTag("character") || objetDeCollision.gameObject.CompareTag("Player"))
        {
            Debug.Log("explosionDetected");
            Destroy(this.gameObject);
            // Could also be     this.gameObject.SetActive(false);

            // sfx
            audioSource.clip = explosionSFX;
            audioSource.Play();

            objetDeCollision.gameObject.GetComponent<HealthBarManager>().health -= damages;
        }
    }
}
