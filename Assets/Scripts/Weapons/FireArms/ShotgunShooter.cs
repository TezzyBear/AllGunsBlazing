using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShooter : FireArm
{
    protected override void Awake()
    {
        base.Awake();
        bulletSpawnPosition = new Vector3(bulletSpawnPosition.x + 1.0f, bulletSpawnPosition.y, bulletSpawnPosition.z);

        //Default shotgun values
        if (fireRange == 0.0f) fireRange = 3.0f;
        if (fireRate == 0.0f) fireRate = 1.5f;
    }

    protected override void Update()
    {
        base.Update();
        bulletSpawnPosition = new Vector3(bulletSpawnPosition.x + 1.0f, bulletSpawnPosition.y, bulletSpawnPosition.z);
    }

    protected override void Spray()
    {
        Vector3 topBulletSpawnPosition = bulletSpawnPosition + new Vector3(0.0f, 0.2f, 0.0f);
        GameObject TopBullet = Instantiate(bulletObject, topBulletSpawnPosition, Quaternion.identity);
        TopBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);

        GameObject MiddleBullet = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        MiddleBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);

        Vector3 bottomBulletSpawnPosition = bulletSpawnPosition + new Vector3(0.0f, -0.2f, 0.0f);
        GameObject BottomBullet = Instantiate(bulletObject, bottomBulletSpawnPosition, Quaternion.identity);
        BottomBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);
    }
}
