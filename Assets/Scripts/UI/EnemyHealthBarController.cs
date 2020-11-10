using System;
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
    public GameObject healthBar;
    public GameObject shieldBar;
    public GameObject damagePopUp;
    public GameObject canvas;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = Instantiate(healthBar, transform.position, Quaternion.identity);
        healthBar.transform.parent = canvas.transform;
        healthBar.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Instantiate(shieldBar, transform.position, Quaternion.identity);
        currentHealth = maxHealth;
        healthBar.GetComponent<HealthBar>().SetMaxHealth(maxHealth);

        shieldBar = Instantiate(shieldBar, transform.position + new Vector3(0.0f, -0.25f, 0.0f) , Quaternion.identity);
        shieldBar.transform.parent = canvas.transform;
        shieldBar.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        currentShield = maxShield;
        shieldBar.GetComponent<ShieldBar>().SetMaxShield(maxShield);
        shieldBar.GetComponent<ShieldBar>().SetColor("ROCK_ARMOR");
        damage = 20;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject damagePopped = Instantiate(damagePopUp, transform.position, Quaternion.identity) as GameObject;
            damagePopped.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
            damagePopped.transform.GetChild(0).GetComponent<TextMesh>().color = Color.red;
            TakeDamageHealth(damage);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            TakeDamageShield(damage);
            GameObject damagePopped = Instantiate(damagePopUp, transform.position, Quaternion.identity) as GameObject;
            damagePopped.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
            damagePopped.transform.GetChild(0).GetComponent<TextMesh>().color = Color.grey;
        }

    }

    void TakeDamageHealth(int damage)
    {
        currentHealth -= damage;
        healthBar.GetComponent<HealthBar>().SetHealth(currentHealth);
    }
    void TakeDamageShield(int damage)
    {
        currentShield -= damage;
        shieldBar.GetComponent<ShieldBar>().SetShield(currentShield);
    }
}
