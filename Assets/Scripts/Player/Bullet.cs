using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 7f;

    public weaponType bulletType = weaponType.normal;

    TrailRenderer trail;

    float trailLength;

    float randomDistance;

    public bool lerpToTarget;
    [Range(0,10)]
    public float lerpAmount = 0.5f;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        PauseScript.OnPauseEvent.AddListener(OnPause);
        randomDistance = Random.Range(0.5f, 1.5f);
        randomDistance = 0;
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
        if (bulletType == weaponType.tracking)
        {
            Vector3 camPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Aim at the mouse
            float AngleRad = Mathf.Atan2(camPoint.y - transform.position.y, camPoint.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object

            camPoint.z = 0;

            Quaternion targetRot = Quaternion.identity;

            if (Vector3.Distance(camPoint, transform.position) > randomDistance)
            {
                targetRot = Quaternion.Euler(0, 0, AngleDeg - 90);
            }
            else
            {
                targetRot = Quaternion.Euler(0, 0, AngleDeg);
            }

            if (lerpToTarget == true)
            {
                //Lerp to the target Rotation
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRot, lerpAmount * Time.deltaTime);
            }
            else
            {
                this.transform.rotation = targetRot;
            }

            //Debug.Log(Vector3.Distance(camPoint, transform.position));
            
        }

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
            collidedObject.GetComponent<EnemyHealth>().RemoveHealth(1);
        }

        //Spawn in bullet removed particle
        Destroy(gameObject);
    }
    
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
