using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.checkpoint)
        {
            ParticleSystem.MainModule settings = GetComponent<ParticleSystem>().main;
            settings.startColor = new ParticleSystem.MinMaxGradient(Color.green);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.checkpoint = true;
            Debug.Log("Checkpoint reached");
        }
    }
}
