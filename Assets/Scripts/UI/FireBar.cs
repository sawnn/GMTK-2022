using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBar : MonoBehaviour
{
    public Slider slider;

    public void SetMinFire()
    {
        slider.value = 0;
    }

    public void SetFire(int fire)
    {
        slider.value = fire;
    }

}
