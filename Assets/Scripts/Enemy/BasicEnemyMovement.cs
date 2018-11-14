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

    public bool destroy;

    public GameObject deathParticle;

    public bool aimAtPlayer = true;

    public int score;

	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        var dir = target.transform.position - transform.position;

        if (aimAtPlayer)
        {
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        rb.velocity += (Vector2)dir * accelerationSpeed * Time.deltaTime;

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
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
            if (Random.Range(0f, 1f) > 0.9f)
            {
                Instantiate(powerUp[Random.Range(0, powerUp.Count)], transform.position, Quaternion.identity, null);
            }
            Instantiate(deathParticle, transform.position, transform.rotation, null);

            ScreenShake.instance.shake = 0.05f;

            EnemyManager.instance.score += score;

            Destroy(gameObject);
        }
    }
}
