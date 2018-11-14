using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public List<GameObject> enemyPrefabs;
    public List<int> enemyCosts;

    public int waveCost;

    public float spawnRange = 5f;

    public float waveNumber = 0;

    public float enemiesAlive = 0;

    public int enemiesKilled;
    public Text enemiesKilledText;

    public int score;
    public Text scoreText;

    private void Update()
    {
        if (enemiesAlive <= 0)
        {
            //Check if there are no more left
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                waveNumber += 1;
                waveCost = (int)waveNumber;

                while (waveCost > 0)
                {
                    //pick a random thing
                    int num = 0;

                    float value = Random.value;

                    if (value > 0.9f)
                    {
                        num = 1;
                    } else if (value > 0.7f)
                    {
                        num = 2;
                    }

                    if (waveCost >= enemyCosts[num])
                    {
                        waveCost -= enemyCosts[num];
                        SpawnEnemy(num);
                    }
                }
            }
            else
            {
                enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
            }
        }

        scoreText.text = score.ToString();
    }

    public void SpawnEnemy(int enemyToSpawn)
    {
        enemiesAlive += 1;

        Vector2 randomPoint = Random.insideUnitCircle.normalized;
        randomPoint *= spawnRange + Random.Range(1,10);
        Instantiate(enemyPrefabs[enemyToSpawn], randomPoint, Quaternion.identity,null);
    }
}
