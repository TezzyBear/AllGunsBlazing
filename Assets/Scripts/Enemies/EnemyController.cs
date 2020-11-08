using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Walking,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float
        movementSpeed,
        maxHealth;

    [SerializeField]
    private GameObject
        hitParticle,
        deathChunkParticle,
        deathBloodParticle;

    private int facingDirection;
    private float 
        currentHealth;

    private Vector2 movement;

    private GameObject alive;
    private Rigidbody2D aliveRb;

    // TEMP VARIABLES
    [SerializeField]
    private float damageRate;

    private float damageTimer;



   

    // Start is called before the first frame update
    void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();

        facingDirection = -1;
        currentHealth = maxHealth;


        damageTimer = 0.0f;
       
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
        if(Time.time > damageTimer)
        {
            damageTimer = Time.time + damageRate;
            Damage();
        }
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

    private void Damage()
    {
        currentHealth -= 30;

        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        if(currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void SwitchState(State state)
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
