using UnityEngine;
using UnityEngine.UI;
 
public class HealthBarHandler : MonoBehaviour
{
    private static Image HealthBarImage;
 
    /// <summary>
    /// Sets the health bar value
    /// </summary>
    /// <param name="value">should be between 0 to 1</param>
    public void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;
        if(HealthBarImage.fillAmount < 0.3f)
        {
            SetHealthBarColor(Color.red);
        }
        else if(HealthBarImage.fillAmount < 0.6f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
    }
 
    public float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }
 
    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public static void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }
 
    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Start()
    {
        HealthBarImage = GetComponent<Image>();
    }
}