using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AliveController : MonoBehaviour
{
    private float[,] BulletVsLife = 
                          { { 0.5f,   0f, 0.1f,   0f, 0.1f,  0.1f},
                            { 0.7f,   0f, 0.2f, 0.2f, 0.1f, 0.05f},
                            { 0.4f,   0f,   0f,   0f, 0.1f, 0.05f},
                            { 0.1f, 0.2f,   0f, 0.3f,   0f,    0f},
                            {   1f,   2f, 1.5f, 1.4f, 1.7f,  1.1f}};

    [SerializeField]
    private int
        maxHealth;

    private int
        currentHealth;

    [SerializeField]
    private GameObject
        hitParticle;

    private EnemyController enemyController;
    void Start()
    {
        currentHealth = maxHealth;
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Bullet"))
        {
            BulletType bulletScript = col.gameObject.GetComponent<BulletType>();
            int outDamage = (int)(bulletScript.damage * BulletVsLife[4,(int)bulletScript.type]);
            Debug.Log(outDamage);
            Debug.Log(BulletVsLife[4, (int)bulletScript.type]);

            Damage(outDamage);
            Destroy(col.gameObject);
        }
    }

    private void Damage(int damage)
    {

        currentHealth -= damage;

        Instantiate(hitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            enemyController.SwitchState(EnemyController.State.Dead);
        }
    }
}
