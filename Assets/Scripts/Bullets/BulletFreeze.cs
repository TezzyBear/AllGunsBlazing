using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFreeze : MonoBehaviour
{
    private GameObject enemyGameObject;
    private float initialMovementSpeed;
    private float freezingPercentage = 0.8f;
    private Color freezingColor = Color.blue;
    private float freezingDuration = 2.0f;
    private float freezingTimer = -1;
    private bool isFrozen = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (freezingTimer != -1) {
            freezingTimer -= Time.deltaTime;
            if (freezingTimer <= 0)
            {
                UnFreeze();
                Destroy(this);
            }
        }
    }
    public void SetInitialSpeed(float initialSpeed) {
        initialMovementSpeed = initialSpeed;
        
    }
    public void SetEnemyGameObject(GameObject enemyObj) {
        enemyGameObject = enemyObj;        
    }

    public void Freeze() {
        if (!isFrozen) {
            freezingTimer = freezingDuration;
            enemyGameObject.GetComponent<EnemyController>().movementSpeed = initialMovementSpeed * freezingPercentage;
            enemyGameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = freezingColor;
            isFrozen = true;
        }       
    }

    private void UnFreeze()
    {
        enemyGameObject.GetComponent<EnemyController>().movementSpeed = initialMovementSpeed;
        enemyGameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        isFrozen = false;
    }
}
