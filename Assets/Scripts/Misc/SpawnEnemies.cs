using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    void Start()
    {
        try
        {
            Instantiate(enemyPrefabs[Random.Range(0, 6)], transform.position, transform.rotation);

            if (enemyPrefabs.Length > 6)
            {
                throw new MissingComponentException("Not enough enemy prefabs to spawn");
            }
        }

        catch (MissingComponentException e)
        {
            Debug.Log(e.Message);
        }

        finally
        {
            Debug.Log("Enemy prefab validation always runs");
        }
    }
}
