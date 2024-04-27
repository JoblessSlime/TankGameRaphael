using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalOptions : MonoBehaviour
{
    public int enemyKilled = 0;
    public int magicBalls = 0;
    public int actualLevel = SceneManager.GetActiveScene().buildIndex;

    public bool nextLevelIsBloody = false;
    public bool nextLevelIsNormal = false;
    public bool nextLevelIsPeaceful = false;

    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log(actualLevel);

       if (enemyKilled > 0 && magicBalls != 3)
        {
            nextLevelIsBloody = true;
            nextLevelIsPeaceful = false;
        }
       
       if (magicBalls == 3 && enemyKilled > 0)
        {
            nextLevelIsNormal = true;
            nextLevelIsBloody = false;
        }

       if (magicBalls == 3 && enemyKilled == 0)
        {
            nextLevelIsPeaceful = true;
            nextLevelIsBloody = false;
            nextLevelIsNormal = false;
        }
    }
}
