using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public enum ShieldType
    {
        ROCK_ARMOR,
        WOOD_ARMOR,
        STEEL_ARMOR,
        PYRO_ARMOR,
        UNARMORED
    }

    public void SetMaxShield(int shield)
    {
        slider.maxValue = shield;
        slider.value = shield;
    }

    public void SetShield(int shield)
    {
        slider.value = shield;
    }

    public void SetColor(string shieldType)
    {
        switch (shieldType)
        {
            case "ROCK_ARMOR":
                fill.color = new Color(0.2196f, 0.1765f, 0.0627f, 1.0f);
                break;
            case "WOOD_ARMOR":
                fill.color = new Color(0.6118f, 0.4471f, 0.0f, 1.0f);
                break;
            case "STEEL_ARMOR":
                fill.color = new Color(0.5255f, 0.5255f, 0.5255f, 1.0f);
                break;
            case "PYRO_ARMOR":
                fill.color = new Color(0.7608f, 0.21570f, 0.1451f, 1.0f);
                break;
            case "UNARMORED":
                fill.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                break;
            default:
                break;
        }
        
    }
}
