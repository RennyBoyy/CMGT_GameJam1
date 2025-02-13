using UnityEngine;
using TMPro; 

public class AppleCounter : MonoBehaviour
{
    public TextMeshProUGUI appleCounterText; 
    private int applesEaten = 0; 

    public void AddApple()
    {
        applesEaten++; 
        appleCounterText.text = "Apples : " + applesEaten; 
    }
}
