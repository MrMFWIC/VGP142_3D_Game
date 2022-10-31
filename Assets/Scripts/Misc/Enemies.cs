using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Rigidbody rb;

    public EnemyTypes currentEnemyType;
    Transform playerTransform;
    public float rotationSpeed = 3f;
    public float moveSpeed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }

    public void OnBecameVisible()
    {
         
    }
    public void OnBecameInvisible()
    {

    }
}
