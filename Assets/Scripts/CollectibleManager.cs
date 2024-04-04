using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public float bonusHealth = 10;

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
        }
    }
}
