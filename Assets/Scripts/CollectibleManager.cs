using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public int points = 0;

    void OnTriggerEnter(Collider objetDeTrigger)
    {
        if (objetDeTrigger.gameObject.CompareTag("collectible"))
        {
            points++;

            Destroy(objetDeTrigger.gameObject);
            // Could also be     objetDeTrigger.gameObject.SetActive(false);
        }
    }
}
