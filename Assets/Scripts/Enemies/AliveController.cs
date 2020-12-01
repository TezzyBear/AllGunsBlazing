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
                          { { 0f,   0.1f,    0.5f,   1.0f, 0.1f,   0.5f},
                            { 0f,   0.3f,    0.5f,   0.5f, 1.0f,   0.2f},
                            { 0f,  0.05f,    0.5f,   0.2f,   0f,  0.05f},
                            { 0f,     0f,      0f,     0f,   0f,   0.4f}};


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
    private bool destroyed;

    public GameObject healthBarPref;
    public GameObject shieldBarPref;

    private GameObject healthBar;
    private GameObject shieldBar;
    
    public GameObject damagePopUp;
    private GameObject canvas;
    private GameController gc;

    void Awake()
    {
        enemyController = transform.parent.GetComponent<EnemyController>();
        destroyed = false;
    }


    public void Create(int health, int armorHealth,EnemyController.Level lvl,EnemyController.EnemyType et, GameObject c, GameController gc)
    {
        this.maxHealth = health;
        this.currentHealth = maxHealth;
        this.type = et;
        this.canvas = c;
        this.gc = gc;
        //HEALTH BAR
        healthBar = Instantiate(healthBarPref, transform.position, Quaternion.identity);
        healthBar.transform.parent = canvas.transform;
        healthBar.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        healthBar.GetComponent<HealthBar>().SetMaxHealth(maxHealth);

        //SHIELD
        if (type != EnemyController.EnemyType.Unarmored)
        {
            armor = Instantiate(armorPref, transform.position, Quaternion.identity);
            armorController = armor.GetComponent<ArmorController>();
            armorController.Create(armorHealth, lvl, (ArmorController.ArmorType)((int)et));
            //SHIELD BAR
            shieldBar = Instantiate(shieldBarPref, transform.position + new Vector3(0.0f, -0.25f, 0.0f), Quaternion.identity);
            shieldBar.transform.parent = canvas.transform;
            shieldBar.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            shieldBar.GetComponent<ShieldBar>().SetMaxShield(armorController.maxHealth);
            shieldBar.GetComponent<ShieldBar>().SetColor(armorController.getType());
        }


    }

    public void Update()
    {
        healthBar.GetComponent<HealthBar>().SetPosition(transform.position + new Vector3(0.0f, 1f, 0.0f));
        healthBar.GetComponent<HealthBar>().SetHealth(currentHealth);

        if (armor != null)
        {
            armorController.UpdatePos(transform.position);
            shieldBar.GetComponent<ShieldBar>().SetPosition(transform.position + new Vector3(0.0f, 0.75f, 0.0f));
            shieldBar.GetComponent<ShieldBar>().SetShield(armorController.currentHealth);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Bullet"))
        {
            //get bullet freezing type by component
            if (col.gameObject.GetComponent<BulletType>().type == BulletType.Type.FREEZING) {
                //add bullet effect controller
                if (GetComponent<BulletFreeze>() == null) {
                    BulletFreeze tmp = gameObject.AddComponent<BulletFreeze>() as BulletFreeze;
                    tmp.SetEnemyGameObject(this.transform.parent.gameObject);
                    tmp.SetInitialSpeed(this.transform.parent.GetComponent<EnemyController>().movementSpeed);
                    tmp.Freeze();
                }                
            }
            
            

            BulletType bulletScript = col.gameObject.GetComponent<BulletType>();
            int residualDamage = 0;
            GameObject damagePopped;
            if (armor != null)
            {
                ArmorController.ArmorType armorType = armorController.getType();
                int armorDamage = (int)(bulletScript.damage * BulletVsArmor[(int)armorType, (int)bulletScript.type]);
                
                damagePopped = Instantiate(damagePopUp, transform.position, Quaternion.identity);
                if (bulletScript.damage > 0 & bulletScript.damage < 25)
                {
                    damagePopped.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                if (bulletScript.damage >= 25)
                {
                    damagePopped.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                }
                damagePopped.transform.GetChild(0).GetComponent<TextMesh>().text = armorDamage.ToString();
                damagePopped.transform.GetChild(0).GetComponent<TextMesh>().color = Color.grey;
                
                

                residualDamage = armorController.Damage(armorDamage);
                shieldBar.GetComponent<ShieldBar>().SetShield(armorController.currentHealth);

                if (residualDamage >= 0)
                {
                    Destroy(armor);
                    Destroy(shieldBar);
                    type = EnemyController.EnemyType.Unarmored;
                    if (BulletVsArmor[(int)armorType, (int)bulletScript.type] == 0)
                    {
                        residualDamage = 0;
                    }
                    else
                    {
                        residualDamage = (int)(residualDamage / BulletVsArmor[(int)armorType, (int)bulletScript.type]);
                    }
                }
                else
                {
                    residualDamage = 0;
                }
                
                
            }

            int lifeDamage = (int)((bulletScript.damage + residualDamage) * BulletVsLife[(int)type,(int)bulletScript.type]);
            damagePopped = Instantiate(damagePopUp, transform.position, Quaternion.identity);
            if(bulletScript.damage > 0 & bulletScript.damage < 25)
            {
                damagePopped.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            if(bulletScript.damage >= 25)
            {
                damagePopped.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            }
            
            damagePopped.transform.GetChild(0).GetComponent<TextMesh>().text = lifeDamage.ToString();
            damagePopped.transform.GetChild(0).GetComponent<TextMesh>().color = Color.red;
            
            Damage(lifeDamage);
            healthBar.GetComponent<HealthBar>().SetHealth(currentHealth);

            Destroy(col.gameObject);
        }
    }

    public void Damage(int damage)
    {

        currentHealth -= damage;

        Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        if (currentHealth <= 0)
        {
            if (!destroyed)
            {
                FullDestroy();
            }
            
        }
    }
    public void FullDestroy()
    {
        if (!destroyed)
        {
            destroyed = true;
            Destroy(armor);
            Destroy(shieldBar);
            Destroy(healthBar);
            enemyController.SwitchState(EnemyController.State.Dead);
            gc.DestroyEnemy();
        }
        
    }
}
