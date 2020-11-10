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
    [SerializeField]
    protected GameObject bulletObject;
    protected Vector3 bulletSpawnPosition;
    [SerializeField]
    protected int bulletDamage;

    public BulletType.Type type;

    protected virtual void Awake()
    {
        bulletSpawnPosition = transform.position;
        canShoot = true;
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

    protected virtual void Shoot() { //Initialize sprayss at fire rate
        bulletSpawnPosition = transform.position;
        Spray();
    }

    protected virtual void Spray() { //Spawns bullets in order

        GameObject bulletInstance = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        bulletInstance.GetComponent<BulletMovement>().setTravelDistance(fireRange);
        bulletInstance.GetComponent<BulletType>().Create(bulletDamage, type);
    }
}
