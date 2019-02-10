using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenShake : MonoBehaviour {

    public Vector3 startingPos;

    public bool moveZ = true;
    //public static ScreenShake instance;
    public static UnityEvent shakeScreen = new UnityEvent();
    
    private void Awake()
    {
        /*
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }*/
        startingPos = transform.position;
        shakeScreen.AddListener(StartShake);
    }

    public float shake = 0;
    public static float shakeTime = 0.5f;
    public float shakeAmount = 0.1f;
    float decreaseFactor = 1f;
    
    public void StartShake()
    {
        shake = shakeTime;
    }

    private void Update()
    {
        if (shake > 0)
        {
            Vector3 pos = startingPos + (Random.insideUnitSphere * shakeAmount);
            if (moveZ == false)
            {
                pos.z = 0;
            }
            transform.position = pos;

            shake -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shake = 0;
            transform.position = startingPos;
        }
    }
}
