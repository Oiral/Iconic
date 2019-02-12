using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    #region Singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance!= this)
        {
            Destroy(this);
            Debug.LogError("Trying to create two Game Managers Detroying this one", gameObject);
        }
    }

    #endregion

    public GameObject player;

    public GameObject deathSpherePrefab;

    [Header("Managers")]
    EnemyManager enemyManager;

    [Header("UI")]
    public GameObject inGameUI;
    public GameObject deathScreen;
    public Text scoreText;

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    public void StartGameOver()
    {
        Time.timeScale = 1;
        Debug.Log("Time Scale Set [1]", gameObject);

        inGameUI.SetActive(false);
        deathScreen.SetActive(true);
        scoreText.text = enemyManager.score.ToString();
        enemyManager.gameOver = true;



        //Deactivate the player
        player.SetActive(false);

        Instantiate(deathSpherePrefab, player.transform.position, Quaternion.identity, null);

        StartCoroutine(GameOver());
        //Set the player to not active
    }

    IEnumerator GameOver()
    {
        //Set the player to not active
        yield return 0;

        //StartCoroutine(RemoveEnemies());
        StartCoroutine(RemoveBullets());
        
    }

    IEnumerator RemoveEnemies()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy != null)
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    yield return new WaitForSeconds(0);
                    enemyHealth.RemoveHealth(enemyHealth.health);
                }
            }
        }
    }

    IEnumerator RemoveBullets()
    {
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(bullet);
            yield return new WaitForSeconds(0);
        }
    }

    public void Restart()
    {
        //Set the player to active and move back to starting position

    }
}
