using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Transform originPoint;
    
    public float sightDistance = 100f;

    public bool playerSeen;
    public float rotationSpeed;
    
    void Start()
    {

    }

    void Update()
    {
        if (!playerTransform)
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        else
        {
            transform.LookAt(playerTransform.position);
            playerSeen = true;
        }

        RaycastHit hit;
        if (Physics.Raycast(originPoint.position, Vector3.forward, out hit, sightDistance))
        {
            if (hit.transform.gameObject.tag == "Player")
                playerTransform = hit.transform;
        }
        else
        {
            playerTransform = null;
        }

        Vector3 dir = transform.TransformDirection(Vector3.forward) * sightDistance;
        Debug.DrawRay(originPoint.position, dir, Color.red);
    }
}
