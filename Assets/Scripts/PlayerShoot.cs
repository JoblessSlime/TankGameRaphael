using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 500f;

    PlayerInput playerInput;
    InputAction shootingAction;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        shootingAction = playerInput.actions.FindAction("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        float shooting = shootingAction.ReadValue<float>();
        if (shooting == 1f)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * 6000f);
        }
    }
}
