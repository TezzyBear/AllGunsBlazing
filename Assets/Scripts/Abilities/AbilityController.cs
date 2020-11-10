using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Ability {
        UNDEFINED,
        NUCLEAR_JUSTICE,
        UNSTOPABLE_DUCKFENSE,
        FIRE_AT_WILL
    }

    [SerializeField]
    private Ability ability = Ability.UNDEFINED;
    [SerializeField]
    private GameObject turretObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            castAbility(ability);
        }
    }

    void castAbility(Ability ability) {
        switch (ability)
        {
            case Ability.UNDEFINED:
                //Do nothing
                break;
            case Ability.NUCLEAR_JUSTICE:
                //Make a number of missiles arrive and explode dealing global damage to all units and damage over time
                break;
            case Ability.UNSTOPABLE_DUCKFENSE:
                //Puts down a turret that deals damage for a duration
                break;
            case Ability.FIRE_AT_WILL:
                //Gigantic laser beam in lane for massive amounts of damage and pushback to enemies
                break;
            default:
                break;
        }
    }

    void nuclearJustice() {
        //animation
        //damage to all enemies on screen
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies) {
            //enemy.GetComponent<EnemyController>().Damage(100);
        }
    }

    void unstopableDuckfense() {
        Instantiate(turretObject, transform.position, Quaternion.identity);
    }

    void fireAtWill() { 
        //make sprite long and less and less high as time pases (fade out effect)
        //raycast line
        //deal damage to all enemies in raycast
        //pushback all enemies in raycast
    }
}
