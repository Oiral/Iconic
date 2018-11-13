using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemySpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BasicEnemyMovement stats = GetComponent<BasicEnemyMovement>();

        CharacterMovement playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

        stats.health = playerStats.multiShot;
	}
}
