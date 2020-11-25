using System.Collections;
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
        public float ms;
        public GameController gc;

        public EnemyParams(int h, int ah, EnemyController.Level l, EnemyController.EnemyType at, GameObject c, float ms, GameController gc)
        {
            this.health = h;
            this.armorHealth = ah;
            this.lvl = l;
            this.enemyType = at;
            this.canvas = c;
            this.ms = ms;
            this.gc = gc;
        }
    }


    public GameObject enemy;
    Vector2 whereToSpawn;
    public float spawnRate = 0.3f;
    private float nextSpawn = 0.0f;
    private float ms = 1;
    public int enemiesHealth;
    public int enemiesArmorHealth;
    public int enemiesQuantity;
    public EnemyController.Level lvl;
    public EnemyController.EnemyType enemiesType;
    public GameObject canvas;
    public GameController gc;

    private float[,] wavesInfo;
    private GameController.State currentState;
    private int currentWave;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameController.State.Wait;
        
    }

    public void Create(float[,] wavesInfo, GameObject c, GameController gc)
    {
        this.wavesInfo = wavesInfo;
        this.gc = gc;
        this.canvas = c;
        this.currentWave = 0;
        this.currentState = GameController.State.Wait;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameController.State.Wait:
                UpdateWaitState();
                break;
            case GameController.State.Wave:
                UpdateWaveState();
                break;
        }
    }

    void UpdateWaitState()
    {
        
    }

    void UpdateWaveState()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            whereToSpawn = new Vector2(transform.position.x, transform.position.y);
            GameObject newEnemy = Instantiate(enemy, whereToSpawn, Quaternion.identity);
            newEnemy.SendMessage("Create", new EnemyParams(enemiesHealth, enemiesArmorHealth,
                lvl, enemiesType, canvas, ms, gc));
            enemiesQuantity--;
        }
        if(enemiesQuantity == 0)
        {
            EnterWaitState();
        }
    }

    void EnterWaitState()
    {
        currentState = GameController.State.Wait;
    }

    void EnterWaveState(int wave)
    {
        currentWave = wave;
        enemiesHealth = (int)wavesInfo[currentWave, 2];
        enemiesArmorHealth = (int)wavesInfo[currentWave, 3];
        lvl = (EnemyController.Level)wavesInfo[currentWave, 0];
        enemiesType = (EnemyController.EnemyType) wavesInfo[currentWave, 1];
        enemiesQuantity = (int) wavesInfo[currentWave, 4];
        ms = wavesInfo[0, 5];
        switch (lvl)
        {
            case EnemyController.Level.Small:
                spawnRate = 0.3f;
                break;
            case EnemyController.Level.Medium:
                spawnRate = 0.8f;
                break;
            case EnemyController.Level.Big:
                spawnRate = 1.0f;
                break;
        }
        nextSpawn = Time.time + wavesInfo[currentWave, 6] + spawnRate;
        currentState = GameController.State.Wave;
    }

}
