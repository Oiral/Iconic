using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance;
    
    [System.Serializable]
    public class Wave
    {
        public string name;
        
        [System.Serializable]
        public class EnemySpawns
        {
            public string name;
            public GameObject enemyPrefab;
            public int amount;
        }

        public EnemySpawns[] enemies;

        public float rate;

    }

    public Wave[] waves;

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

    public List<GameObject> enemyPrefabs;
    public List<int> enemyCosts;

    public int waveCost = 1;

    public float spawnRange = 5f;

    public int waveNumber = 0;

    public float enemiesAlive = 0;

    public int enemiesKilled;
    public Text enemiesKilledText;

    public int score;
    public Text scoreText;

    public bool gameOver = false;

    private void Update()
    {
        scoreText.text = score.ToString();
        if (gameOver == false)
        {
            if (enemiesAlive <= 0)
            {
                //Check if there are no more left
                if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
                {
                    waveNumber += 1;
                    waveCost += waveNumber;
                    SpawnWave(waveCost);
                }
                else
                {
                    enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
                }
            }

            
        }
    }

    public void IncreaseScore(int amount)
    {
        if (gameOver == false)
        {
            score += amount;
        }
    }

    public void SpawnWave(int budget)
    {
        while (budget > 0)
        {
            //pick a random thing
            int num = 0;

            float value = Random.value;

            if (value > 0.9f)
            {
                num = 1;
            }
            else if (value > 0.7f)
            {
                num = 2;
            }
            else if (value > 0.5f)
            {
                num = 3;
            }

            if (budget >= enemyCosts[num])
            {
                budget -= enemyCosts[num];
                SpawnEnemy(num);
            }
        }
    }

    public void SpawnEnemy(int enemyToSpawn)
    {
        enemiesAlive += 1;

        Vector2 randomPoint = Random.insideUnitCircle.normalized;
        randomPoint *= spawnRange + Random.Range(1,10);
        Instantiate(enemyPrefabs[enemyToSpawn], randomPoint, Quaternion.identity,null);
    }
}
