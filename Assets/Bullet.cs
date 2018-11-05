using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 7f;

	// Update is called once per frame
	void Update () {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Boom");
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.tag == "Enemy")
        {
            collidedObject.GetComponent<BasicEnemyMovement>().RemoveHealth(1);
        }

        //Spawn in bullet removed particle
        Destroy(gameObject);
    }
}
