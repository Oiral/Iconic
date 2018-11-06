using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { fireRate, multishot, speed}

public class ShotPowerUp : MonoBehaviour {

    public PowerUpType type = PowerUpType.fireRate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (type)
            {
                case PowerUpType.fireRate:
                    collision.gameObject.GetComponent<CharacterMovement>().shotSpeed = collision.gameObject.GetComponent<CharacterMovement>().shotSpeed / 1.1f;
                    break;
                case PowerUpType.multishot:
                    break;
                case PowerUpType.speed:
                    collision.gameObject.GetComponent<CharacterMovement>().moveSpeed += 0.1f;
                    break;
                default:
                    break;
            }

            
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
    }
}
