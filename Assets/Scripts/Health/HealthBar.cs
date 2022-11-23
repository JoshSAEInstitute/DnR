using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHealth(int health)
    {
        //Sets HP max value
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        //Controls the health amount
        slider.value = health;
    }


}
