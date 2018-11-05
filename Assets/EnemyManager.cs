using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public GameObject enemyPrefab;

    public float spawnRange = 5f;

    float waveNumber = 0;

    public float enemiesAlive = 0;

    private void Update()
    {
        if (enemiesAlive <= 0)
        {
            waveNumber += 1;
            for (int i = 0; i < waveNumber; i++)
            {
                SpawnEnemy();
            }
        }
    }

    public void SpawnEnemy()
    {
        enemiesAlive += 1;

        Vector2 randomPoint = Random.insideUnitCircle.normalized;
        randomPoint *= spawnRange + Random.Range(1,10);
        Instantiate(enemyPrefab, randomPoint, Quaternion.identity,null);
    }
}
