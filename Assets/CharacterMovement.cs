using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float moveDeadZone;
    public float aimDeadZone;

    public float moveSpeed = 5;

    public GameObject bulletPrefab;
    public float shotSpeed = 0.5f;

    public int health = 2;
    public float invulnerableTimer = 0;

    float healthRegenTimer;
    public float healthRegenTime;

    float shotTimer;

    public int multiShot = 1;

    public float range = 70;

	// Update is called once per frame
	void Update () {
        Movement();
        Firing();
        InvulnerabilityTimer();
        

        ClampToScreen(0.01f,0.99f);
    }

    public void Firing()
    {
        shotTimer += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (shotTimer >= shotSpeed)
            {
                shotTimer = 0;
                //Spawn stuff
                Debug.Log("Pew");
                for (int i = 0; i < multiShot - 1; i++)
                {
                    float angle = Random.Range(0, (70 / 2));
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
        Vector2 inputVect = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector2 movementVect = new Vector2(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"));

        if (inputVect.magnitude > moveDeadZone)
        {

            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(inputVect.y, inputVect.x) * 180 / Mathf.PI);

            transform.position += transform.right * moveSpeed * inputVect.magnitude * Time.deltaTime;


        }
        if (movementVect.magnitude > aimDeadZone)
        {

            transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(movementVect.y, movementVect.x) * 180 / Mathf.PI);
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
        if (invulnerableTimer == 0)
        {
            healthRegenTimer += Time.deltaTime;
            if (healthRegenTimer > healthRegenTime)
            {
                health += 1;
                if (health > 2)
                {
                    health = 2;
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
        Debug.Log("Game Over");

        if (health > 0)
        {
            invulnerableTimer += 1;
            healthRegenTimer = 0;
            collision.gameObject.GetComponent<BasicEnemyMovement>().RemoveHealth(99);
            health -= 1;
        }
        else
        {
            Debug.Break();
        }
        
    }

    int RandomSign()
    {
        return Random.value < .5 ? 1 : -1;
    }
}
