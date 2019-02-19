using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    [Header("Input")]
    public float moveDeadZone;
    public float aimDeadZone;
    public float moveSpeed = 5;
    public bool usingMouse = true;
    public bool touch = false;

    private Vector2 mouseCurrentPosition;
    private Vector2 mouseLastPosition;

    public FixedJoystick movementStick;
    public FixedJoystick aimStick;

    public float angleAfterLook = 90;

    [Header("Health")]
    public int health = 1;
    public int maxHealth = 1;

    public float invulnerableTime = 1;
    float invulnerableTimer = 0;

    float healthRegenTimer;
    public float healthRegenTime;

    public Animator healthAnimator;
    public AudioSource shieldAudio;


    [Header("Shooting")]
    public float range = 70;
    float shotTimer;
    public GameObject bulletPrefab;
    public CustomSlider shotSlider;
    public AudioSource shootingAudioSource;

    [Header("Power Ups")]

    public float shotSpeed = 1f;
    public int multiShot = 1;

    [Header("UI")]
    public Text speedText;
    public Text shotSpeedText;
    public Text multiShotText;


	// Update is called once per frame
	void Update () {
        if (PauseScript.paused == false)
        {
            Movement();
            Firing();
            InvulnerabilityTimer();
            HealthRegen();
            UpdateUI();
        }

        ClampToScreen(0.01f,0.99f);
    }

    public void Firing()
    {
        shotTimer += Time.deltaTime;

        shotSlider.value = shotTimer / Mathf.Pow((1 / 1.3f), shotSpeed / 2);

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
            //first iteration of shot timer
            //(1/ (shotSpeed/2))

            //second iteration of shot timer
            //Mathf.Pow((1 / 1.3f), shotSpeed / 2)

            if (shotTimer >= Mathf.Pow((1 / 1.3f), shotSpeed / 2))
            {
                shotTimer = 0;
                //Spawn stuff
                //Debug.Log("Pew");
                Shoot();
            }
        }
    }

    void Shoot()
    {
        for (int i = 0; i < multiShot - 1; i++)
        {
            float angle = Random.Range(0, (range / 2));
            angle = angle * RandomSign();

            //angle -= 90;

            Instantiate(bulletPrefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, angle)), null);
        }

        Instantiate(bulletPrefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, 0)), null);
        //ScreenShake.instance.shake = .2f;
        ScreenShake.shakeTime = .2f;
        ScreenShake.shakeScreen.Invoke();

        if (shootingAudioSource != null)
        {
            shootingAudioSource.Play();
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
        if (!usingMouse)
        {
            mouseCurrentPosition = Input.mousePosition;
            Vector2 deltaPos = mouseCurrentPosition - mouseLastPosition;
            mouseLastPosition = mouseCurrentPosition;

            if (deltaPos.magnitude > aimDeadZone)
            {
                usingMouse = true;
            }
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
            this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg + angleAfterLook);
        }
        else
        {
            //Aim with the aim keys/joystick
            if (aimVect.magnitude > aimDeadZone)
            {

                transform.eulerAngles = new Vector3(0, 0, (Mathf.Atan2(aimVect.y, aimVect.x) * 180 / Mathf.PI) + angleAfterLook);
            }
        }
    }

    public void InvulnerabilityTimer()
    {
        if (invulnerableTimer > 0)
        {
            //ShiftManager.instance.UpdateShift(1);
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
        if (/*invulnerableTimer == 0 &&*/ health < maxHealth)
        {
            //ShiftManager.instance.UpdateShift(1 - (healthRegenTimer/healthRegenTime));
            healthRegenTimer += Time.deltaTime;
            if (healthRegenTimer > healthRegenTime)
            {
                healthRegenTimer = 0;
                health += 1;
                //ShiftManager.instance.UpdateShift(0);
                if (health > maxHealth)
                {
                    health = maxHealth;
                }
            }
            healthAnimator.SetFloat("HealthAmount", healthRegenTimer/healthRegenTime);
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
            //Play shield particls
            GetComponent<ParticleSystem>().Play();

            //Play shield sound
            shieldAudio.Play();

            invulnerableTimer = invulnerableTime;
            healthRegenTimer = 0;
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealth>().RemoveHealth(collision.gameObject.GetComponent<EnemyHealth>().health);
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
        GameManager.instance.StartGameOver();
    }
}
