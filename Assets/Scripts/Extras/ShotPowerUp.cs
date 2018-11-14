using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { fireRate, multishot, speed}

public class ShotPowerUp : MonoBehaviour {

    public PowerUpType type = PowerUpType.fireRate;

    public int score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (type)
            {
                case PowerUpType.fireRate:
                    collision.gameObject.GetComponent<CharacterMovement>().shotSpeed += 1;
                    break;
                case PowerUpType.multishot:
                    collision.gameObject.GetComponent<CharacterMovement>().multiShot += 1;
                    break;
                case PowerUpType.speed:
                    collision.gameObject.GetComponent<CharacterMovement>().moveSpeed += 0.1f;
                    break;
                default:
                    break;
            }

            EnemyManager.instance.score += score;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
    }
}
