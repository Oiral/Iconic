using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirection : MonoBehaviour {

    public float moveSpeed;
    public Vector3 dir;
    public float turnSpeed;
    float targetAngle;
    Vector3 currentPos;
    bool play = true;
    Vector3 direction;

    void Start()
    {
        dir = Vector3.up;
        InvokeRepeating("Start1", Random.Range(0f,2f), 2f);
    }

    void Start1()
    {
        play = true;
        direction = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height))); //random position in x and y
    }

    void Update()
    {
        if (PauseScript.paused == false)
        {
            currentPos = transform.position;//current position of gameObject
            if (play)
            { //calculating direction
                dir = direction - currentPos;

                dir.z = 0;
                dir.Normalize();
                play = false;
            }
            Vector3 target = dir * moveSpeed + currentPos;  //calculating target position
            transform.position = Vector3.Lerp(currentPos, target, Time.deltaTime);//movement from current position to target position
            targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90; //angle of rotation of gameobject
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), turnSpeed * Time.deltaTime); //rotation from current direction to target direction
        }
    }
}
