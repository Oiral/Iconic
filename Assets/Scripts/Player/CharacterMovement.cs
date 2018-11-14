using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour {

    [Header("Input")]
    public float moveDeadZone;
    public float aimDeadZone;
    public float moveSpeed = 5;
    public bool usingMouse = true;
    public bool touch = false;

    public FixedJoystick movementStick;
    public FixedJoystick aimStick;

    [Header("Health")]
    public int health = 1;
    public int maxHealth = 1;

    public float invulnerableTime = 1;
    float invulnerableTimer = 0;

    float healthRegenTimer;
    public float healthRegenTime;

    [Header("Shooting")]
    public float range = 70;
    float shotTimer;
    public GameObject bulletPrefab;

    [Header("Power Ups")]

    public float shotSpeed = 1f;
    public int multiShot = 1;

    [Header("UI")]
    public Text speedText;
    public Text shotSpeedText;
    public Text multiShotText;

    public GameObject inGameUI;
    public GameObject deathScreen;
    public Text scoreText;

	// Update is called once per frame
	void Update () {
        Movement();
        Firing();
        InvulnerabilityTimer();
        HealthRegen();
        UpdateUI();

        ClampToScreen(0.01f,0.99f);
    }

    public void Firing()
    {
        shotTimer += Time.deltaTime;

        Vector2 aim;
        if (touch)
        {
            aim = aimStick.Direction;
        }
        else
        {
            aim = new Vector2(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"));
        }

        if (Input.GetButton("Fire1") || aim.magnitude > aimDeadZone)
        {
            if (shotTimer >= (1/ (shotSpeed/2)))
            {
                shotTimer = 0;
                //Spawn stuff
                Debug.Log("Pew");
                for (int i = 0; i < multiShot - 1; i++)
                {
                    float angle = Random.Range(0, (range / 2));
                    angle = angle * RandomSign();

                    angle -= 90;

                    Instantiate(bulletPrefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, angle)), null);
                }

                Instantiate(bulletPrefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, -90)), null);
                ScreenShake.instance.shake = .2f;
            }
        }
    }

    public void Movement()
    {


        Vector2 movementVect = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector2 aimVect = new Vector2(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"));

        if (touch)
        {
            movementVect = movementStick.Direction;
            aimVect = aimStick.Direction;
        }

        if (usingMouse && aimVect.magnitude > aimDeadZone)
        {
            usingMouse = false;
        }

        if (movementVect.magnitude > moveDeadZone)
        {

            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(movementVect.y, movementVect.x) * 180 / Mathf.PI);

            transform.position += transform.right * moveSpeed * movementVect.magnitude * Time.deltaTime;


        }
        if (usingMouse && touch == false)
        {
            //Aim towards the mouse

            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        }
        else
        {
            //Aim with the aim keys/joystick
            if (aimVect.magnitude > aimDeadZone)
            {

                transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(aimVect.y, aimVect.x) * 180 / Mathf.PI);
            }
        }
    }

    public void InvulnerabilityTimer()
    {
        if (invulnerableTimer > 0)
        {
            invulnerableTimer -= Time.deltaTime;
            Time.timeScale = Mathf.Abs( 1 - (invulnerableTimer / 2));
        }
        else
        {
            invulnerableTimer = 0;
            Time.timeScale = 1;
        }
    }

    public void HealthRegen()
    {
        if (invulnerableTimer == 0 && health < maxHealth)
        {
            healthRegenTimer += Time.deltaTime;
            if (healthRegenTimer > healthRegenTime)
            {
                healthRegenTimer = 0;
                health += 1;
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
            }
        }
    }

    public void ClampToScreen(float min, float max)
    {
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, min, max);
        pos.y = Mathf.Clamp(pos.y, min, max);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Game Over");
        
        if (health > 0)
        {
            invulnerableTimer = invulnerableTime;
            healthRegenTimer = 0;
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<BasicEnemyMovement>().RemoveHealth(99);
            }
            else
            {
                Destroy(collision.gameObject);
            }
            health -= 1;
        }
        else
        {
            Die();

            //SceneManager.LoadScene(0);
        }
        
    }

    int RandomSign()
    {
        return Random.value < .5 ? 1 : -1;
    }

    public void UpdateUI()
    {
        speedText.text = moveSpeed.ToString();
        shotSpeedText.text = shotSpeed.ToString();
        multiShotText.text = multiShot.ToString();
    }

    public void Die()
    {
        Time.timeScale = 1;

        //inGameUI.SetActive(false);

        deathScreen.SetActive(true);
        scoreText.text = EnemyManager.instance.score.ToString();

        Destroy(EnemyManager.instance.gameObject);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(bullet);
        }
    }
}
