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

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            canShoot = false;
            bulletSpawnPosition = transform.position;
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
        Spray();
    }

    protected virtual void Spray() { //Spawns bullets in order
        GameObject bulletInstance = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        bulletInstance.GetComponent<BulletMovement>().setTravelDistance(fireRange);
    }
}
