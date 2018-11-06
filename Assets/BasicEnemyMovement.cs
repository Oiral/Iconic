using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour {

    public GameObject target;

    public float accelerationSpeed = 0.1f;

    public float maxSpeed = 0.1f;

    Rigidbody2D rb;
    // Use this for initialization

    public List<GameObject> powerUp;

	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        var dir = target.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        rb.velocity += (Vector2)dir * accelerationSpeed * Time.deltaTime;

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
	}

    public void RemoveHealth(int damage)
    {
        EnemyManager.instance.enemiesAlive -= 1;
        if (Random.Range(0f,1f) > 0.9f)
        {
            Instantiate(powerUp[Random.Range(0,powerUp.Count)], transform.position, Quaternion.identity, null);
        }

        Destroy(gameObject);
    }
}
