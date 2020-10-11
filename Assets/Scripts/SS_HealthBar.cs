//Skript mit Hilfe des Tutorials "How to make a HEALTH BAR in Unity!" von Brackeys erstellt
//https://www.youtube.com/watch?v=BLfNP4Sc_iA&t=354s

using UnityEngine;
using UnityEngine.UI;

public class SS_HealthBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
