using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemies : MonoBehaviour
{
    public enum EnemyTypes
    {
        Chest,
        Floater,
        Slime,
        Spike,
        GrWizard,
        BlWizard
    }

    NavMeshAgent agent;
    public CheckForPlayer sight;

    public EnemyTypes currentEnemyType;
    Transform playerTransform;
    public float rotationSpeed = 3f;
    public float moveSpeed = 3f;

    public GameObject firePoint;
    public Rigidbody projectilePrefab;
    public float projectileForce = 30.0f;
    private float fireCountdown = 0.0f;
    private float fireRate = 2.0f;

    public GameObject meleePoint;
    public BoxCollider meleeATK;
    private float attackCountdown = 0.0f;
    private float attackRate = 1.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        /*firePoint = GameObject.FindGameObjectWithTag("FirePoint");
        meleePoint = GameObject.FindGameObjectWithTag("MeleePoint");*/
    }

    void Update()
    {
        agent.SetDestination(playerTransform.position);

        if (currentEnemyType == EnemyTypes.Floater || currentEnemyType == EnemyTypes.Slime || currentEnemyType == EnemyTypes.Spike)
        {
            if (agent.remainingDistance <= 2.0f)
            {
                agent.isStopped = true;
                Melee();
            }

            if (agent.remainingDistance > 2.0f)
            {
                agent.isStopped = false;
            }
        }

        if (currentEnemyType == EnemyTypes.GrWizard || currentEnemyType == EnemyTypes.BlWizard)
        {
            if (agent.remainingDistance <= 20.0f)
            {
                agent.isStopped = true;
                Fire();
            }

            if (agent.remainingDistance > 20.0f)
            {
                agent.isStopped = false;
            }
        }

        if (currentEnemyType == EnemyTypes.Chest)
        {
            agent.isStopped = true;
            
            if (agent.remainingDistance <= 15.0f)
            {
                Fire();
            }
        }
    }

    void Melee()
    {
        if (meleeATK && meleePoint)
        {
            //if (attackCountdown <= 0.0f)
            //{
                BoxCollider temp = Instantiate(meleeATK, meleePoint.transform.position, meleePoint.transform.rotation);

                attackCountdown = 1.0f / fireRate;

                Destroy(temp.gameObject, 0.5f);
           // }

            //fireCountdown -= Time.deltaTime;
        }
    }

    void Fire()
    {
        if (projectilePrefab && firePoint)
        {
            if (fireCountdown <= 0.0f)
            {
                Rigidbody temp = Instantiate(projectilePrefab, firePoint.transform.position, firePoint.transform.rotation);
                temp.AddForce(firePoint.transform.forward * projectileForce, ForceMode.Impulse);

                fireCountdown = 1.0f / fireRate;

                Destroy(temp.gameObject, 2.0f);
            }

            fireCountdown -= Time.deltaTime;
        }
    }
}