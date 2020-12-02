using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using UnityEngine;

public class FireArm : MonoBehaviour
{

    [SerializeField]
    protected float fireRate;
    private float fireRateCooldown;
    private bool canShoot;
    [SerializeField]
    protected float fireRange;
    protected float startFireRange;
    [SerializeField]
    protected GameObject bulletObject;
    protected Vector3 bulletSpawnPosition;
    [SerializeField]
    protected int bulletDamage;
    protected int startBulletDamage;
    [SerializeField]
    protected float bulletSpeed;
    protected float startBulletSpeed;
    protected WeatherController.WeatherData weatherData;

    public BulletType.Type type;

    protected virtual void Awake()
    {
        bulletSpeed = bulletSpeed == 0 ? 5.0f : bulletSpeed;
        bulletSpawnPosition = transform.position;
        canShoot = true;
        startBulletDamage = bulletDamage;
        startFireRange = fireRange;
        startBulletSpeed = bulletSpeed;
        weatherData = new WeatherController.WeatherData(WeatherController.Weather.RAIN, 0);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        fireRateCooldown = fireRate;
    }

    protected virtual void Create(BulletType.Type t)
    {
        type = t;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            Shoot();
        }
        else {
            fireRateCooldown -= Time.deltaTime;
            if (fireRateCooldown <= 0)
            {
                fireRateCooldown = fireRate;
                canShoot = true;
            }
        }
    }

    public void setWeather(WeatherController.WeatherData wd)
    {
        weatherData = wd;
    }

    protected virtual void Shoot() { //Initialize sprayss at fire rate
        bulletSpawnPosition = transform.position;
        Spray();
    }

    protected virtual void Spray() { //Spawns bullets in order

        GameObject bulletInstance = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        bulletInstance.GetComponent<BulletMovement>().Create(type ,fireRange, bulletSpeed,weatherData);
        bulletInstance.GetComponent<BulletType>().Create(bulletDamage, type, weatherData);
    }
}
