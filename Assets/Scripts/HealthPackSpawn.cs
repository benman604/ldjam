using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthPackSpawner : MonoBehaviour
{
    public GameObject HealthPack;  // The prefab for the health pack
    public int numberOfHealthPacks = 10;  // Number of health packs to spawn
    public float spawnRadius = 50f;      // Radius in which to spawn the health packs
    public LayerMask groundLayer;        // Layer for ground if needed to filter collisions

    void Awake()
    {
        SpawnHealthPacks();
    }

    void SpawnHealthPacks()
    {
        for (int i = 0; i < numberOfHealthPacks; i++)
        {
            Vector3 randomPosition = GetRandomNavMeshPosition();
            if (randomPosition != Vector3.zero)
            {
                Instantiate(HealthPack, randomPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += transform.position;  // Offset from the center of the map

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, spawnRadius, NavMesh.AllAreas))
        {
            // The hit position is valid on the nav mesh
            return hit.position;
        }

        // If it couldn't find a position on the NavMesh, return zero and try again
        return Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
