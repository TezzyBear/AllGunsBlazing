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

    protected override void Spray()
    {
        Vector3 shooterPos = this.transform.position;
        bulletSpawnPosition = new Vector3(shooterPos.x + 1.2f, shooterPos.y, shooterPos.z);

        Vector3 topBulletSpawnPosition = bulletSpawnPosition + new Vector3(-0.2f, 0.2f, 0.0f);
        GameObject TopBullet = Instantiate(bulletObject, topBulletSpawnPosition, Quaternion.identity);
        TopBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);

        GameObject MiddleBullet = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        MiddleBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);

        Vector3 bottomBulletSpawnPosition = bulletSpawnPosition + new Vector3(0.2f, -0.2f, 0.0f);
        GameObject BottomBullet = Instantiate(bulletObject, bottomBulletSpawnPosition, Quaternion.identity);
        BottomBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);
    }
}