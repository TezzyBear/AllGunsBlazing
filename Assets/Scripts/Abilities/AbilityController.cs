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
    [SerializeField]
    private GameObject nuclearJusticeObject;
    [SerializeField]
    private GameObject gigaLaserObject;

    [HideInInspector]
    public Sprite abilityImage;

    [HideInInspector]
    public float abilityCooldown;
    [HideInInspector]
    public float abilityCooldownTimer;

    void Start()
    {
        switch (ability)
        {
 
            case Ability.NUCLEAR_JUSTICE:
                abilityCooldown = 15.0f;
                abilityImage = nuclearJusticeObject.GetComponent<SpriteRenderer>().sprite;
                break;
            case Ability.UNSTOPABLE_DUCKFENSE:
                abilityCooldown = 20.0f;
                abilityImage = turretObject.GetComponent<SpriteRenderer>().sprite;
                break;                
            case Ability.FIRE_AT_WILL:
                abilityCooldown = 10.0f;
                abilityImage = gigaLaserObject.GetComponent<SpriteRenderer>().sprite;
                break;
        }     
    }

    // Update is called once per frame
    void Update()
    {
        abilityCooldownTimer -= Time.deltaTime;
        if (transform.parent.GetComponent<CharacterMovement>().isSelected && abilityCooldownTimer <= 0)
        {
            if (Input.GetKeyDown("r"))
            {
                castAbility(ability);
            }
        }
        if (abilityCooldownTimer <= 0) {
            abilityCooldownTimer = 0; 
        }
    }

    void castAbility(Ability ability) {
        abilityCooldownTimer = abilityCooldown;

        switch (ability)
        {
            case Ability.UNDEFINED:
                //Do nothing
                break;
            case Ability.NUCLEAR_JUSTICE:
                nuclearJustice();
                break;
            case Ability.UNSTOPABLE_DUCKFENSE:
                unstopableDuckfense();
                break;
            case Ability.FIRE_AT_WILL:
                fireAtWill();
                break;
            default:
                break;
        }
    }

    void nuclearJustice() {
        //animation
        //damage to all enemies on screen
        Instantiate(nuclearJusticeObject);
    }

    void unstopableDuckfense() {
        Instantiate(turretObject, transform.position, Quaternion.identity);
    }

    void fireAtWill() {
        Vector3 laserPosOffset = new Vector3(transform.position.x + 10.5f, transform.position.y, transform.position.y);
        GameObject gigaLaserInstance = Instantiate(gigaLaserObject, laserPosOffset, Quaternion.Euler(new Vector3(0, 0, 90.0f)));
        gigaLaserInstance.transform.localScale = new Vector3(5.0f, 20.0f, 1.0f);

        //make sprite long and less and less high as time pases (fade out effect)
        //raycast line
        //deal damage to all enemies in raycast
        //pushback all enemies in raycast
    }
}
