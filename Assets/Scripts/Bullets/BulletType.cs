using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletType : MonoBehaviour
{    public enum Type {
        NONE,
        PIERCING,
        BLEEDING,
        EXPLOSIVE,
        BLUNDERING,
        INCENDIARY,
        FREEZING
    }

    public Type type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
