﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShooter : FireArm
{
    protected override void Awake()
    {
        base.Awake();    

        //Default sniper values
        if (fireRange == 0.0f) fireRange = 10.0f;
        if (fireRate == 0.0f) fireRate = 2.0f;
    }

    protected override void Spray()
    {
        bulletSpawnPosition = new Vector3(bulletSpawnPosition.x + 0.5f, bulletSpawnPosition.y, bulletSpawnPosition.z);

        GameObject bulletInstance = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        bulletInstance.GetComponent<BulletMovement>().Create(type, fireRange, bulletSpeed, weatherData);
        bulletInstance.GetComponent<BulletType>().Create(bulletDamage, type, weatherData);
    }   
}