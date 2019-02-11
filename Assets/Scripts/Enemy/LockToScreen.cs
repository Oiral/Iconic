using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToScreen : MonoBehaviour {

    public float min = 0.01f;
    public float max = 0.99f;

    public bool locked = false;

	// Update is called once per frame
	void Update () {
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        
        //if we are inside the screen and locked is false locked is true
        if (!locked)
        {
            //if we are inside the screen bounds
            if ((pos.x > min && pos.x < max) && pos.y > min && pos.y < max)
            {
                locked = true;
            }
        }


        if (locked)
        {
            
            pos.x = Mathf.Clamp(pos.x, min, max);
            pos.y = Mathf.Clamp(pos.y, min, max);
            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }
}
