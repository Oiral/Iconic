using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    public GameObject target;

	// Use this for initialization
	void Start () {
		if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (PauseScript.paused == false)
        {
            var dir = target.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
	}
}
