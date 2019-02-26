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

    [Header("Input")]
    public float aimDeadZone;

    [Header("Shooting")]
    public float range = 70;
    float shotTimer;

    [Header("Misc shooting stuffs")]
    public CustomSlider shotSlider;
    public AudioSource shootingAudioSource;


    private void Update()
    {
        if (PauseScript.paused == false)
        {
            shotTimer += Time.deltaTime;
            //shotSlider.value = shotTimer / Mathf.Pow((1 / 1.3f), fireRate / 2);

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
                if (Input.GetButtonUp("Fire1"))
                {
                    Debug.Log("Test");
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
        for (int i = 0; i < multiShot - 1; i++)
        {
            float angle = Random.Range(-(range/2), (range / 2));

            //angle -= 90;

            Instantiate(bulletPrefab, transform.position, (transform.rotation * Quaternion.Euler(0, 0, angle)), null);
        }

        GameObject prefabToSpawn = bulletPrefab;

        switch (selectedWeapon)
        {
            case weaponType.normal:
                prefabToSpawn = bulletPrefab;
                break;
            case weaponType.explosive:
                break;
            case weaponType.tracking:
                break;
            case weaponType.charge:
                break;
            default:
                break;
        }
        Instantiate(prefabToSpawn, transform.position, (transform.rotation * Quaternion.Euler(0, 0, 0)), null);


        //ScreenShake.instance.shake = .2f;
        ScreenShake.shakeTime = .2f;
        ScreenShake.shakeScreen.Invoke();

        if (shootingAudioSource != null)
        {
            shootingAudioSource.Play();
        }
    }
}
