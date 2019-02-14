using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour {

    public GameObject target;

    public float accelerationSpeed = 0.1f;

    public float maxSpeed = 0.1f;

    public int health = 1;

    Rigidbody2D rb;
    // Use this for initialization

    public List<GameObject> powerUp;
    public bool guaranteeDrop;

    public bool destroy;

    public GameObject deathParticle;

    public bool aimAtPlayer = true;

    public int score;

	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        PauseScript.OnPauseEvent.AddListener(TogglePause);
	}
	
	// Update is called once per frame
	void Update () {
        if (PauseScript.paused == false)
        {

            //Get the direction to the player
            var dir = target.transform.position - transform.position;

            //Aim at the player
            if (aimAtPlayer)
            {
                var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            //Move the thingy forward
            rb.velocity += (Vector2)dir * accelerationSpeed * Time.deltaTime;

            //Clamp the velocity to the max speed
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
	}

    public void RemoveHealth(int damage)
    {
        health -= damage;
        if (health <= 0 && destroy == false)
        {
            destroy = true;
            EnemyManager.instance.enemiesAlive -= 1;
            EnemyManager.instance.enemiesKilled += 1;
            EnemyManager.instance.enemiesKilledText.text = EnemyManager.instance.enemiesKilled.ToString(); ;
            if (Random.Range(0f, 1f) > 0.9f || guaranteeDrop)
            {
                Instantiate(powerUp[Random.Range(0, powerUp.Count)], transform.position, Quaternion.identity, null);
            }
            Instantiate(deathParticle, transform.position, transform.rotation, null);

            //ScreenShake.instance.shake = 0.05f;
            ScreenShake.shakeTime = 0.05f;
            ScreenShake.shakeScreen.Invoke();

            EnemyManager.instance.IncreaseScore(score);

            Destroy(gameObject);
        }
    }

    private Vector2 savedMovementVel;
    public void TogglePause()
    {
        if (PauseScript.paused == true)
        {
            //Pause the script
            savedMovementVel = rb.velocity;
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = savedMovementVel;
        }
    }
}
