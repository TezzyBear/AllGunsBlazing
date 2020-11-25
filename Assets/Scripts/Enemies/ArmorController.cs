using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : MonoBehaviour
{
    private float[,] offsets = { {-0.134f, 0.126f, 0.0f },
                                 {-0.164f, 0.033f, 0.0f },
                                 { -0.49f, -0.13f, 0.0f } };

    public enum ArmorType
    {
        Rock,
        Wood,
        Steel,
        Fire
    }

    [SerializeField]
    private Sprite[] sprites;
    private ArmorType type;
    private EnemyController.Level level;
    public int maxHealth,
                currentHealth;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Create(int maxH, EnemyController.Level lvl, ArmorType t)
    {
        maxHealth = maxH;
        currentHealth = maxHealth;
        level = lvl;
        transform.position += new Vector3(offsets[(int)level, 0], offsets[(int)level, 1], offsets[(int)level, 2]);
        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        type = t;
        spriteRenderer.sprite = sprites[(int)level * 4 + (int)type];
    }
    public int Damage(int damage)
    {
        currentHealth -= damage;
        return -currentHealth;
    }
    public void UpdatePos(Vector3 pos)
    {
        transform.position = pos + new Vector3(offsets[(int)level, 0], offsets[(int)level, 1], offsets[(int)level, 2]);
    }
    public ArmorType getType() { return type; }
}
