using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { fireRate = 0, multishot = 1, speed = 2,lifetimeUp = 3}

public class ShotPowerUp : MonoBehaviour {

    public PowerUpType type = PowerUpType.fireRate;

    public int score;

    public GameObject explosionCircle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (type)
            {
                case PowerUpType.fireRate:
                    collision.gameObject.GetComponent<PlayerWeapon>().fireRate += 1;
                    TextManager.messages.Add("Fire Rate Up");
                    break;
                case PowerUpType.multishot:
                    collision.gameObject.GetComponent<PlayerWeapon>().multiShot += 1;
                    TextManager.messages.Add("Shot Count Up");
                    break;
                case PowerUpType.speed:
                    collision.gameObject.GetComponent<Character>().moveSpeed += 0.1f;
                    TextManager.messages.Add("Movement Speed Up");
                    break;
                case PowerUpType.lifetimeUp:
                    collision.gameObject.GetComponent<PlayerWeapon>().bulletLifeTime += 0.2f;
                    TextManager.messages.Add("Bullet Lifetime Up");
                    break;
                default:
                    break;
            }

            EnemyManager.instance.IncreaseScore(score);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
    }
}
