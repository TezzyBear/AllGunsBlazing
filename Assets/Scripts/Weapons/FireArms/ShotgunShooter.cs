using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShooter : FireArm
{
    protected override void Awake()
    {
        base.Awake();

        //Default shotgun values
        if (fireRange == 0.0f) fireRange = 3.0f;
        if (fireRate == 0.0f) fireRate = 1.5f;
    }

    protected override void Spray()
    {
        Vector3 shooterPos = this.transform.position;
        bulletSpawnPosition = new Vector3(shooterPos.x + 1.0f, shooterPos.y, shooterPos.z);

        Vector3 topBulletSpawnPosition = bulletSpawnPosition + new Vector3(0.0f, 0.2f, 0.0f);
        GameObject TopBullet = Instantiate(bulletObject, topBulletSpawnPosition, Quaternion.identity);
        TopBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);
        TopBullet.GetComponent<BulletType>().Create(20, type);

        GameObject MiddleBullet = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        MiddleBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);
        MiddleBullet.GetComponent<BulletType>().Create(20, type);

        Vector3 bottomBulletSpawnPosition = bulletSpawnPosition + new Vector3(0.0f, -0.2f, 0.0f);
        GameObject BottomBullet = Instantiate(bulletObject, bottomBulletSpawnPosition, Quaternion.identity);
        BottomBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);
        BottomBullet.GetComponent<BulletType>().Create(20, type);
    }
}
