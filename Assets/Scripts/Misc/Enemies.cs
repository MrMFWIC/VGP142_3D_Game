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
    
    public enum EnemyState
    {
        Chase, 
        Patrol
    }

    NavMeshAgent agent;
    public CheckForPlayer sight;

    public EnemyState currentState;
    public GameObject[] path;
    public int pathIndex;
    public float distThreshold;

    public EnemyTypes currentEnemyType;
    Transform playerTransform;
    public float rotationSpeed = 3f;
    public float moveSpeed = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        /*playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (path.Length <= 0)
        {
            path = GameObject.FindGameObjectsWithTag("PatrolNode");
        }

        if (currentState == EnemyState.Chase)
        {
            target = GameObject.FindGameObjectWithTag("Player");

            if (target)
                agent.SetDestination(target.transform.position);
        }

        if (distThreshold <= 0)
        {
            distThreshold = 0.5f;
        }*/
    }

    void Update()
    {
        /*transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        
        if (currentState == EnemyState.Patrol)
        {
            if (target)
                Debug.DrawLine(transform.position, target.transform.position, Color.red);

            if (agent.remainingDistance < distThreshold)
            {
                pathIndex++;
                pathIndex %= path.Length;

              target = path[pathIndex];
            }
        }

        if (currentState == EnemyState.Chase)
        {
            if (target.CompareTag("PatrolNode"))
                target = GameObject.FindGameObjectWithTag("Player");
        }

        if (target)
            agent.SetDestination(target.transform.position);*/

        if (sight.playerSeen)
        {
            agent.SetDestination(playerTransform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            //Destroy(gameObject);
        }
    }
}