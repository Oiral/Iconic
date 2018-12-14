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
        if (Random.Range(0f, 1f) < dropChance || guaranteeDrop)
        {
            Instantiate(powerUps[Random.Range(0, powerUps.Count)], transform.position, Quaternion.identity, null);
        }
    }
}
