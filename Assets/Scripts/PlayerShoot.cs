using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Importing

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;

    public float bulletSpeed = 500f;
    public float impulsion;
    private float elapsedTime;
    public float timeBetweenShoots;

    PlayerInput playerInput;
    InputAction shootingAction;

    //sfx
    public AudioSource audioSource;
    public AudioClip explosionSFX;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        shootingAction = playerInput.actions.FindAction("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float shooting = shootingAction.ReadValue<float>();
        if (shooting == 1f && elapsedTime >= timeBetweenShoots)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * impulsion);

            elapsedTime = 0f;

            // sfx
            audioSource.clip = explosionSFX;
            audioSource.Play();
        }
    }
}
