using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TurretLifespan : MonoBehaviour
{
    [SerializeField]
    float lifespan = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0) {
            Destroy(this.gameObject);
        }
    }
}
