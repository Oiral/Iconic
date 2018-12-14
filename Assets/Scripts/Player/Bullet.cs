using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 7f;

    TrailRenderer trail;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        PauseScript.OnPauseEvent.AddListener(OnPause);
    }

    // Update is called once per frame
    void Update () {
        if (PauseScript.paused == false)
        {
            transform.position += transform.up * bulletSpeed * Time.deltaTime;
        }
        /*
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position,transform.position) > 30)
        {
            Destroy(gameObject);
        }*/
        /*
        if (render.isVisible)
        {
            Debug.Log("Destroying bullet");
            Destroy(gameObject);
        }
        */
	}

    private void OnBecameInvisible()
    {
        //Debug.Log("Destroying bullet");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Boom");
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.tag == "Enemy")
        {
            collidedObject.GetComponent<BasicEnemyMovement>().RemoveHealth(1);
        }

        //Spawn in bullet removed particle
        Destroy(gameObject);
    }

    float trailLength;
    void OnPause()
    {
        if (PauseScript.paused)
        {
            //pause the bullet
            trailLength = trail.time;
            trail.time = float.PositiveInfinity;
        }
        else
        {
            //unpause the bullet
            trail.time = trailLength;
        }
    }
}
