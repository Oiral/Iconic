using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum weaponType {normal, explosive, tracking, charge};

public class PlayerWeapon : MonoBehaviour {

    public weaponType selectedWeapon = weaponType.normal;

    [Header("Prefabs")]
	public GameObject bulletPrefab;
    public GameObject trackingPrefab;
    public GameObject chargePrefab;
    public GameObject explosivePrefab;

    [Header("Stats")]
    public int fireRate;
    public int multiShot;
    public float bulletLifeTime;

    [Header("Input")]
    public float aimDeadZone;

    [Header("Shooting")]
    public float range = 70;
    float shotTimer;

    [Header("Misc shooting stuffs")]
    public CustomSlider shotSlider;
    //public AudioSource shootingAudioSource;
	public AK.Wwise.Event PlayerShoots;

    private void Update()
    {
        if (PauseScript.paused == false)
        {
            shotTimer += Time.deltaTime;
            shotSlider.value = shotTimer / Mathf.Pow((1 / 1.3f), fireRate / 2);

            Vector2 aim;

            aim = new Vector2(Input.GetAxis("AimHorizontal"), Input.GetAxis("AimVertical"));

            if (Input.GetButton("Fire1") || aim.magnitude > aimDeadZone)
            {
                //first iteration of shot timer
                //(1/ (shotSpeed/2))

                //second iteration of shot timer
                //Mathf.Pow((1 / 1.3f), shotSpeed / 2)

                if (shotTimer >= Mathf.Pow((1 / 1.3f), fireRate / 2))
                {
                    shotTimer = 0;
                    //Spawn stuff
                    //Debug.Log("Pew");
                    Shoot();
                }
            }


            if (selectedWeapon == weaponType.charge && Input.GetButtonUp("Fire1"))
            {
                Debug.Log("Fire Charge");
            }
        }
    }

    private void Shoot()
    {

        GameObject prefab = bulletPrefab;

        float angleMultiplier = 1;

        switch (selectedWeapon)
        {
            case weaponType.normal:
                /*for (int i = 0; i < multiShot - 1; i++)
                {
                    float angle = Random.Range(-(range / 2), (range / 2));

                    //angle -= 90;

                    Instantiate(bulletPrefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, angle)), null);
                }
                Instantiate(bulletPrefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, 0)), null);*/
                prefab = bulletPrefab;
                angleMultiplier = 1;
                break;
            case weaponType.explosive:
                break;
            case weaponType.tracking:
                //GameObject bullet = Instantiate(trackingPrefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, 0)), null);
                //bullet.GetComponent<TimedDestroy>().destroyTimer = bulletLifeTime;
                prefab = trackingPrefab;
                angleMultiplier = 3;
                break;
            case weaponType.charge:
                break;
            default:
                break;
        }

        SpawnBullet(prefab);

        for (int i = 0; i < multiShot - 1; i++)
        {
            float angle = Random.Range(-(range / 2), (range / 2));

            //angle -= 90;

            SpawnBullet(prefab,angle * angleMultiplier);
        }

        //ScreenShake.instance.shake = .2f;
        ScreenShake.shakeTime = .2f;
        ScreenShake.shakeScreen.Invoke();

        //if (shootingAudioSource != null)
        //{
        //    shootingAudioSource.Play();
        //}
		PlayerShoots.Post(gameObject);
	
    }

    public void SpawnBullet(GameObject prefab)
    {
        GameObject bullet = Instantiate(prefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, 0)), null);
        /*
        switch (selectedWeapon)
        {
            case weaponType.tracking:
                bullet.GetComponent<TimedDestroy>().destroyTimer = bulletLifeTime;
                bullet.transform.rotation = Quaternion.Inverse(bullet.transform.rotation);
                break;
            default:
                break;
        }*/
    }

    public void SpawnBullet(GameObject prefab, float extraRotation)
    {
        GameObject bullet = Instantiate(prefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, extraRotation)), null);

        
        switch (selectedWeapon)
        {
            case weaponType.tracking:
                bullet.GetComponent<TimedDestroy>().destroyTimer = bulletLifeTime;
                bullet.transform.rotation = Quaternion.Inverse(bullet.transform.rotation);
                break;
            default:
                break;
        }
    }
}
