
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] CollisionChecker collisionChecker;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] int enemiesInMap = 0;
    [SerializeField] Tilemap tilemap;
    bool coroutineStarted = false;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    void Update()
    {
        //if we want the number of enemiesInMap to be numberOfEnemies at all times (need to update destory of enemy)
        //if (enemiesInMap < numberOfEnemies && !coroutineStarted)
        //{
        //    StartCoroutine(SpawnEnemies());
        //}
    }

    private IEnumerator SpawnEnemies()
    {
        coroutineStarted = true;
        var boundsMin = new Vector2(tilemap.cellBounds.xMin + 5f, tilemap.cellBounds.yMin + 5f);
        var boundsMax = new Vector2(tilemap.cellBounds.xMax - 5f, tilemap.cellBounds.yMax - 5f);
        do
        {
            var spawnPlace = new Vector3(Random.Range(boundsMin.x, boundsMax.x), Random.Range(boundsMin.y, boundsMax.y), 0);

            collisionChecker.transform.position = spawnPlace;
            collisionChecker.gameObject.SetActive(true);
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
        coroutineStarted = false;
    }
}

