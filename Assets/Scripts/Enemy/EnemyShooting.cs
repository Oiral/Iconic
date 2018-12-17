using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    public List<Sprite> sprites;

    SpriteRenderer renderer;

    GameObject player;

    float shotTimer;
    public float fireRate;

    public GameObject bulletPrefab;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        renderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (PauseScript.paused == false)
        {
            Aim();

            Shoot();
        }
    }

    void Aim()
    {
        //Aim towards the player

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90f);
    }

    void Shoot()
    {
        shotTimer += Time.deltaTime;
        float time = (1 / (fireRate));

        float step = time / 5;

        int round = Mathf.Clamp(Mathf.RoundToInt(shotTimer / step) - 1,0,sprites.Count);

        renderer.sprite = sprites[round];


        if (shotTimer >= time)
        {
            shotTimer = 0;
            //Spawn stuff

            Instantiate(bulletPrefab, transform.position + transform.up * 0.5f, (transform.rotation * Quaternion.Euler(0, 0, 0)), null);
            //ScreenShake.instance.shake = .2f;
            ScreenShake.shakeTime = .2f;
            ScreenShake.shakeScreen.Invoke();
        }

    }
}
