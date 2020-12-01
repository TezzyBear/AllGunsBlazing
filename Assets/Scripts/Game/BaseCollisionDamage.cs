using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionDamage : MonoBehaviour
{
    public GameObject baseHealth;
    public GameObject canvas;

    public float hpPosX;
    public float hpPosY;
    // Start is called before the first frame update
    void Start()
    {
        baseHealth = Instantiate(baseHealth, transform.position + new Vector3(hpPosX, hpPosY, 0.0f), Quaternion.identity);
        baseHealth.transform.parent = canvas.transform;
        baseHealth.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        baseHealth.GetComponent<BaseHealthBar>().SetMaxHealth(GameController.instance.towerHitPoints);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            GameController.instance.towerHitPoints -= 25;
            baseHealth.GetComponent<BaseHealthBar>().SetHealth(GameController.instance.towerHitPoints);
            ScreenShakeController.instance.LittleShake();
            collision.gameObject.SendMessage("FullDestroy");      
            if(GameController.instance.towerHitPoints <= 0)
            {
                GameController.instance.youLoose();
            }
        }
    }
}
