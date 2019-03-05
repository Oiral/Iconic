using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

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

    [Header("Weapon")]
    public weaponType selectedWeapon;
    public WeaponDrops normalDrops;
    public WeaponDrops trackingDrops;

    [Header("Managers")]
    EnemyManager enemyManager;
    //public Animator cameraAnimator;

    [Header("UI")]
    public GameObject inGameUI;
    public GameObject deathScreen;
    public Text scoreText;
    public Text highScoreText;

    [Header("Reset")]
    public float scoreSpinDownTime;
    public int repeats;
    [Space]
    public int multishot = 1;
    public int fireRate = 1;
    public int MovementSpeed = 5;
    public int bulletLifetime = 2;
    [Space]
    public int startingWaveNumber;
    public int startingWavePoints;
    [Space]
    public GameObject startingEnemyPrefab;
    public Vector3 startingEnemyPosition;

    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
    }

    public void StartGameOver()
    {
        DisableGamePlay();
        deathScreen.SetActive(true);
        scoreText.text = enemyManager.score.ToString();
        
        SaveSetScore();

        //Send analytics
        GetComponent<AnalyticsEventTracker>().TriggerEvent();

        

        StartCoroutine(GameOver());
        //Set the player to not active
    }

    void DisableGamePlay()
    {
        enemyManager.gameOver = true;
        inGameUI.SetActive(false);
        //Deactivate the player
        player.SetActive(false);
        Instantiate(deathSpherePrefab, player.transform.position, Quaternion.identity, null);

        Time.timeScale = 1;
        Debug.Log("Time Scale Set [1]", gameObject);
    }

    void SaveSetScore()
    {
        //Save the players score
        ScoreData data = SaveSystem.LoadScores();
        if (data != null)
        {
            data.highScores.Add(enemyManager.score);
            data.highScores.Sort();

            SaveSystem.SaveScore(data);
        }
        else
        {
            SaveSystem.SaveScore(enemyManager.score);
        }

        //Debug the current scores
        SaveSystem.DebugList(data.highScores);
        Debug.Log("High Score | " + data.highScores[data.highScores.Count - 1]);
        highScoreText.text = data.highScores[data.highScores.Count - 1].ToString();
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

    public void RunRestart()
    {
        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        inGameUI.SetActive(true);
        deathScreen.SetActive(false);

        foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("Death Sphere"))
        {
            Destroy(sphere);
        }

        //int repeats = (int)scoreSpinDownTime / 10;

        //int scoreGaps = Mathf.RoundToInt(enemyManager.score / (scoreSpinDownTime * 10));

        float scoreGaps = enemyManager.score / (scoreSpinDownTime * 10);
        Debug.Log(scoreGaps);
        for (int i = 0; i < scoreSpinDownTime * 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            enemyManager.score = (int)Mathf.Clamp(enemyManager.score - scoreGaps, 0, 9999999999);
        }

        //Spawn in the starting enemy
        StartCoroutine(StartGame());

    }

    public IEnumerator StartGame()
    {
        //cameraAnimator.SetTrigger("Move");

        foreach (GameObject sphere in GameObject.FindGameObjectsWithTag("Death Sphere"))
        {
            Destroy(sphere);
        }
        //Reset stats
        Character playerScript = player.GetComponent<Character>();
        PlayerWeapon weaponScript = player.GetComponent<PlayerWeapon>();
        weaponScript.multiShot = multishot;
        weaponScript.fireRate = fireRate;
        weaponScript.bulletLifeTime = bulletLifetime;
        playerScript.moveSpeed = MovementSpeed;
        playerScript.health = 1;

        WeaponDrops dropData = normalDrops;
        switch (selectedWeapon)
        {
            case weaponType.normal:
                dropData = normalDrops;
                break;
            case weaponType.explosive:
                break;
            case weaponType.tracking:
                weaponScript.multiShot = 5;
                dropData = trackingDrops;
                break;
            case weaponType.charge:
                break;
            default:
                dropData = normalDrops;
                break;
        }

        //Set the drops for the game
        DropManager dropMan = DropManager.instance;
        dropMan.drops = dropData.pickups;
        dropMan.guaranteedDrop = dropData.guaranteeDrop;
        //Set the player weapon
        weaponScript.selectedWeapon = dropData.weapon;


        //Reset Enemy Manager
        //Reset wave count
        enemyManager.waveCost = startingWavePoints;
        enemyManager.score = 0;
        enemyManager.waveNumber = startingWaveNumber;

        yield return new WaitForSeconds(0.5f);
        //Turn the player on
        player.SetActive(true);
        player.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.5f);
        //Turn on the UI
        inGameUI.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        //Create the initial Enemy
        Instantiate(startingEnemyPrefab, startingEnemyPosition, Quaternion.identity, null);

        enemyManager.gameOver = false;
    }

    public void CallMainMenu()
    {
        if (PauseScript.paused)
        {
            PauseScript.instance.TogglePause();
        }
        DisableGamePlay();
        MenuFunctions.instance.MainMenu();
    }

    public void CallMainMenuFromEndScreen()
    {
        MenuFunctions.instance.MainMenu();
    }
}
