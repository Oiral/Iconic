using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public float moveDeadZone;
    public float aimDeadZone;

    public float moveSpeed = 5;

    public GameObject bulletPrefab;
    public float shotSpeed = 0.5f;

    float shotTimer;

	// Update is called once per frame
	void Update () {
        Movement();

        shotTimer += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (shotTimer >= shotSpeed)
            {
                shotTimer = 0;
                //Spawn stuff
                Debug.Log("Pew");
                Instantiate(bulletPrefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, -90)), null);
            }
        }
        ClampToScreen(0.01f,0.99f);
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
        Debug.Break();
    }
}
