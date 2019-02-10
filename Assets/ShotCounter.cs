using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCounter : MonoBehaviour {

    public GameObject image;
    public float numberOfIcons;


    private void OnGUI()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Character player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

            if (player != null)
            {
                if (numberOfIcons < player.multiShot)
                {
                    Instantiate(image, this.transform);
                    numberOfIcons += 1;
                }
            }
        }
    }
}
