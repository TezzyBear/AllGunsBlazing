using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum State
    {
        Walking,
        Dead
    }

    private State currentState;

    [SerializeField]
    private float
        movementSpeed;
    

    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;

    private int
        facingDirection;

    private Vector2 movement;

    private GameObject alive;
    private Rigidbody2D aliveRb;




   

    // Start is called before the first frame update
    void Start()
    {
        alive = transform.Find("Alive").gameObject;
        aliveRb = alive.GetComponent<Rigidbody2D>();

        facingDirection = -1;
        
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
