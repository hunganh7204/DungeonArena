using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthBar;
    
    public void UpdateHealthBar(float percent)
    {
        healthBar.value = percent;
    }

}
