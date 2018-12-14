using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class HeavyEnemySpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        EnemyHealth stats = GetComponent<EnemyHealth>();

        CharacterMovement playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

        stats.health = playerStats.multiShot * 2;

        Destroy(this);
	}
}
