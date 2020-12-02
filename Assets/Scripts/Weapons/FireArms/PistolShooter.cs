using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShooter : FireArm
{
    protected override void Awake()
    {
        base.Awake();

        //Default pistol values
        if (fireRange == 0.0f) fireRange = 8.0f;
        if (fireRate == 0.0f) fireRate = 0.8f;
    }
    
    protected override void Spray()
    {
        Vector3 shooterPos = this.transform.position;
        bulletSpawnPosition = new Vector3(shooterPos.x + 0.5f, shooterPos.y + 0.15f, shooterPos.z);

        GameObject bulletInstance = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        bulletInstance.GetComponent<BulletMovement>().setTravelDistance(fireRange);
        bulletInstance.GetComponent<BulletMovement>().setSpeed(bulletSpeed);
        bulletInstance.GetComponent<BulletType>().Create(bulletDamage, type);
    }

}
