using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBurn : MonoBehaviour
{
    private GameObject enemyGameObject;
    private GameController alive;
    private float burnTicks = 5;
    private Color burnColor = Color.red;
    private float burnTickTime = 0.5f;
    private float burnTickTimer;
    private int damage = 5;
    private bool isBurning = false;
    // Start is called before the first frame update
    void Start()
    {
        burnTickTimer = burnTickTime;
    }

    // Update is called once per frame
    void Update()
    {
        burnTickTimer -= Time.deltaTime;       
        if (burnTicks <= 0) {
            StopBurn();
            Destroy(this);
        }
        else {
            if (burnTickTimer <= 0) {
                burnTickTimer = burnTickTime;
                burnTicks--;
                Burn();
            }
        }
    }

    public void SetEnemyGameObject(GameObject enemyObj)
    {
        enemyGameObject = enemyObj;
    }

    public void Burn()
    {
        enemyGameObject.transform.GetChild(0).gameObject.GetComponent<AliveController>().Damage(damage);
        enemyGameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = burnColor;
    }

    private void StopBurn()
    {        
        enemyGameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    public void RefreshBurn() {
        burnTicks = 5;
    }
}
