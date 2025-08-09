
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool keepSpawningEnemies;
    [SerializeField] GameObject enemy;
    [SerializeField] CollisionChecker collisionChecker;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] int enemiesInMap = 0;
    [SerializeField] Tilemap tilemap;
    Coroutine spawnEnemies;
    Vector2 boundsMin;
    Vector2 boundsMax;

    private void Start()
    {
        boundsMin = new Vector2(tilemap.cellBounds.xMin + 5f, tilemap.cellBounds.yMin + 5f);
        boundsMax = new Vector2(tilemap.cellBounds.xMax - 5f, tilemap.cellBounds.yMax - 5f);
        spawnEnemies = StartCoroutine(SpawnEnemiesWithDelay());
    }
    void Update()
    {
        if (enemiesInMap < numberOfEnemies && spawnEnemies == null
            && keepSpawningEnemies)
        {
            spawnEnemies = StartCoroutine(SpawnEnemiesWithDelay());
        }
    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        do
        {
            Vector3 spawnPlace = PlaceCollisionChecker();
            yield return new WaitForSeconds(0.2f);

            if (collisionChecker.collisionDetected)
            {
                yield return new WaitForSeconds(2f);
                collisionChecker.collisionDetected = false;
            }
            else
            {
                collisionChecker.gameObject.SetActive(false);
                Instantiate(enemy, spawnPlace, Quaternion.identity);
                enemiesInMap++;
            }
        }
        while (enemiesInMap < numberOfEnemies);
        spawnEnemies = null;
    }

    public void SpawnEnemeis()
    {
        if (enemiesInMap > numberOfEnemies)
            return;
        do
        {
            Vector3 spawnPlace = PlaceCollisionChecker();

            if (collisionChecker.collisionDetected)
            {
                collisionChecker.collisionDetected = false;
            }
            else
            {
                collisionChecker.gameObject.SetActive(false);
                Instantiate(enemy, spawnPlace, Quaternion.identity);
                enemiesInMap++;
            }
        }
        while (enemiesInMap < numberOfEnemies);
        spawnEnemies = null;
    }

    private Vector3 PlaceCollisionChecker()
    {
        var spawnPlace = new Vector3(Random.Range(boundsMin.x, boundsMax.x),
                        Random.Range(boundsMin.y, boundsMax.y), 0);
        collisionChecker.transform.position = spawnPlace;
        collisionChecker.gameObject.SetActive(true);
        return spawnPlace;
    }
}