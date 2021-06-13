using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private float spawnTime;
    private Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = new Vector3(Random.Range(5f, 95f), 1f, Random.Range(4f, 95f));
        LeanPool.Spawn(enemies[0], new Vector3(95, 1, 25), Quaternion.identity);
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            spawnPosition = new Vector3(Random.Range(-1.5f, -100f), 1f, Random.Range(0.5f, 100f));
            LeanPool.Spawn(enemies[Random.Range(0, enemies.Length - 1)], spawnPosition, Quaternion.identity);
        }
    }
}
