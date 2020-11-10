﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{

    public struct EnemyParams
    {
        public int health;
        public int armorHealth;
        public EnemyController.Level lvl;
        public EnemyController.EnemyType enemyType;
        public GameObject canvas;

        public EnemyParams(int h, int ah, EnemyController.Level l, EnemyController.EnemyType at, GameObject c)
        {
            this.health = h;
            this.armorHealth = ah;
            this.lvl = l;
            this.enemyType = at;
            this.canvas = c;
    }
    }

    public GameObject enemy;
    Vector2 whereToSpawn;
    public float spawnRate = 0.3f;
    private float nextSpawn = 0.0f;
    public int enemiesHealth;
    public int enemiesArmorHealth;
    public EnemyController.Level lvl;
    public EnemyController.EnemyType enemiesType;
    public GameObject canvas;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            whereToSpawn = new Vector2(transform.position.x, transform.position.y);
            GameObject newEnemy = Instantiate(enemy, whereToSpawn, Quaternion.identity);
            newEnemy.SendMessage("Create", new EnemyParams(enemiesHealth, enemiesArmorHealth, lvl, enemiesType, canvas));
        }
    }
}
