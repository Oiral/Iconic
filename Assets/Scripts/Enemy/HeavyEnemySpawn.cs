using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class HeavyEnemySpawn : MonoBehaviour {

    public int multiplier = 2;

	// Use this for initialization
	void Start () {
        EnemyHealth stats = GetComponent<EnemyHealth>();

        PlayerWeapon playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerWeapon>();

        stats.health = playerStats.multiShot * multiplier;

        Destroy(this);
	}
}
