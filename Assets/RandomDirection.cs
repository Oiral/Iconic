using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirection : MonoBehaviour {

    public float timeTillNextTurn;

    bool randomDirectionStarted = false;

    private void Update()
    {
        if (randomDirectionStarted == false)
        {
            CheckIfCanStart();
        }

        ChangeDirection();
    }

    void CheckIfCanStart()
    {
        if (GetComponent<LockToScreen>() != null)
        {
            if (GetComponent<LockToScreen>().locked == false)
            {
                //look at the center of the screen
                transform.LookAt(Vector3.zero, Vector3.forward);
                transform.Rotate(new Vector3(90, 0, 0));
            }
            else
            {
                randomDirectionStarted = true;
            }
        }
        else
        {
            randomDirectionStarted = true;
        }
    }

    void ChangeDirection()
    {
        //transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.Rotate(new Vector3(0, 0, Random.Range(-360, 360) * Time.deltaTime));
    }
}
