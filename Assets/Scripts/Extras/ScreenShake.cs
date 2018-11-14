using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    public Vector3 startingPos;

    public static ScreenShake instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        startingPos = transform.position;
    }

    public float shake = 0;
    public float shakeAmount = 0.1f;
    float decreaseFactor = 1f;

    

    private void Update()
    {
        if (shake > 0)
        {
            transform.localPosition = startingPos + (Random.insideUnitSphere * shakeAmount);
            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0;
            transform.position = startingPos;
        }
    }
}
