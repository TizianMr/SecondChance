using UnityEngine;
using UnityEngine.UI;

public class SS_AmmoBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxAmmo(float ammo)
    {
        slider.maxValue = ammo;
        slider.value = ammo;

    }
    public void SetAmmo(float ammo)
    {
        slider.value = ammo;

    }
}
