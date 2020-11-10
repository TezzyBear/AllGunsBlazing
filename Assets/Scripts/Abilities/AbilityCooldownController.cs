using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownController : MonoBehaviour
{
    public GameObject bindedCharacter;
    [SerializeField]
    private Image backgroundImage;
    private AbilityController abilityController;
    [SerializeField]
    private Image overlay;
    [SerializeField]
    private Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        abilityController = bindedCharacter.transform.GetComponentInChildren<AbilityController>();
        backgroundImage.sprite = abilityController.abilityImage;
        
    }

    // Update is called once per frame
    void Update()
    {
        overlay.fillAmount = abilityController.abilityCooldownTimer / abilityController.abilityCooldown;
        timerText.text = abilityController.abilityCooldownTimer.ToString("F0");
    }
}
