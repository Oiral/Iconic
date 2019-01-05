using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCounter : MonoBehaviour {

    public GameObject image;
    public float numberOfIcons;


    private void OnGUI()
    {
        if (numberOfIcons < GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().multiShot)
        {
            Instantiate(image, this.transform);
            numberOfIcons += 1;
        }
    }
}
