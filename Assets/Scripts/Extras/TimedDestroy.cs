using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour {

    public float destroyTimer = 2f;

    public void Update()
    {
        destroyTimer -= Time.deltaTime;

        if (destroyTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
