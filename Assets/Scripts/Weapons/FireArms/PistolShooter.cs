using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShooter : FireArm
{
    protected override void Awake()
    {
        base.Awake();
        bulletSpawnPosition = new Vector3(bulletSpawnPosition.x + 0.5f, bulletSpawnPosition.y + 0.15f, bulletSpawnPosition.z);
        
        //Default pistol values
        if (fireRange == 0.0f) fireRange = 8.0f;
        if (fireRate == 0.0f) fireRate = 0.8f;
    }
    
    protected override void Spray()
    {
        GameObject bulletInstance = Instantiate(bulletObject, bulletSpawnPosition, Quaternion.identity);
        bulletInstance.GetComponent<BulletMovement>().setTravelDistance(fireRange);
    }

}
