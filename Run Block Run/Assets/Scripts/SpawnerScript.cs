using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;

    public float obstacleSpawnTime = 2f;
    [SerializeField] public float obstacleSpeed = 10f;
    private float timeUntilSpawn = 0;

    private void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timeUntilSpawn += Time.deltaTime;

        if(timeUntilSpawn >= obstacleSpawnTime)
        {
            Spawn();
            timeUntilSpawn = 0;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity); //Quaternion.identity ensures same rotation as spawner

        Rigidbody2D obstacleRb = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRb.velocity = Vector2.left * obstacleSpeed;
    }
}
