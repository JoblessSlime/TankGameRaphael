using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Importing UnityEngine.AI
using UnityEngine.AI;
using UnityEngine.Audio;

public class TankAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float patrolTimeLimit;  // This is mostly for debugging if AI is stuck with a walkpoint
    private float patrolTime;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Shoot (attacks)
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 1f;

    // Wheels rotation
    public float wheelRotationSpeed; // in degrees
    public Transform wheelATransform, wheelBTransform, wheelCTransform, wheelDTransform;

    // Audio
    public AudioSource audioSrcEnemy;
    public AudioClip shootSFX;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) 
        {
            Patroling();
            // Rotate wheels
            wheelATransform.Rotate(1f * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelBTransform.Rotate(1f * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelCTransform.Rotate(1f * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelDTransform.Rotate(1f * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
            // Rotate wheels
            wheelATransform.Rotate(1f * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelBTransform.Rotate(1f * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelCTransform.Rotate(1f * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
            wheelDTransform.Rotate(1f * wheelRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
        }

        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
            patrolTime = 0f;
        }

        else
        {
            agent.SetDestination(walkPoint);
        }

        patrolTime += Time.deltaTime;
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f || patrolTime >= patrolTimeLimit)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer() 
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * 2000f);

        // sfx
        audioSrcEnemy.clip = shootSFX;
        audioSrcEnemy.Play();
    }
}
