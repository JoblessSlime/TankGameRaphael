using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalOptions : ScriptableObject
{
    public int enemyKilled = 0;
    public int BlueBalls = 0;
    public int actualLevel = 0;
    public bool NextLevelIsBloody = false;
    public bool NextLevelIsNormal = false;
    public bool NextLevelIsPeaceful = false;

    // Update is called once per frame
    void LateUpdate()
    {
       if (enemyKilled > 0 && BlueBalls != 0)
        {
            NextLevelIsBloody = true;
            NextLevelIsPeaceful = false;
        }
       
       if (BlueBalls == 3 && enemyKilled > 0)
        {
            NextLevelIsNormal = true;
            NextLevelIsBloody = false;
        }

       if (BlueBalls == 3 && enemyKilled == 0)
        {
            NextLevelIsPeaceful = true;
            NextLevelIsBloody = false;
            NextLevelIsNormal = false;
        }
    }
}
