using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraView : MonoBehaviour
{
    public GameObject FirstPersonCamera;
    public GameObject ThirdPersonCamera;
    public GameObject TopDownCamera;

    // Start is called before the first frame update
    void Start()
    {
        ThirdPersonCamera.SetActive(true);

        FirstPersonCamera.SetActive(false);
        TopDownCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FirstPersonSwitch();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ThirdPersonSwitch();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TopDownSwitch();
        }
    }

    void TopDownSwitch()
    {
        TopDownCamera.SetActive(true);

        FirstPersonCamera.SetActive(false);
        ThirdPersonCamera.SetActive(false);
    }

    void FirstPersonSwitch()
    {
        FirstPersonCamera.SetActive(true);

        TopDownCamera.SetActive(false);
        ThirdPersonCamera.SetActive(false);
    }

    void ThirdPersonSwitch()
    {
        ThirdPersonCamera.SetActive(true);

        FirstPersonCamera.SetActive(false);
        TopDownCamera.SetActive(false);
    }
}
