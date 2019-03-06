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
        public int healthMultiplier;
        public int movementSpeed;
        public Sprite image;
    }

    public Level[] levels;
    

	// Use this for initialization
	void Start () {

        Level upgrade = CheckIfLevel(EnemyManager.instance.score);

        if (upgrade != null)
        {
            //Try get all the different components
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            MoveForward moveScript = GetComponent<MoveForward>();
            RandomDirection randomMoveScript = GetComponent<RandomDirection>();
            HeavyEnemySpawn heavyHealth = GetComponent<HeavyEnemySpawn>();
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();

            //Change the health
            if (enemyHealth != null)
            {
                enemyHealth.health = upgrade.health;
            }
            if (heavyHealth != null)
            {
                heavyHealth.multiplier = upgrade.healthMultiplier;
            }

            //Change the move speeds
            if (moveScript != null)
            {
                moveScript.accelerationSpeed = upgrade.movementSpeed;
                moveScript.maxSpeed = upgrade.movementSpeed / 2;
            }
            if (randomMoveScript != null)
            {
                randomMoveScript.moveSpeed = upgrade.movementSpeed;
            }




            //Change the sprite
            if (renderer != null)
            {
                renderer.sprite = upgrade.image;
            }
            
        }

        
	}

    //Check if there is a level to use
    Level CheckIfLevel(int currScore)
    {
        Level tempLevel = null;

        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].score < currScore)
            {
                if (tempLevel == null || levels[i].score > tempLevel.score)
                {
                    tempLevel = levels[i];
                }
            }
        }

        return tempLevel;
    }
}
