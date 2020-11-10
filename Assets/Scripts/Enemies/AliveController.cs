using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


public class AliveController : MonoBehaviour
{
    private float[,] BulletVsLife = 
                          { { 0.5f,   0f, 0.1f,   0f, 0.1f,  0.1f},
                            { 0.7f,   0f, 0.2f, 0.2f, 0.1f, 0.05f},
                            { 0.4f,   0f,   0f,   0f, 0.1f, 0.05f},
                            { 0.1f, 0.2f,   0f, 0.3f,   0f,    0f},
                            {   1f,   2f, 1.5f, 1.4f, 1.7f,  1.1f}};
    private float[,] BulletVsArmor =
                          { { 0f,   0.1f, 0.5f,   1.0f, 0.1f,  0.5f},
                            { 0f,   0.3f, 0.5f, 0.5f, 1.0f, 0.2f},
                            { 0f,   0.05f,   0.5f,   0.2f, 0f, 0.05f},
                            { 0f, 0f, 0f, 0f,   0f,    0.4f}};


    private int
        maxHealth;

    private int
        currentHealth;

    [SerializeField]
    private GameObject
        hitParticle;

    [SerializeField]
    private GameObject armorPref;
    private GameObject armor;
    private ArmorController armorController;
    private EnemyController enemyController;
    private EnemyController.EnemyType type;


    void Awake()
    {
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    public void Create(int health, int armorHealth,EnemyController.Level lvl,EnemyController.EnemyType et)
    {
        maxHealth = health;
        currentHealth = maxHealth;
        type = et;
        if(type != EnemyController.EnemyType.Unarmored)
        {
            armor = Instantiate(armorPref, transform.position, Quaternion.identity);
            armorController = armor.GetComponent<ArmorController>();
            armorController.Create(armorHealth, lvl, (ArmorController.ArmorType)((int)et));
        }
        
    }

    public void Update()
    {
        if(armor != null)
        {
            armorController.UpdatePos(transform.position);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Bullet"))
        {
            BulletType bulletScript = col.gameObject.GetComponent<BulletType>();
            int residualDamage = 0;
            if(armor != null)
            {
                ArmorController.ArmorType armorType = armorController.getType();
                int armorDamage = (int)(bulletScript.damage * BulletVsArmor[(int)armorType, (int)bulletScript.type]);
                residualDamage = armorController.Damage(armorDamage);
                if(residualDamage >= 0)
                {
                    Destroy(armor);
                    type = EnemyController.EnemyType.Unarmored;
                }
                residualDamage = (int) (residualDamage / BulletVsArmor[(int)armorType, (int)bulletScript.type]);
            }

            int lifeDamage = (int)((bulletScript.damage + residualDamage) * BulletVsLife[(int)type,(int)bulletScript.type]);

            Damage(lifeDamage);
            Destroy(col.gameObject);
        }
    }

    public void Damage(int damage)
    {

        currentHealth -= damage;

        Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        if (currentHealth <= 0)
        {
            Destroy(armor);
            enemyController.SwitchState(EnemyController.State.Dead);
        }
    }
}
