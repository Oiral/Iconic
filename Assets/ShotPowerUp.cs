using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPowerUp : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterMovement>().shotSpeed += .1f;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 180) * Time.deltaTime);
    }
}
