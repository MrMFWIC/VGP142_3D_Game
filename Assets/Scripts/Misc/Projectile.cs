using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool collided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Projectile" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            Destroy(gameObject);
        }
    }
}
