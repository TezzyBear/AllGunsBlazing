﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiffleShooter : FireArm
{
    protected override void Awake()
    {
        base.Awake();
        bulletSpawnPosition = new Vector3(bulletSpawnPosition.x + 1.0f, bulletSpawnPosition.y + 0.15f, bulletSpawnPosition.z);

        //Default riffle values
        if (fireRange == 0.0f) fireRange = 8.0f;
        if (fireRate == 0.0f) fireRate = 0.8f;
    }

    protected override void Spray()
    {
        Vector3 topBulletSpawnPosition = bulletSpawnPosition + new Vector3(-0.1f, 0.1f, 0.0f);
        GameObject TopBullet = Instantiate(bulletObject, topBulletSpawnPosition, Quaternion.identity);
        TopBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);

        Vector3 bottomBulletSpawnPosition = bulletSpawnPosition + new Vector3(0.1f, -0.1f, 0.0f);
        GameObject BottomBullet = Instantiate(bulletObject, bottomBulletSpawnPosition, Quaternion.identity);
        BottomBullet.GetComponent<BulletMovement>().setTravelDistance(fireRange);
    }

}