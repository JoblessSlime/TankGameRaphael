using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleManager : MonoBehaviour
{
    public float bonusHealth = 10;

    //sfx
    public AudioSource audioSource;
    public AudioClip healthSFX;
    public AudioClip portalTouchedSFX;

    // Could Be OnTriggerEnter
    private void OnCollisionEnter(Collision objetDeCollision)
    {
        Debug.Log("CollisionDetected");
        if (objetDeCollision.gameObject.CompareTag("HealthCollectible"))
        {
            Debug.Log("HealthDetected");
            Destroy(objetDeCollision.gameObject);
            // Could also be     objetDeTrigger.gameObject.SetActive(false);

            float healthChanger = this.gameObject.GetComponent<HealthBarManager>().health;
            healthChanger += bonusHealth;
            healthChanger = Mathf.Clamp(healthChanger, 0, 100);
            this.gameObject.GetComponent<HealthBarManager>().health = healthChanger;

            // sfx
            audioSource.clip = healthSFX;
            audioSource.Play();
        }
        else if (objetDeCollision.gameObject.CompareTag("portal1"))
        {
            Debug.Log("portalDetected");

            // sfx
            audioSource.clip = portalTouchedSFX;
            audioSource.Play();

            SceneManager.LoadScene("Level2");
        }
    }
}
