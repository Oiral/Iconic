using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;

    bool destroying;

    public int score;

	public AK.Wwise.Event EExplodeAudio;
    public GameObject deathParticle;

    public void RemoveHealth(int damage)
    {
        health -= damage;
        if (health <= 0 && destroying == false)
        {
            destroying = true;
            EnemyManager.instance.enemiesAlive -= 1;
            EnemyManager.instance.enemiesKilled += 1;
            EnemyManager.instance.enemiesKilledText.text = EnemyManager.instance.enemiesKilled.ToString(); ;
            
            Instantiate(deathParticle, transform.position, transform.rotation, null);
			EExplodeAudio.Post (gameObject);

            //Call screen shake
            ScreenShake.shakeTime = 0.05f;
            ScreenShake.shakeScreen.Invoke();

            EnemyManager.instance.IncreaseScore(score);

            if (GetComponent<DropPowerup>())
            {
                GetComponent<DropPowerup>().DropPowerUp();
            }

            Destroy(gameObject);
        }
    }
}
