using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int maxShield = 100;
    public int currentShield;
    public HealthBar healthBar;
    public ShieldBar shieldBar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentShield = maxShield;
        shieldBar.SetMaxShield(maxShield);
        shieldBar.SetColor(0.0f, 0.0f, 255.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamageHealth(20);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            TakeDamageShield(20);
        }

    }

    void TakeDamageHealth(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    void TakeDamageShield(int damage)
    {
        currentShield -= damage;
        shieldBar.SetShield(currentShield);
    }
}
