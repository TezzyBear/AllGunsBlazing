using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private float[,] bcMetrics = { { -0.1602327f, -0.04214495f, 1.054409f, 0.6634768f},
                                    { -0.06081128f, -0.245612f, 0.8000755f, 1.477345f},
                                    { -0.3151496f, -0.3017359f, 1.122926f, 2.689894f} };
    public enum State
    {
        Walking,
        Dead
    }

    public enum Level
    {
        Small,
        Medium,
        Big
    }
    public enum EnemyType
    {
        Rock,
        Wood,
        Steel,
        Fire,
        Unarmored
    }

    private State currentState;

    public float
        movementSpeed;
    

    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;

    private int
        facingDirection;

    private Vector2 movement;

    private GameObject alive;
    [SerializeField]
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D aliveBc;
    private Rigidbody2D aliveRb;
    private AliveController aliveC;


    private Level level;

   
    void Awake()
    {
        alive = transform.Find("Alive").gameObject;
        spriteRenderer = alive.GetComponent<SpriteRenderer>();
        aliveRb = alive.GetComponent<Rigidbody2D>();
        aliveBc = alive.GetComponent<BoxCollider2D>();
        aliveC = alive.GetComponent<AliveController>();
    }
    // Start is called before the first frame update
    void Start()
    {

        facingDirection = -1;
        
    }

    public void Create(EnemySpawnController.EnemyParams param)
    {
        level = param.lvl;
        movementSpeed = param.ms;
        spriteRenderer.sprite = sprites[(int)level];
        aliveBc.offset = new Vector2(bcMetrics[(int)level, 0], bcMetrics[(int)level, 1]);
        aliveBc.size = new Vector2(bcMetrics[(int)level, 2], bcMetrics[(int)level, 3]);
        aliveC.Create(param.health, param.armorHealth, level, param.enemyType, param.canvas, param.gc);
    }

    

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    
    
    // WALKING ------------------------------
    private void EnterWalkingState()
    {
        
    }

    private void UpdateWalkingState()
    {
        movement.Set(movementSpeed * facingDirection, aliveRb.velocity.y);
        aliveRb.velocity = movement;
        
    }

    private void ExitWalkingState()
    {

    }

    // DEAD ---------------------------------

    private void EnterDeadState()
    {
        // Spawn chunks and blood
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    // OTHER FUNCTIONS -------------------------------

    

    public void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        
    }
}
