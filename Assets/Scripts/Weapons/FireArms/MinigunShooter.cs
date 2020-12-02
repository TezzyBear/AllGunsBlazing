using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunShooter : FireArm
{
    protected override void Awake()
    {
        base.Awake();

        //Default minigun values
        if (fireRange == 0.0f) fireRange = 5.0f;
        if (fireRate == 0.0f) fireRate = 0.3f;
    }

    protected override void Update() 
    {
        base.Update();
        bulletSpawnPosition = new Vector3(bulletSpawnPosition.x + 1.2f, bulletSpawnPosition.y, bulletSpawnPosition.z);
    }

    protected override void Spray()
    {
        
        bulletSpawnPosition += new Vector3(1.2f, 0.0f, 0.0f);
        

        Vector3 topBulletSpawnPosition = bulletSpawnPosition + new Vector3(-0.2f, 0.2f, 0.0f);
        GameObject TopBullet = Instantiate(bulletObject, topBulletSpawnPosition, Quaternion.identity);
        TopBullet.GetComponent<BulletMovement>().Create(type, fireRange,bulletSpeed, weatherData);
        TopBullet.GetComponent<BulletType>().Create(bulletDamage, type, weatherData);

        GameObject MiddleBullet = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        MiddleBullet.GetComponent<BulletMovement>().Create(type, fireRange, bulletSpeed, weatherData);
        MiddleBullet.GetComponent<BulletType>().Create(bulletDamage, type, weatherData);

        Vector3 bottomBulletSpawnPosition = bulletSpawnPosition + new Vector3(0.2f, -0.2f, 0.0f);
        GameObject BottomBullet = Instantiate(bulletObject, bottomBulletSpawnPosition, Quaternion.identity);
        BottomBullet.GetComponent<BulletMovement>().Create(type, fireRange, bulletSpeed, weatherData);
        BottomBullet.GetComponent<BulletType>().Create(bulletDamage, type, weatherData);
    }
}