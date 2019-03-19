using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMoveBack : MonoBehaviour {

    EnemyManager manager;

    Camera cam;

    float startingSize;

    private void Start()
    {
        cam = GetComponent<Camera>();
        manager = EnemyManager.instance;
        startingSize = cam.orthographicSize;
    }


    // Update is called once per frame
    void Update () {
        cam.orthographicSize = startingSize + ((float)manager.score / 1000f);
	}
}
