using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            GameController.instance.towerHitPoints -= 25;
            Destroy(collision.gameObject.transform.parent.gameObject);         
        }
    }
}
