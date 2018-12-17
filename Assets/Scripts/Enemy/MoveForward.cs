using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveForward : MonoBehaviour {
    
    Rigidbody2D rb;
    public float accelerationSpeed;
    public float maxSpeed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //Move the thingy forward
        if (PauseScript.paused == false)
        {
            rb.velocity += (Vector2)transform.up * accelerationSpeed * Time.deltaTime;

            //Clamp the velocity to the max speed
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}
