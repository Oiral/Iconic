using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class saveVelocityOnPause : MonoBehaviour {

    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        PauseScript.OnPauseEvent.AddListener(TogglePause);
        rb = GetComponent<Rigidbody2D>();
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
