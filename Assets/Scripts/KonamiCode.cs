using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : MonoBehaviour
{
    private List<string> konamiCode;
    private List<string> code;
    // Start is called before the first frame update
    void Start()
    {
        konamiCode.Add("up");
        konamiCode.Add("up");
        konamiCode.Add("down");
        konamiCode.Add("down");
        konamiCode.Add("left");
        konamiCode.Add("right");
        konamiCode.Add("left");
        konamiCode.Add("right");
        konamiCode.Add("b");
        konamiCode.Add("a");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
