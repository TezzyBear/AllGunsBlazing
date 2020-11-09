using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletType : MonoBehaviour
{   
    public enum Type {
        PIERCING,
        BLEEDING,
        EXPLOSIVE,
        BLUNDERING,
        INCENDIARY,
        FREEZING
    }

    
    public int
        damage;

    private SpriteRenderer spriteRenderer;

    public Type type;
    [SerializeField]
    private Sprite[] sprites;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Create(int dmg, Type t)
    {
        type = t;
        spriteRenderer.sprite = sprites[(int)t];
        damage = dmg;
    }

}
