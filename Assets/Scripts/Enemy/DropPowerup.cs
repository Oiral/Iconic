using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPowerup : MonoBehaviour {

    public List<GameObject> powerUps;

    public bool guaranteeDrop;

    [Range(0,1)]
    public float dropChance;


    public void DropPowerUp()
    {
        if (guaranteeDrop == true)
        {
            DropManager.instance.DropGuaranteed(transform.position);
        }
        else
        {
            DropManager.instance.DropPowerUp(transform.position);
        }
    }
}
