using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 7f;

	// Update is called once per frame
	void Update () {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position,transform.position) > 30)
        {
            Destroy(gameObject);
        }
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
