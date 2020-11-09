﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShooter : FireArm
{
    protected override void Awake()
    {
        base.Awake();
        bulletSpawnPosition = new Vector3(bulletSpawnPosition.x + 0.5f, bulletSpawnPosition.y, bulletSpawnPosition.z);

        //Default sniper values
        if (fireRange == 0.0f) fireRange = 10.0f;
        if (fireRate == 0.0f) fireRate = 2.0f;
    }

    protected override void Update()
    {
        base.Update();
        bulletSpawnPosition = new Vector3(bulletSpawnPosition.x + 0.5f, bulletSpawnPosition.y, bulletSpawnPosition.z);
    }

    protected override void Spray()
    {
        GameObject bulletInstance = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        bulletInstance.GetComponent<BulletMovement>().setTravelDistance(fireRange);
        bulletInstance.GetComponent<BulletMovement>().setSpeed(10.0f);
    }   
}