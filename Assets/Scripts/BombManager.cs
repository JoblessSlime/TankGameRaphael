using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public float damages = 10;

    // Could Be OnTriggerEnter
    private void OnCollisionEnter(Collision objetDeCollision)
    {
        Debug.Log("CollisionDetected");
        if (objetDeCollision.gameObject.CompareTag("character"))
        {
            Debug.Log("explosionDetected");
            Destroy(this.gameObject);
            // Could also be     this.gameObject.SetActive(false);

            objetDeCollision.gameObject.GetComponent<HealthBarManager>().health -= damages;
        }
    }
}
