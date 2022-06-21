using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void SetHealth(float value)
    {
        slider.value = value;
    }
}
