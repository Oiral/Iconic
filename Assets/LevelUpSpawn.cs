using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSpawn : MonoBehaviour {

    [System.Serializable]
    public class Level
    {
        public string name = "Level #";
        public int score;
        public int health;
        public int movementSpeed;
        public Sprite image;
    }


    

	// Use this for initialization
	void Start () {
        if (GetComponent<EnemyHealth>() != null)
        {

        }
	}
}
