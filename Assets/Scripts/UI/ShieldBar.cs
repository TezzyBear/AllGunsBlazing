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

    public void SetMaxShield(int shield)
    {
        slider.maxValue = shield;
        slider.value = shield;
    }

    public void SetShield(int shield)
    {
        slider.value = shield;
    }

    public void SetColor(float r, float g, float b)
    {
        fill.color = new Color(r, g, b);
    }
}
