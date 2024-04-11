using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleManager : MonoBehaviour
{
    public float bonusHealth = 10;
    private bool RuneStoneTouched = false;

    // sfx
    public AudioSource audioSource;
    public AudioClip healthSFX;
    public AudioClip MagicSFX;
    public AudioClip runeStoneSFX;
    public AudioClip portalTouchedSFX;

    // Script
    [SerializeField] private PortalOptions portalOptions;

    // Could Be OnTriggerEnter
    private void OnCollisionEnter(Collision objetDeCollision)
    {
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
        else if (objetDeCollision.gameObject.CompareTag("MagicCollectible"))
        {
            Debug.Log("MagicDetected");
            Destroy(objetDeCollision.gameObject);
            portalOptions.magicBalls += 1;

            // sfx
            audioSource.clip = MagicSFX;
            audioSource.Play();
        }

        // Portals
        else if (objetDeCollision.gameObject.CompareTag("portalRed"))
        {
            Debug.Log("portalDetected");
            portalOptions.magicBalls = 0;
            SceneManager.LoadScene("Level2");

            // sfx
            audioSource.clip = portalTouchedSFX;
            audioSource.Play();
        }
        else if (objetDeCollision.gameObject.CompareTag("portalBlue"))
        {
            Debug.Log("portalDetected");
            portalOptions.magicBalls = 0;
            SceneManager.LoadScene("Level2");

            // sfx
            audioSource.clip = portalTouchedSFX;
            audioSource.Play();
        }
        else if (objetDeCollision.gameObject.CompareTag("portalGreen"))
        {
            Debug.Log("portalDetected");
            portalOptions.magicBalls = 0;
            SceneManager.LoadScene("Level2");

            // sfx
            audioSource.clip = portalTouchedSFX;
            audioSource.Play();
        }

        // RuneStone
        if (objetDeCollision.gameObject.CompareTag("runeStone"))
        {
            Debug.Log("RuneStoneDetected");
            RuneStoneTouched = true;

            // Creating Portal
            if (portalOptions.nextLevelIsBloody)
            {
                Debug.Log("bloody");
                GameObject portal = objetDeCollision.transform.GetChild(0).gameObject;
                portal.SetActive(true);
            }
            else if (portalOptions.nextLevelIsNormal)
            {
                Debug.Log("normal");
                GameObject portal = objetDeCollision.transform.GetChild(1).gameObject;
                portal.SetActive(true);
            }
            else if (portalOptions.nextLevelIsPeaceful)
            {
                Debug.Log("peaceful");
                GameObject portal = objetDeCollision.transform.GetChild(2).gameObject;
                portal.SetActive(true);
            }

            // sfx
            audioSource.clip = runeStoneSFX;
            audioSource.Play();
        }
        else
        {
            RuneStoneTouched = false;
        }
    }
}
